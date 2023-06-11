using AutoMapper;
using medicalAppointmentsAPI.Models.DTO;
using medicalAppointmentsAPI.Repositories;
using medicalAppointmentsAPI.Repositories.Implements;
using medicalAppointmentsApplication.Models;
using Microsoft.Build.Framework;

namespace medicalAppointmentsAPI.Services
{
    public class CitaService
    {
        private IMapper _mapper;
        private IAppointmentRepository _appointmentRepository;

        public CitaService(IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<CitaDTO>> GetAllAppointments()
        {
            List<Cita> citas = await _appointmentRepository.GetAppointments();
            return _mapper.Map<List<CitaDTO>>(citas);
        }

        public async Task<CitaDTO> GetAppointmentById(int id)
        {
            Cita cita = await _appointmentRepository.GetAppointmentById(id);
            return _mapper.Map<Cita, CitaDTO>(cita);
        }

        public async Task<CitaDTO> CreateAppointment(CitaDTO citaDTO)
        {
            Cita citaToCreate = _mapper.Map<CitaDTO, Cita>(citaDTO);
            citaToCreate = await _appointmentRepository.CreateUpdate(citaToCreate);
            return _mapper.Map<Cita, CitaDTO>(citaToCreate);
        }

        public async Task<CitaDTO>  UpdateAppointment(CitaDTO citaDTO)
        {

            Cita citaToUpdate = _mapper.Map<CitaDTO, Cita>(citaDTO);
            citaToUpdate = await _appointmentRepository.CreateUpdate(citaToUpdate);
            return _mapper.Map<Cita, CitaDTO>(citaToUpdate);
        }
        public async Task<bool> DeleteAppointment(int id)
        {
            bool isDeleted = await _appointmentRepository.DeleteAppointment(id);
            return isDeleted;
        }

        public async Task<List<CitaDTO>> GetPatientAppointments(int patientId)
        {
            List<Cita> appointmentsFounded = await _appointmentRepository.GetPatientAppointments(patientId);
            return _mapper.Map<List<CitaDTO>>(appointmentsFounded);
        }

        public async Task<List<CitaDTO>> GetAppointemntsWithoutDiagnosis()
        {
            List<Cita> appointmentsFounded = await _appointmentRepository.GetAppointementsWithoutDiagnosis();
            return _mapper.Map<List<CitaDTO>>(appointmentsFounded);
        }

        public async Task<bool> DeleteAppointmentsFromUser(int userId)
        {
            bool isDeleted = await _appointmentRepository.DeleteAppointmentsFromUser(userId);
            return isDeleted;
        }
    }
}
