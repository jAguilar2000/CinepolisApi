using Cinepolis.Api.Responses;
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

        [HttpPost]
        public async Task<IActionResult> Post(VentaViewModels venta)
        {
            await _ventasRepository.InsertVenta(venta);
            var response = new ApiResponse<VentaViewModels>(venta);
            return Ok(response);
        }
    }
}
