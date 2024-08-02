using Cinepolis.Core.Entities;
using Cinepolis.Core.ViewModels;

namespace Cinepolis.Core.Interface
{
    public interface IVentasRepository
    {
        Task<IEnumerable<Venta>> Gets(int? userId);
        Task<IEnumerable<Venta>> GetsById(int? ventaId);
        Task InsertVenta(VentaViewModels venta);
    }
}
