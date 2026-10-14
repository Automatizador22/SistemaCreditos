using SistemaCreditos.Application.Interfaces.ExternalServices;
using Microsoft.Extensions.Logging; // Para imprimir en la consola

namespace SistemaCreditos.Infrastructure.ExternalServices
{
    public class FakeEmailService : IEmailService
    {
        private readonly ILogger<FakeEmailService> _logger;

        public FakeEmailService(ILogger<FakeEmailService> logger)
        {
            _logger = logger;
        }

        public Task EnviarEmailAsync(string destinatario, string asunto, string cuerpoHtml)
        {
            _logger.LogInformation("================================================");
            _logger.LogInformation($"SIMULANDO ENVÍO DE CORREO");
            _logger.LogInformation($"DESTINATARIO : {destinatario}");
            _logger.LogInformation($"ASUNTO       : {asunto}");
            _logger.LogInformation($"MENSAJE      : {cuerpoHtml}");
            _logger.LogInformation("================================================");

            return Task.CompletedTask;
        }
    }
}