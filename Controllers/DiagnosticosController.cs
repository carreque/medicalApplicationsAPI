using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using medicalAppointmentsApplication.ContextApplication;
using medicalAppointmentsApplication.Models;
using medicalAppointmentsAPI.Services;
using medicalAppointmentsAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace medicalAppointmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiagnosticosController : ControllerBase
    {
        private readonly DiagnosticoService _diagnosticoService;
        protected ResponseDTO _response;

        public DiagnosticosController(DiagnosticoService diagnosticoService)
        {
            _diagnosticoService = diagnosticoService;
            _response = new ResponseDTO();
        }

        // GET: api/Diagnosticos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diagnostico>>> GetDiagnosis()
        {
            try
            {
                var lista = await _diagnosticoService.GetAllDiagnosticos();
                _response.Result = lista;
                _response.MessageDisplay = "Lista de Diagnosticos obtenidas";
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_response);
        }

        // GET: api/Diagnosticos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diagnostico>> GetDiagnostico(int id)
        {
            var diagnostico = await _diagnosticoService.GetDiagnosticoById(id);

            if (diagnostico == null)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Diagnostico inexistente";
                return NotFound(_response);
            }

            _response.Result = diagnostico;
            _response.MessageDisplay = "Informacion del diagnostico";
            return Ok(_response);
        }

        [HttpGet("/api/GetDiagnosticoFromCita/{idCita}")]
        public async Task<ActionResult<Diagnostico>> GetDiagnosticoFromAppointment(int idCita)
        {
            var diagnostico = await _diagnosticoService.GetADiagnosticoFromAppointment(idCita);

            if (diagnostico == null)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Diagnostico inexistente";
                return NotFound(_response);
            }

            _response.Result = diagnostico;
            _response.MessageDisplay = "Informacion del diagnostico";
            return Ok(_response);
        }

        // PUT: api/Diagnosticos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiagnostico(int id, DiagnosticoDTO diagnosticoDTO)
        {
            try
            {
                DiagnosticoDTO diagnosticoModel = await _diagnosticoService.UpdateDiagnostico(diagnosticoDTO);
                _response.Result = diagnosticoModel;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Error al actualizar el diagnostico";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/Diagnosticos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diagnostico>> PostDiagnostico(DiagnosticoDTO diagnosticoDTO)
        {
            try
            {
                DiagnosticoDTO diagnosticoModel = await _diagnosticoService.CreateDiagnostico(diagnosticoDTO);
                _response.Result = diagnosticoModel;
                return CreatedAtAction("GetDiagnostico", new { id = diagnosticoModel.Id }, diagnosticoModel);

            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Error al actualizar el diagnostico";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/Diagnosticos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnostico(int id)
        {
            try
            {
                bool isDeleted = await _diagnosticoService.DeleteDiagnostico(id);

                if (isDeleted)
                {
                    _response.Result = isDeleted;
                    _response.MessageDisplay = "Diagnostico Eliminado con exito";
                    return Ok(_response);
                }

                _response.isSuccess = false;
                _response.MessageDisplay = "Se produjo un error al eliminar el diagnostico";
                return BadRequest(_response);

            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Error al eliminar el diagnostico";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }


    }
}
