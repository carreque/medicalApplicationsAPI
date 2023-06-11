using AutoMapper;
using medicalAppointmentsAPI.Models.DTO;
using medicalAppointmentsAPI.Repositories.Implements;
using medicalAppointmentsApplication.ContextApplication;
using medicalAppointmentsApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace medicalAppointmentsAPI.Repositories
{
    public class UsuarioRepositorio : IUserRepository
    {
        private readonly ApplicationContext _db;
        private IMapper _mapper;
        private readonly IConfiguration _config;
        //Constructor de la clase repositorio en la que se asocia el contexto de la aplicación y el mapper
        public UsuarioRepositorio(ApplicationContext db, IMapper mapper, IConfiguration config)
        {
            _db = db;
            _mapper = mapper;
            _config = config;
        }

        
        public async Task<int> Register(UsuarioDTO userDTO, string password)
        {
            try
            {

                string discriminator = userDTO.Discriminator;              
                if (await UserExist(userDTO.Id))
                {
                    return -1;
                }

                if (discriminator == "Medico")
                {
                    Medico user = _mapper.Map<UsuarioDTO, Medico>(userDTO);
                    CrearPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    await _db.Medics.AddAsync(user);
                    await _db.SaveChangesAsync();
                    return user.Id;
                }
                else if (discriminator == "Paciente")
                {
                    Paciente user = _mapper.Map<UsuarioDTO, Paciente>(userDTO);
                    CrearPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    await _db.Patients.AddAsync(user);
                    await _db.SaveChangesAsync();
                    return user.Id;
                }

                return 0;

            }catch(Exception)
            {
                return -500;
            }

            
        }

        public async Task<List<UsuarioDTO>> GetUsuarios()
        {
            List<Medico> medics = await _db.Medics.ToListAsync();
            List<Paciente> patient = await _db.Patients.ToListAsync();

            List<UsuarioDTO> medicsUsers = _mapper.Map<List<UsuarioDTO>>(medics);
            List<UsuarioDTO> patientUsers = _mapper.Map<List<UsuarioDTO>>(patient);

            List<UsuarioDTO> usersInApplication = medicsUsers.Concat(patientUsers).ToList(); 
            return _mapper.Map<List<UsuarioDTO>>(usersInApplication);
        }

        public async Task<UsuarioDTO> GetUsuarioById(int id)
        {

            Medico medic = await _db.Medics.FindAsync(id);

            if (medic != null)
            {
                return _mapper.Map<Medico, UsuarioDTO>(medic);
            }

            Paciente patient = await _db.Patients.FindAsync(id);
            return _mapper.Map<Paciente, UsuarioDTO>(patient);

        }

        public async Task<UsuarioDTO> UpdateUser(UsuarioDTO usuarioDTO)
        {
            string discriminator = usuarioDTO.Discriminator;
            var idToCheck = usuarioDTO.Id;


            if (idToCheck != null && idToCheck > 0)
            {              

                if (discriminator == "Medico")
                {

                    Medico oldUser = await _db.Medics.FindAsync(usuarioDTO.Id);
                    oldUser.name = usuarioDTO.name;
                    oldUser.lastnames = usuarioDTO.lastnames;
                    oldUser.user = usuarioDTO.user;
                    oldUser.MemberShipNumber = usuarioDTO.MemberShipNumber;
                    oldUser.key = usuarioDTO.key;

                    if (usuarioDTO.Password != null)
                    {
                        CrearPasswordHash(usuarioDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                        oldUser.PasswordHash = passwordHash;
                        oldUser.PasswordSalt = passwordSalt;
                    }
                     _db.Medics.Update(oldUser);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<Medico, UsuarioDTO>(oldUser);
                }
                else if (discriminator == "Paciente")
                {
                    Paciente oldUser = await _db.Patients.FindAsync(usuarioDTO.Id);
                    oldUser.name = usuarioDTO.name;
                    oldUser.lastnames = usuarioDTO.lastnames;
                    oldUser.user = usuarioDTO.user;
                    oldUser.key = usuarioDTO.key;
                    oldUser.address = usuarioDTO.address;
                    oldUser.cardNumber = usuarioDTO.cardNumber;
                    oldUser.tlf = usuarioDTO.tlf;
                    oldUser.NSS = usuarioDTO.NSS;

                    if (usuarioDTO.Password != null)
                    {
                        CrearPasswordHash(usuarioDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                        oldUser.PasswordHash = passwordHash;
                        oldUser.PasswordSalt = passwordSalt;
                    }

                    _db.Patients.Update(oldUser);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<Paciente, UsuarioDTO>(oldUser);
                }
            }

            return usuarioDTO;
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                Usuario user = await _db.Users.FindAsync(id);

                if (user == null)
                {
                    return false;
                }

                //También se podría implementar en vez de un borrado físico uno lógio en caso de tener un state que indique si el registro está activo o no.
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return true;

            }catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UserExist(int id)
        {
            if (await _db.Users.FindAsync(id) != null)
            {
                return true;
            }

            return false;
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            passwordSalt = Encoding.UTF8.GetBytes(BCrypt.Net.BCrypt.GenerateSalt(12));         
            string passwordHashDecoded = BCrypt.Net.BCrypt.HashPassword(password, Encoding.UTF8.GetString(passwordSalt));
            passwordHash = Encoding.UTF8.GetBytes(passwordHashDecoded);
        }

        private bool VerifyPassword(string password, byte[] hash)
        {
            string hashDecoded = Encoding.UTF8.GetString(hash);
            return BCrypt.Net.BCrypt.Verify(password, hashDecoded);
        }

        
        public async Task<Dictionary<string, string>> Login( string username, string password)
        {
            var userToLogin = await _db.Users.FirstOrDefaultAsync(user => user.name.ToLower().Equals(username.ToLower()));
            var dict = new Dictionary<string, string>();
            if (userToLogin == null)
            {
                return dict;
            }

            if (!VerifyPassword(password, userToLogin.PasswordHash))
            {
                return dict;
            }

            dict.Add("token", TokenCreation(username, userToLogin.Id));
            dict.Add("id", userToLogin.Id.ToString());
            return dict;
        }

        private string TokenCreation(string username, int id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name,username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:keyToken").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddHours(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<List<UsuarioDTO>> GetMedics()
        {
            List<UsuarioDTO> medics = null;
            try
            {
                List < Medico > medicos = await _db.Medics.ToListAsync();
                medics = _mapper.Map<List<UsuarioDTO>>(medicos);
                return medics;
            }
            catch (Exception)
            {
                return medics;
            }
        }
    }
}
