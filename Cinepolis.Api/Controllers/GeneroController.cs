using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Cinepolis.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroRepository _generoRepository;
        public GeneroController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _generoRepository.Gets();
            var response = new ApiResponse<IEnumerable<Genero>>(result);
            return Ok(response);
        }

        [HttpGet("{generoId}")]
        public async Task<IActionResult> Get(int generoId)
        {
            var result = await _generoRepository.Get(generoId);
            var response = new ApiResponse<Genero>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Genero genero)
        {
            await _generoRepository.Insert(genero);
            var response = new ApiResponse<Genero>(genero);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Genero genero)
        {
            var result = await _generoRepository.Edit(genero);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
