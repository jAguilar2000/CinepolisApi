using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Cinepolis.Infrastructure.Repositories;
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

        [HttpGet("{tipoProyeccionId}")]
        public async Task<IActionResult> Get(int tipoProyeccionId)
        {
            var result = await _proyeccionRepository.Get(tipoProyeccionId);
            var response = new ApiResponse<TipoProyeccion>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TipoProyeccion tipoProyect)
        {
            await _proyeccionRepository.Insert(tipoProyect);
            var response = new ApiResponse<TipoProyeccion>(tipoProyect);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(TipoProyeccion tipoProyect)
        {
            var result = await _proyeccionRepository.Edit(tipoProyect);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
