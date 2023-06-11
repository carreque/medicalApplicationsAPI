using AutoMapper;
using medicalAppointmentsAPI.Models.DTO;
using medicalAppointmentsAPI.Repositories.Implements;
using medicalAppointmentsApplication.Models;

namespace medicalAppointmentsAPI.Services
{
    public class DiagnosticoService
    {
        private IMapper _mapper;
        private readonly IDiagnosticoRepository _diagnosticoRepository;

        public DiagnosticoService(IMapper mapper, IDiagnosticoRepository diagnosticoRepository)
        {
            _mapper = mapper;
            _diagnosticoRepository = diagnosticoRepository;
        }

        public async Task<List<DiagnosticoDTO>> GetAllDiagnosticos()
        {
            List<Diagnostico> diagnosticos = await _diagnosticoRepository.GetDiagnosticos();
            return _mapper.Map<List<DiagnosticoDTO>>(diagnosticos);
        }

        public async Task<DiagnosticoDTO> GetDiagnosticoById(int id)
        {
            Diagnostico diagnostico = await _diagnosticoRepository.GetDiagnosticoById(id);
            return _mapper.Map<Diagnostico, DiagnosticoDTO>(diagnostico);
        }

        public async Task<DiagnosticoDTO> CreateDiagnostico(DiagnosticoDTO diagnosticoDTO)
        {
            Diagnostico diagnosticoToCreate = _mapper.Map<DiagnosticoDTO, Diagnostico>(diagnosticoDTO);
            diagnosticoToCreate = await _diagnosticoRepository.CreateAndUpdate(diagnosticoToCreate);
            return _mapper.Map<Diagnostico, DiagnosticoDTO>(diagnosticoToCreate);
        }

        public async Task<DiagnosticoDTO> UpdateDiagnostico(DiagnosticoDTO diagnosticoDTO)
        {
            Diagnostico diagnosticoToCreate = _mapper.Map<DiagnosticoDTO, Diagnostico>(diagnosticoDTO);
            diagnosticoToCreate = await _diagnosticoRepository.CreateAndUpdate(diagnosticoToCreate);
            return _mapper.Map<Diagnostico, DiagnosticoDTO>(diagnosticoToCreate);
        }
        public async Task<bool> DeleteDiagnostico(int id)
        {

            bool isDiagnosticoDeleted = await _diagnosticoRepository.DeleteDiagnostico(id);
            return isDiagnosticoDeleted;
        }

        public async Task<DiagnosticoDTO> GetADiagnosticoFromAppointment(int id_cita)
        {
            Diagnostico diagnostico = await _diagnosticoRepository.GetDiagnosticoByIdCita(id_cita);
            return _mapper.Map<Diagnostico, DiagnosticoDTO>(diagnostico);
        }

    }
}
