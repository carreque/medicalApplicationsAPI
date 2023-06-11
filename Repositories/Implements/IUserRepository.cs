using medicalAppointmentsAPI.Models.DTO;

namespace medicalAppointmentsAPI.Repositories.Implements
{
    public interface IUserRepository
    {
        Task<List<UsuarioDTO>> GetUsuarios();

        Task<UsuarioDTO> GetUsuarioById(int id);

        Task<int> Register(UsuarioDTO userDTO, string password);

        Task<UsuarioDTO> UpdateUser(UsuarioDTO userDTO);
        Task<bool> DeleteUser(int id);

        Task<Dictionary<string, string>> Login(string username, string password);
        Task<bool> UserExist(int id);

        Task<List<UsuarioDTO>> GetMedics();

    }
}
