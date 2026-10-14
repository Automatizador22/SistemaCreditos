using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Application.DTOs.Usuarios;
using SistemaCreditos.Application.UseCases.Usuarios;
using System.Security.Claims;

namespace SistemaCreditos.Api.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly ActualizarDatosUsuarioUC _actualizarMisDatosUC;

        public UsuarioController(ActualizarDatosUsuarioUC actualizarMisDatosUC)
        {
            _actualizarMisDatosUC = actualizarMisDatosUC;
        }

        [HttpPut("ActualizarDatosUsuario")]
        public async Task<IActionResult> ActualizarDatosUsuario([FromBody] ActualizarDatosUsuarioRequestDto dto)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idClaim) || !int.TryParse(idClaim, out int idUsuario))
            {
                return Unauthorized(new { error = "Token inválido o corrupto." });
            }
            await _actualizarMisDatosUC.EjecutarAsync(idUsuario, dto);
            return Ok(new { mensaje = "Tus datos han sido actualizados correctamente." });
        }
    }
}