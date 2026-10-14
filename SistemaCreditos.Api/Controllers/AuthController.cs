using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Application.DTOs.Auth;
using SistemaCreditos.Application.DTOs.Usuarios;
using SistemaCreditos.Application.UseCases.Usuarios;

namespace SistemaCreditos.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly RegistrarUsuarioUC _registrarUsuarioUseCase;
        private readonly LoginUC _loginUC;
        public AuthController(RegistrarUsuarioUC registrarUsuarioUseCase, LoginUC loginUC)
        {
            _registrarUsuarioUseCase = registrarUsuarioUseCase;
            _loginUC = loginUC; 
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var resultado = await _loginUC.EjecutarAsync(dto);
            return Ok(resultado);
        }
        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioRequestDto dto)
        {
            var resultado = await _registrarUsuarioUseCase.EjecutarAsync(dto);
            return Ok(resultado);
        }      
        [Authorize]
        [HttpGet("perfil-seguro")]
        public IActionResult PerfilSeguro()
        {
            return Ok(new { Mensaje = $"¡Bienvenido al Sistema Creditos, {User.Identity?.Name}!" });
        }
    }
}
