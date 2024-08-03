using Cinepolis.Api.Responses;
using Cinepolis.Core.Entities;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cinepolis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVentasRepository _ventasRepository;
        public VentasController(IVentasRepository ventasRepository)
        {
            _ventasRepository = ventasRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Gets(int? userId)
        {
            var result = await _ventasRepository.Gets(userId);
            var response = new ApiResponse<IEnumerable<Venta>>(result);
            return Ok(response);
        }

        [HttpGet]
        [Route("resumen")]
        public async Task<IActionResult> GetsResumen(int? userId)
        {
            var result = await _ventasRepository.GetsResumen(userId);
            var response = new ApiResponse<IEnumerable<VentasResumenViewModel>>(result);
            return Ok(response);
        }

        [HttpGet]
        [Route("VentaById")]
        public async Task<IActionResult> GetById(int? ventaId)
        {
            var result = await _ventasRepository.GetsById(ventaId);
            var response = new ApiResponse<VentasHeaderViewModel>(result);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(VentaViewModels venta)
        {
            await _ventasRepository.InsertVenta(venta);
            var response = new ApiResponse<VentaViewModels>(venta);
            return Ok(response);
        }
    }
}
