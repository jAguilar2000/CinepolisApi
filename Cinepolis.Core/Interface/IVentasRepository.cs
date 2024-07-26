using Cinepolis.Core.ViewModels;

namespace Cinepolis.Core.Interface
{
    public interface IVentasRepository
    {
        Task InsertVenta(VentaViewModels venta);
    }
}
