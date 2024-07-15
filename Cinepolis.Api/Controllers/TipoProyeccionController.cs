using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProyeccionController : ControllerBase
    {
        private readonly ITipoProyeccionRepository _proyeccionRepository;
        public TipoProyeccionController(ITipoProyeccionRepository proyeccionRepository)
        {
            _proyeccionRepository = proyeccionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _proyeccionRepository.Gets();
            var response = new ApiResponse<IEnumerable<TipoProyeccion>>(result);
            return Ok(response);
        }
    }
}
