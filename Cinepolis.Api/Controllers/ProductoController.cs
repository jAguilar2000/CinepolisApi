using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;
        public ProductoController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _productoRepository.Gets();
            var response = new ApiResponse<IEnumerable<Producto>>(result);
            return Ok(response);
        }

        [HttpGet("{productoId}")]
        public async Task<IActionResult> Get(int productoId)
        {
            var result = await _productoRepository.Get(productoId);
            var response = new ApiResponse<Producto>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Producto producto)
        {
            await _productoRepository.Insert(producto);
            var response = new ApiResponse<Producto>(producto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Producto producto)
        {
            var result = await _productoRepository.Edit(producto);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpPut("UploadImage")]
        public async Task<IActionResult> UploadImage(ImagenFileViewModel img)
        {
            var result = await _productoRepository.UploadImage(img);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpPut("UploadFotoBase64")]
        public async Task<IActionResult> UploadFotoBase64(ImagenViewModel img)
        {
            var result = await _productoRepository.UploadFotoBase64(img);
            var response = new ApiResponse<string>(result);
            return Ok(response);
        }
    }
}
