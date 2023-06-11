using medicalAppointmentsApplication.Models;

namespace medicalAppointmentsAPI.Repositories.Implements
{
    public interface IDiagnosticoRepository
    {
        Task<List<Diagnostico>> GetDiagnosticos();
        Task<Diagnostico> GetDiagnosticoById(int id);
        Task<Diagnostico> CreateAndUpdate(Diagnostico diagnostico);
        Task<bool> DeleteDiagnostico(int id);
        Task<Diagnostico> GetDiagnosticoByIdCita(int cita_id);

  
    }
}
