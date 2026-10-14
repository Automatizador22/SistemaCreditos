using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SistemaCreditos.Api.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;

        public HealthController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHealth()
        {
            HealthReport report = await _healthCheckService.CheckHealthAsync();
            var response = new
            {
                status = report.Status.ToString(),
                tiempoRespuesta = report.TotalDuration.TotalMilliseconds + " ms",
                componentes = report.Entries.Select(e => new
                {
                    nombre = e.Key,
                    estado = e.Value.Status.ToString(),
                    descripcion = e.Value.Description ?? "OK"
                })
            };
            return report.Status == HealthStatus.Healthy ? Ok(response) : StatusCode(503, response);
        }
    }
}