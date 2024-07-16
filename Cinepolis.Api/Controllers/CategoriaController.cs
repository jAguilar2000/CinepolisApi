using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Microsoft.AspNetCore.Http;
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
    }
}
