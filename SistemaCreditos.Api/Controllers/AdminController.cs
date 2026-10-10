using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Application.DTOs.Profesores;
using SistemaCreditos.Application.DTOs.Usuarios;
using SistemaCreditos.Application.UseCases.Profesores;
using SistemaCreditos.Application.UseCases.Usuarios;

namespace SistemaCreditos.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize(Roles = "ADMIN")]
    public class AdminController : ControllerBase
    {
        private readonly ObtenerSolicitudesPendientesUC _obtenerSolicitudesPendientesUC;
        private readonly ProcesarSolicitudProfesorUC _procesarSolicitudProcesorUC;
        private readonly ActualizarUsuarioAdminUC _actualizarUsuarioAdminUC;

        public AdminController(
            ObtenerSolicitudesPendientesUC obtenerSolicitudesPendientesUC,
            ProcesarSolicitudProfesorUC procesarSolicitudProfesorUC,
            ActualizarUsuarioAdminUC actualizarUsuarioAdminUC)
        {
            _obtenerSolicitudesPendientesUC = obtenerSolicitudesPendientesUC;
            _procesarSolicitudProcesorUC = procesarSolicitudProfesorUC;
            _actualizarUsuarioAdminUC = actualizarUsuarioAdminUC;
        }

        [HttpGet("SolicitudesPendientes")]
        public async Task<IActionResult> ObtenerSolicitudesPendientes()
        {
            var solicitudes = await _obtenerSolicitudesPendientesUC.EjecutarAsync();
            return Ok(solicitudes);
        }

        [HttpPut("ProcesarSolicitud")]
        public async Task<IActionResult> ProcesarSolicitud([FromBody] ProcesarSolicitudRequestDto dto) 
        { 
            await _procesarSolicitudProcesorUC.EjecutarAsync(dto);

            string procesar = dto.Aprobar ? "aprobado" : "rechazado";
            return Ok(new {mensaje = $"El profesor ha sido {procesar} exitosamente. " });
        }

        [HttpPut("usuarios/{idUsuario}")]
        public async Task<IActionResult> ActualizarUsuarioPorAdmin(int idUsuario, [FromBody] ActualizarUsuarioRequestDto dto)
        {
            await _actualizarUsuarioAdminUC.EjecutarAsync(idUsuario, dto);
            return Ok(new { mensaje = "El perfil del usuario ha sido actualizado exitosamente por el administrador." });
        }
    }
}
