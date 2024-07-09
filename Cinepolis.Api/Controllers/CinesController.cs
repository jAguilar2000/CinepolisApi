using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinesController : ControllerBase
    {
        private readonly ICinesRepository _cinesRepository;
        public CinesController(ICinesRepository cinesRepository)
        {
            _cinesRepository = cinesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _cinesRepository.Gets();
            var response = new ApiResponse<IEnumerable<Cines>>(result);
            return Ok(response);
        }

        [HttpGet("{cineId}")]
        public async Task<IActionResult> Get(int cineId)
        {
            var result = await _cinesRepository.Get(cineId);
            var response = new ApiResponse<Cines>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cines cine)
        {
            await _cinesRepository.Insert(cine);
            var response = new ApiResponse<Cines>(cine);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Cines cine)
        {
            var result = await _cinesRepository.Edit(cine);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpPut("UploadImage")]
        public async Task<IActionResult> UploadImage(ImagenFileViewModel img)
        {
            var result = await _cinesRepository.UploadImage(img);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpPut("UploadFotoBase64")]
        public async Task<IActionResult> UploadFotoBase64(ImagenViewModel img)
        {
            var result = await _cinesRepository.UploadFotoBase64(img);
            var response = new ApiResponse<string>(result);
            return Ok(response);
        }
    }
}
