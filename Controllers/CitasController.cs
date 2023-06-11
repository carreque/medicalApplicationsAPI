using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using medicalAppointmentsAPI.Repositories.Implements;
using medicalAppointmentsApplication.Models;
using medicalAppointmentsAPI.Models.DTO;
using medicalAppointmentsAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace medicalAppointmentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitasController : ControllerBase
    {
        private readonly CitaService _citaService;
        protected ResponseDTO _response;

        public CitasController(CitaService citaService)
        {
            _citaService = citaService;
            _response = new ResponseDTO();
        }

        // GET: api/Citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cita>>> GetAppointment()
        {
            try
            {
                var lista = await _citaService.GetAllAppointments();
                _response.Result = lista;
                _response.MessageDisplay = "Lista de Citas obtenidas";
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_response);
        }

        // GET: api/Citas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetCita(int id)
        {
            var cita = await _citaService.GetAppointmentById(id);

            if (cita == null)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Cita inexistente";
                return NotFound(_response);
            }

            _response.Result = cita;
            _response.MessageDisplay = "Informacion de la cita";
            return Ok(_response);
        }

        [HttpGet("/api/CitasUsuario/{patient_id}")]
        public async Task<ActionResult<Cita>> GetPatientAppointements(int patient_id)
        {
            var citas = await _citaService.GetPatientAppointments(patient_id);

            if (citas == null)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "No existen citas asociadas al usuario";
                return NotFound(_response);
            }

            _response.Result = citas;
            _response.MessageDisplay = "Listado de citas encontrado";
            return Ok(_response);
        }

        [HttpGet("/api/CitasUsuarioSinDiagnostico")]
        public async Task<ActionResult<Cita>> GetAppointementsWithoutDiagnosis()
        {
            var citas = await _citaService.GetAppointemntsWithoutDiagnosis();

            if (citas == null)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "No existen citas asociadas al usuario";
                return NotFound(_response);
            }

            _response.Result = citas;
            _response.MessageDisplay = "Listado de citas encontrado";
            return Ok(_response);
        }
        // PUT: api/Citas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, CitaDTO citaDTO)
        {
            try
            {
                CitaDTO citaModel = await _citaService.UpdateAppointment(citaDTO);
                _response.Result = citaModel;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Error al actualizar la cita";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/Citas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cita>> PostCita(CitaDTO citaDTO)
        {
            try
            {
                CitaDTO citaModel = await _citaService.CreateAppointment(citaDTO);
                _response.Result = citaModel;
                return CreatedAtAction("GetCita", new { id = citaModel.Id }, citaModel);

            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Error al actualizar la lista";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/Citas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            try
            {
                bool isDeleted = await _citaService.DeleteAppointment(id);

                if (isDeleted)
                {
                    _response.Result = isDeleted;
                    _response.MessageDisplay = "Cita Eliminada con exito";
                    return Ok(_response);
                }

                _response.isSuccess = false;
                _response.MessageDisplay = "Se produjo un error al eliminar la cita";
                return BadRequest(_response);

            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Error al eliminar la cita";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }

        }
        //DeleteAppointmentsFromUser
        [HttpDelete("/api/CitasFromUser/{userId}")]
        public async Task<IActionResult> DeleteAppointmentsFromUser(int userId)
        {
            try
            {
                bool isDeleted = await _citaService.DeleteAppointmentsFromUser(userId);

                if (isDeleted)
                {
                    _response.Result = isDeleted;
                    _response.MessageDisplay = "Citas Eliminadas con exito";
                    return Ok(_response);
                }

                _response.isSuccess = false;
                _response.MessageDisplay = "Se produjo un error al eliminar las citas";
                return BadRequest(_response);

            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.MessageDisplay = "Error al eliminar las citas";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }

        }
    }
}   
