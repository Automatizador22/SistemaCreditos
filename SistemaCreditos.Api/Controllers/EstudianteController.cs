using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Application.DTOs.Estudiantes;
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
        public EstudianteController(InscribirMateriasUC inscribirMateriasUC, VerCompañerosClaseUC verCompañerosClaseUC)
        {
            _inscribirMateriasUC = inscribirMateriasUC;
            _verCompañerosClaseUC = verCompañerosClaseUC;
        }

        [HttpPost("inscribir-materia")]
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

        [HttpGet("ver-companeros-clase")]
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
    }
}