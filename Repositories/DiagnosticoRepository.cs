using medicalAppointmentsAPI.Models.DTO;
using medicalAppointmentsAPI.Repositories.Implements;
using medicalAppointmentsApplication.ContextApplication;
using medicalAppointmentsApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace medicalAppointmentsAPI.Repositories
{
    public class DiagnosticoRepository : IDiagnosticoRepository
    {
        private readonly ApplicationContext _db;
        
        public DiagnosticoRepository(ApplicationContext db)
        {
            _db = db;
        }
        public async Task<Diagnostico> CreateAndUpdate(Diagnostico diagnostico)
        {
            if (diagnostico.Id != null && diagnostico.Id > 0)
            {
                
                _db.Diagnosis.Update(diagnostico);
            }
            else
            {
                await _db.Diagnosis.AddAsync(diagnostico);
            }

            await _db.SaveChangesAsync();
            return diagnostico;
        }

        public async Task<bool> DeleteDiagnostico(int id)
        {
            try
            {
                Diagnostico diagnostico = await _db.Diagnosis.FindAsync(id);

                if (diagnostico == null)
                {
                    return false;
                }

                _db.Diagnosis.Remove(diagnostico);
                await _db.SaveChangesAsync();
                return true;

            }catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<Diagnostico> GetDiagnosticoById(int id)
        {
            Diagnostico diagnostico = await _db.Diagnosis.FindAsync(id);
            return diagnostico;
        }

        public async Task<Diagnostico> GetDiagnosticoByIdCita(int cita_id)
        {
            using (var context = _db)
            {
                Diagnostico diagnostico = await context.Diagnosis.Where(diagnosis => diagnosis.cita_id == cita_id).FirstOrDefaultAsync();
                return diagnostico;
            }
        }

        public async Task<List<Diagnostico>> GetDiagnosticos()
        {
            List<Diagnostico> diagnosticos = await _db.Diagnosis.ToListAsync();
            return diagnosticos;
        }

        public Task<List<Diagnostico>> GetDiagnosticosFromUser(int user_id)
        {
            throw new NotImplementedException();
        }
    }
}
