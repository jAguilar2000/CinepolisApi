using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaController(ICategoriaRepository cinesRepository)
        {
            _categoriaRepository = cinesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _categoriaRepository.Gets();
            var response = new ApiResponse<IEnumerable<Categoria>>(result);
            return Ok(response);
        }

        [HttpGet("{categoriaId}")]
        public async Task<IActionResult> Get(int categoriaId)
        {
            var result = await _categoriaRepository.Get(categoriaId);
            var response = new ApiResponse<Categoria>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Categoria categoria)
        {
            await _categoriaRepository.Insert(categoria);
            var response = new ApiResponse<Categoria>(categoria);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Categoria categoria)
        {
            var result = await _categoriaRepository.Edit(categoria);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
