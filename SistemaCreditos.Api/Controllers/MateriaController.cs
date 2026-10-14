using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Application.DTOs.Materias;
using SistemaCreditos.Application.UseCases.Materias;

namespace SistemaCreditos.Api.Controllers
{
    [ApiController]
    [Route("api/materia")]
    
    public class MateriaController : ControllerBase
    {
        private readonly RegistrarMateriaUC _registrarMateriaUC;
        private readonly ObtenerMateriasDisponiblesUC _obtenerMateriasDisponiblesUC;
        public MateriaController(RegistrarMateriaUC registrarMateriaUC, ObtenerMateriasDisponiblesUC obtenerMateriasDisponiblesUC)
        {
            _registrarMateriaUC = registrarMateriaUC;
            _obtenerMateriasDisponiblesUC = obtenerMateriasDisponiblesUC;
        }

        [HttpPost("RegistrarMateria")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> RegistrarMateria([FromBody] RegistrarMateriaRequestDto dto)
        {
            var resultado = await _registrarMateriaUC.EjecutarAsync(dto);
            return Ok(resultado);
        }

        [HttpGet("ObtenerMateriasDisponibles")]
        [Authorize]
        public async Task<IActionResult> ObtenerMateriasDisponibles()
        {
            var resultado = await _obtenerMateriasDisponiblesUC.EjecutarAsync();
            return Ok(resultado); 
        }
    }
}
