

namespace SistemaCreditos.Application.Interfaces.ExternalServices
    {
        public interface IEmailService
        {
            Task EnviarEmailAsync(string destinatariom, string asunto, string cuerpoHtml);
        }
    }
