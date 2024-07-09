using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Cinepolis.Infrastructure.Data;
using Cinepolis.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private readonly IPeliculaRepository _peliculaRepository;
        public PeliculaController(IPeliculaRepository peliculaRepository)
        {
            _peliculaRepository = peliculaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _peliculaRepository.Gets();
            var response = new ApiResponse<IEnumerable<Pelicula>>(result);
            return Ok(response);
        }

        [HttpGet("{peliculaId}")]
        public async Task<IActionResult> Get(int peliculaId)
        {
            var result = await _peliculaRepository.Get(peliculaId);
            var response = new ApiResponse<Pelicula>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Pelicula pelicula)
        {
            await _peliculaRepository.Insert(pelicula);
            var response = new ApiResponse<Pelicula>(pelicula);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Pelicula pelicula)
        {
            var result = await _peliculaRepository.Edit(pelicula);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpPut("UploadImage")]
        public async Task<IActionResult> UploadImage(ImagenFileViewModel img)
        {
            var result = await _peliculaRepository.UploadImage(img);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpPut("UploadFotoBase64")]
        public async Task<IActionResult> UploadFotoBase64(ImagenViewModel img)
        {
            var result = await _peliculaRepository.UploadFotoBase64(img);
            var response = new ApiResponse<string>(result);
            return Ok(response);
        }
    }
}
