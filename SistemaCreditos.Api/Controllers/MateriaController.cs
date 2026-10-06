using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Application.DTOs.Materias;
using SistemaCreditos.Application.UseCases.Materias;

namespace SistemaCreditos.Api.Controllers
{
    [ApiController]
    [Route("api/materia")]
    [Authorize(Roles = "ADMIN")]
    public class MateriaController : ControllerBase
    {
        private readonly RegistrarMateriaUC _registrarMateriaUC;
        public MateriaController(RegistrarMateriaUC registrarMateriaUC)
        {
            _registrarMateriaUC = registrarMateriaUC;
        }

        [HttpPost("registrar-materia")]
        public async Task<IActionResult> RegistrarMateria([FromBody] RegistrarMateriaRequestDto dto)
        {
            var resultado = await _registrarMateriaUC.EjecutarAsync(dto);
            return Ok(resultado);
        }
    }
}
