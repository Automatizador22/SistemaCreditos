using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Application.Interfaces.Persistence;
using SistemaCreditos.Application.UseCases.Profesores;
using System.Security.Claims;

namespace SistemaCreditos.Api.Controllers
{
    [ApiController]
    [Route("api/estudiante")]
    [Authorize(Roles = "PROFESOR")]
    public class ProfesorController : ControllerBase
    {
        private readonly RepostularProfesorUC _repostularProfesorUC;

        public ProfesorController(RepostularProfesorUC repostularProfesorUC)
        {
            _repostularProfesorUC = repostularProfesorUC;
        }

        [HttpPost("repostular")]
        public async Task<IActionResult> Repostular()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idClaim) || !int.TryParse(idClaim, out int idUsuario))
            {
                return Unauthorized(new { error = "Token inválido o corrupto." });
            }
            await _repostularProfesorUC.EjecutarAsync(idUsuario);
            return Ok(new { mensaje = "Te has repostulado exitosamente. Tu solicitud está en revisión." });
        }

    }
}
