using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Cinepolis.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly IHorarioRepository _horarioRepository;
        public HorarioController(IHorarioRepository horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _horarioRepository.Gets();
            var response = new ApiResponse<IEnumerable<Horarios>>(result);
            return Ok(response);
        }

        [HttpGet("{horarioId}")]
        public async Task<IActionResult> Get(int horarioId)
        {
            var result = await _horarioRepository.Get(horarioId);
            var response = new ApiResponse<Horarios>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Horarios horario)
        {
            await _horarioRepository.Insert(horario);
            var response = new ApiResponse<Horarios>(horario);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Horarios horario)
        {
            var result = await _horarioRepository.Edit(horario);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpGet]
        [Route("asientosOcupados")]
        public async Task<IActionResult> Gets(int horarioId)
        {
            var result = await _horarioRepository.GetAsientosOcupados(horarioId);
            var response = new ApiResponse<IEnumerable<SP_AsientosOcupados>>(result);
            return Ok(response);
        }
    }
}
