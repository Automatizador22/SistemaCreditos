using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Application.DTOs.Inscripciones;
using SistemaCreditos.Application.UseCases.Estudiantes;

namespace SistemaCreditos.Api.Controllers
{   
    [ApiController]
    [Route("api/estudiante")]
    [Authorize(Roles = "ESTUDIANTE")]

    public class EstudianteController : ControllerBase
    {
        private readonly InscribirMateriasUC _inscribirMateriasUC;
        public EstudianteController(InscribirMateriasUC inscribirMateriasUC)
        {

            _inscribirMateriasUC = inscribirMateriasUC;
        }
       
        [HttpPost("inscribir-materia")]
        public async Task<IActionResult> InscribirMateria([FromBody] InscribirMateriaRequestDto dto)
        {
                var resultado = await _inscribirMateriasUC.EjecutarAsync(dto);
                return Ok(resultado);            
        }
    }
}
