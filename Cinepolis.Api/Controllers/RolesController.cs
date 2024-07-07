using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;
        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _rolesRepository.Gets();
            var response = new ApiResponse<IEnumerable<Rol>>(result);
            return Ok(response);
        }

        [HttpGet("{rolId}")]
        public async Task<IActionResult> Get(int rolId)
        {
            var result = await _rolesRepository.Get(rolId);
            var response = new ApiResponse<Rol>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Rol rol)
        {
            await _rolesRepository.Insert(rol);
            var response = new ApiResponse<Rol>(rol);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Rol rol)
        {
            var result = await _rolesRepository.Edit(rol);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
