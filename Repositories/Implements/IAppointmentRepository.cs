using medicalAppointmentsAPI.Models.DTO;
using medicalAppointmentsApplication.Models;

namespace medicalAppointmentsAPI.Repositories.Implements
{
    public interface IAppointmentRepository
    {
        Task<List<Cita>> GetAppointments();

        Task<Cita> GetAppointmentById(int id);

        Task<Cita> CreateUpdate(Cita cita);

        Task<bool> DeleteAppointment(int id);

        Task<List<Cita>> GetPatientAppointments(int patientId);

        Task<List<Cita>> GetAppointementsWithoutDiagnosis();

        Task<bool> DeleteAppointmentsFromUser(int userId);
    }
}
