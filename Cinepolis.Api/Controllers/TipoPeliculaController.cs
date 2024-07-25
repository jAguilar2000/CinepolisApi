using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPeliculaController : ControllerBase
    {
        private readonly ITipoPeliculaRepository _tipoPeliculaRepository;
        public TipoPeliculaController(ITipoPeliculaRepository tipoPeluculaRepository)
        {
            _tipoPeliculaRepository = tipoPeluculaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _tipoPeliculaRepository.Gets();
            var response = new ApiResponse<IEnumerable<TipoPelicula>>(result);
            return Ok(response);
        }
    }
}
