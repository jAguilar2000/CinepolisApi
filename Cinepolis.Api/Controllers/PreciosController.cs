using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreciosController : ControllerBase
    {
        private readonly IPreciosRepository _preciosRepository;
        public PreciosController(IPreciosRepository preciosRepository)
        {
            _preciosRepository = preciosRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _preciosRepository.Gets();
            var response = new ApiResponse<IEnumerable<Precios>>(result);
            return Ok(response);
        }

        [HttpGet("{precioId}")]
        public async Task<IActionResult> Get(int precioId)
        {
            var result = await _preciosRepository.Get(precioId);
            var response = new ApiResponse<Precios>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Precios precio)
        {
            await _preciosRepository.Insert(precio);
            var response = new ApiResponse<Precios>(precio);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Precios precio)
        {
            var result = await _preciosRepository.Edit(precio);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
