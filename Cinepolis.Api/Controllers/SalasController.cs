using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private readonly ISalasRepository _salasRepository;
        public SalasController(ISalasRepository salasRepository)
        {
            _salasRepository = salasRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _salasRepository.Gets();
            var response = new ApiResponse<IEnumerable<Salas>>(result);
            return Ok(response);
        }

        [HttpGet("{cineId}")]
        public async Task<IActionResult> Get(int salaId)
        {
            var result = await _salasRepository.Get(salaId);
            var response = new ApiResponse<Salas>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Salas sala)
        {
            await _salasRepository.Insert(sala);
            var response = new ApiResponse<Salas>(sala);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Salas sala)
        {
            var result = await _salasRepository.Edit(sala);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
