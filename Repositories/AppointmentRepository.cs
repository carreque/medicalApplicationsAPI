using AutoMapper;
using medicalAppointmentsAPI.Models.DTO;
using medicalAppointmentsAPI.Repositories.Implements;
using medicalAppointmentsApplication.ContextApplication;
using medicalAppointmentsApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace medicalAppointmentsAPI.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationContext _db;
        private IMapper _mapper;

        public AppointmentRepository(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Cita> CreateUpdate(Cita cita)
        {
            if (cita.Id != null && cita.Id > 0)
            {
                _db.Appointment.Update(cita);
            }
            else
            {
                await _db.Appointment.AddAsync(cita);
            }

            await _db.SaveChangesAsync();
            return cita;
        }

        public async Task<bool> DeleteAppointment(int id)
        {
            try
            {
                Cita appointment = await _db.Appointment.FindAsync(id);

                if (appointment == null)
                {
                    return false;
                }

                _db.Appointment.Remove(appointment);
                await _db.SaveChangesAsync();
                return true;

            }catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<Cita> GetAppointmentById(int id)
        {
            Cita appointment = await _db.Appointment.FindAsync(id);
            return appointment;
        }

        public async Task<List<Cita>> GetAppointments()
        {
            List<Cita> citas = await _db.Appointment.ToListAsync();
            return citas;
        }

        public async Task<List<Cita>> GetPatientAppointments(int patientId)
        {
            using (var context = _db)
            {
                var pacienteCita = await context.Appointment.Where(cita => cita.Patient_id == patientId && cita.diagnostico_id != 0).ToListAsync();
                return pacienteCita;
            }
                        
        }

        public async Task<List<Cita>> GetAppointementsWithoutDiagnosis()
        {
            using (var context = _db)
            {
                var pacienteCita = await context.Appointment.Where(cita => cita.diagnostico_id == 0).ToListAsync();
                return pacienteCita;
            }
        }

        public async Task<bool> DeleteAppointmentsFromUser(int userId)
        {

            try
            {

                using (var context = _db)
                {
                    var recordsToDelete = await context.Appointment.Where(cita => cita.Patient_id == userId).ToListAsync();
                    int[] idsToDelete = recordsToDelete.Select(record => record.diagnostico_id).ToArray();

                    foreach(int idToDelete in idsToDelete)
                    {
                        var diagnosticoToDelete = await context.Diagnosis.FindAsync(idToDelete);
                        if (diagnosticoToDelete != null)
                        {
                            context.Diagnosis.Remove(diagnosticoToDelete);
                        }
                    }


                    context.Appointment.RemoveRange(recordsToDelete);
                    await context.SaveChangesAsync();
                    return true;

                }
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
