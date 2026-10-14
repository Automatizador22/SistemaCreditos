using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Application.DTOs.Inscripciones;
using SistemaCreditos.Application.UseCases.Estudiantes;
using System.Security.Claims;

namespace SistemaCreditos.Api.Controllers
{
    [ApiController]
    [Route("api/estudiante")]
    [Authorize(Roles = "ESTUDIANTE")]
    public class EstudianteController : ControllerBase
    {
        private readonly InscribirMateriasUC _inscribirMateriasUC;
        private readonly VerCompañerosClaseUC _verCompañerosClaseUC;
        private readonly CancelarInscripcionUC _cancelarInscripcionUC;
        private readonly VerPerfilEstudianteUC _verPerfilEstudianteUC;
        public EstudianteController(
            InscribirMateriasUC inscribirMateriasUC, 
            VerCompañerosClaseUC verCompañerosClaseUC, 
            CancelarInscripcionUC cancelarInscripcionUC,
            VerPerfilEstudianteUC verPerfilEstudianteUC)
        {
            _inscribirMateriasUC = inscribirMateriasUC;
            _verCompañerosClaseUC = verCompañerosClaseUC;
            _cancelarInscripcionUC = cancelarInscripcionUC;
            _verPerfilEstudianteUC = verPerfilEstudianteUC;
        }

        [HttpPost("InscribirMateria")]
        public async Task<IActionResult> InscribirMateria([FromBody] InscribirMateriaRequestDto dto)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idClaim) || !int.TryParse(idClaim, out int idUsuario))
            {
                return Unauthorized(new { error = "Token inválido o corrupto." });
            }
            var resultado = await _inscribirMateriasUC.EjecutarAsync(idUsuario, dto);
            return Ok(resultado);
        }

        [HttpGet("VerCompañerosClase")]
        public async Task<IActionResult> VerCompañerosClase()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idClaim) || !int.TryParse(idClaim, out int idUsuario))
            {
                return Unauthorized(new { error = "Token inválido o corrupto." });
            }

            var resultado = await _verCompañerosClaseUC.EjecutarAsync(idUsuario);
            return Ok(resultado);
        }
        [HttpDelete("CancelarMateria/{idMateria}")]
        public async Task<IActionResult> CancelarMateria(int idMateria)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idClaim) || !int.TryParse(idClaim, out int idUsuario))
            {
                return Unauthorized(new { error = "Token inválido o corrupto." });
            }
            var resultado = await _cancelarInscripcionUC.EjecutarAsync(idUsuario, idMateria);
            return Ok(new { mensaje = "Inscripción cancelada exitosamente." });
        }
        [HttpGet("ResumenEstudiante")]
        public async Task<IActionResult> ResumenEstudiante()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idClaim) || !int.TryParse(idClaim, out int idUsuario))
            {
                return Unauthorized(new { error = "Token inválido o corrupto." });
            }

            var resultado = await _verPerfilEstudianteUC.EjecutarAsync(idUsuario);
            return Ok(resultado);
        }

    }
}