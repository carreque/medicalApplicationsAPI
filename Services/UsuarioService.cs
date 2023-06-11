using medicalAppointmentsAPI.Models.DTO;
using medicalAppointmentsAPI.Repositories.Implements;

namespace medicalAppointmentsAPI.Services
{
    public class UsuarioService
    {
        private readonly IUserRepository _userRepository;

        public UsuarioService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> UserCreation(UsuarioDTO usuarioDTO, string password)
        {
            int respuesta = await _userRepository.Register(usuarioDTO, password);
            return respuesta;
        }

        public async Task<List<UsuarioDTO>> GetAllUsers()
        {
            List<UsuarioDTO> users = await _userRepository.GetUsuarios();
            return users;
        }

        public async Task<UsuarioDTO> GetUserById(int id)
        {
            UsuarioDTO usuarioModel = await _userRepository.GetUsuarioById(id);
            return usuarioModel;
        }

        public async Task<UsuarioDTO> UpdateUser(UsuarioDTO usuarioDTO)
        {
            
            UsuarioDTO usuarioModel = await _userRepository.UpdateUser(usuarioDTO);
            return usuarioModel;
        }

        public async Task<bool> DeleteUser(int id)
        {
            bool isDeleted = await _userRepository.DeleteUser(id);
            return isDeleted;
        }

        public async Task<bool> UserExists(int id)
        {
            bool isUserActive = await _userRepository.UserExist(id);
            return isUserActive;
        }
        public async Task<Dictionary<string, string>> Login(string username, string password)
        {
            Dictionary<string, string> answerLogin = await _userRepository.Login(username, password);
            return answerLogin;
        }

        public async Task <List<UsuarioDTO>> GetMedics()
        {
            List<UsuarioDTO> medicos = await _userRepository.GetMedics();
            return medicos;
        }
    }
}
