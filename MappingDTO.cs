using AutoMapper;
using medicalAppointmentsAPI.Models.DTO;
using medicalAppointmentsApplication.Models;

namespace medicalAppointmentsAPI
{
    public class MappingDTO
    {
        /// Función encargada de realizar los mapeos entre modelos y DTO (Data transfer Object).
        public static MapperConfiguration RegisterMaps()
        {
            
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<UsuarioDTO, Medico>();
                config.CreateMap<Medico, UsuarioDTO>();
                config.CreateMap<UsuarioDTO, Usuario>();
                config.CreateMap<Usuario, UsuarioDTO>();
                config.CreateMap<UsuarioDTO, Paciente>();
                config.CreateMap<Paciente, UsuarioDTO>();
                config.CreateMap<CitaDTO, Cita>();
                config.CreateMap<Cita, CitaDTO>();
                config.CreateMap<DiagnosticoDTO, Diagnostico>();
                config.CreateMap<Diagnostico, DiagnosticoDTO>();
            });

            return mappingConfig;
        }
    }
}
