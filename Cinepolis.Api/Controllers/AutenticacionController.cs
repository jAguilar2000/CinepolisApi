using Cinepolis.Api.Responses;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAutenticacionRepository _autenticacionRepository;
        public AutenticacionController(IAutenticacionRepository autenticacionRepository)
        {
            _autenticacionRepository = autenticacionRepository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] Credenciales credenciales)
        {
            var result = await _autenticacionRepository.Login(credenciales);
            var response = new ApiResponse<AutenticacionResponse>(result);

            if (result.Status == 200)
                return Ok(response);

            return Unauthorized(result);
        }

        [HttpPost]
        [Route("EnviarClaveTemporal")]
        public async Task<ActionResult> EnviarClaveTemporal(string usuario)
        {
            var result = await _autenticacionRepository.EnviarClaveTemporal(usuario);
            var response = new ApiResponse<ClaveTemporalResponse>(result);
            return Ok(response);
        }

        [HttpPut]
        [Route("VerificarUsuario")]
        public async Task<ActionResult> VerificarUsuario(int usuarioId)
        {
            var result = await _autenticacionRepository.UsuarioVerificado(usuarioId);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpPut]
        [Route("ReestablecerPassword")]
        public async Task<ActionResult> ReestablecerPassword(string password, int userId)
        {
            var result = await _autenticacionRepository.ReestablecerPassword(password, userId);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
