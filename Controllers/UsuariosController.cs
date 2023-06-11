using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using medicalAppointmentsApplication.ContextApplication;
using medicalAppointmentsApplication.Models;
using medicalAppointmentsAPI.Models.DTO;
using medicalAppointmentsAPI.Repositories.Implements;
using medicalAppointmentsAPI.Services;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace medicalAppointmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        //private readonly ApplicationContext _context;
        private readonly UsuarioService _usuarioService;
        protected ResponseDTO _response;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
            _response = new ResponseDTO();
        }

        // GET: api/Usuarios
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsers()
        {
            try
            {
                var lista = await _usuarioService.GetAllUsers();
                _response.Result = lista;
                _response.MessageDisplay = "Lista de usuarios obtenidos";
            }catch (Exception ex) 
            { 
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_response);
        }

        // GET: api/Usuarios/Medicos
        [HttpGet("/api/Medicos")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetMedics()
        {
            try
            {
                var lista = await _usuarioService.GetMedics();
                _response.Result = lista;
                _response.MessageDisplay = "Lista de medicos obtenidos";
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_response);
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetUserById(id);
          
            if (usuario == null)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Usuario inexistente";
                return NotFound(_response);
            }

            _response.Result = usuario;
            _response.MessageDisplay = "Informacion del usuario";
            return Ok(_response);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login()
        {
            string requestBody = null;

            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                   requestBody = await reader.ReadToEndAsync();
            }

            dynamic requestObject = JsonConvert.DeserializeObject(requestBody);
            
            string username = requestObject.username;
            string password = requestObject.password;
            Dictionary<string, string> respuesta = await _usuarioService.Login(username, password);
            if (respuesta.Count == 0) 
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Usuario o password incorrectos";
                return BadRequest(_response);
            }

            _response.Result = respuesta;
            _response.MessageDisplay = "Usuario conectado";
            return Ok(_response);

        }
        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDTO usuarioDTO)
        {
            try
            {
                UsuarioDTO userModel = await _usuarioService.UpdateUser(usuarioDTO);
                _response.Result = userModel;
                return Ok(_response);

            }catch(Exception ex)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Error al actualizar el usuario";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Register")]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                var answer = await _usuarioService.UserCreation(usuarioDTO, usuarioDTO.Password);
                if (answer == - 1)
                {
                    _response.isSuccess = false;
                    _response.MessageDisplay = "Usuario ya existe";
                    return BadRequest(_response);
                }

                if (answer == -500)
                {
                    _response.isSuccess = false;
                    _response.MessageDisplay = "Error al crear usuario";
                    return BadRequest(_response);
                }

                _response.MessageDisplay = "Usuario creado correctamente";
                _response.Result = answer;
                return Ok(_response);

            }catch(Exception ex)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Error al crear el usuario";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }

        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                bool isDeleted = await _usuarioService.DeleteUser(id);

                if (isDeleted)
                {
                    _response.Result = isDeleted;
                    _response.MessageDisplay = "Usuario Eliminado con exito";
                    return Ok(_response);
                }

                _response.isSuccess = false;
                _response.MessageDisplay = "Se produjo un error al eliminar el usuario";
                return BadRequest(_response);

            }
            catch(Exception ex)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Error al eliminar el usuario";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
    }
}
