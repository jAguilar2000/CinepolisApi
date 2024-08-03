using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosController(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _usuariosRepository.Gets();
            var response = new ApiResponse<IEnumerable<Usuario>>(result);
            return Ok(response);
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> Get(int usuarioId)
        {
            var result = await _usuariosRepository.Get(usuarioId);
            var response = new ApiResponse<Usuario>(result);
            return Ok(response);
        }

        [HttpGet]
        [Route("UsuarioById")]
        public async Task<IActionResult> UsuarioById(int usuarioId)
        {
            var result = await _usuariosRepository.GetUserById(usuarioId);
            var response = new ApiResponse<UsuariosViewModel>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuariosViewModel usuario)
        {
            var result = await _usuariosRepository.Insert(usuario);
            var response = new ApiResponse<Usuario>(result);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Usuario usuario)
        {
            var result = await _usuariosRepository.Edit(usuario);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpPut("UploadImage")]
        public async Task<IActionResult> UploadImage(ImagenFileViewModel img)
        {
            var result = await _usuariosRepository.UploadImage(img);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpPut("UploadFotoBase64")]
        public async Task<IActionResult> UploadFotoBase64(ImagenViewModel img)
        {
            var result = await _usuariosRepository.UploadFotoBase64(img);
            var response = new ApiResponse<string>(result);
            return Ok(response);
        }
    }
}
