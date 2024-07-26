using System.ComponentModel.DataAnnotations.Schema;

namespace Cinepolis.Core.ViewModels
{
    public class VentaViewModels
    {
        public int ventaId { get; set; }
        public int usuarioId { get; set; }
        public int horarioId { get; set; }
        public List<VentaEntradaDetalleViewModels?> VentaEntrada {  get; set; }
        public List<VentaProductoDetalleViewModels?> VentaProducto {  get; set; }
    }

    public class VentaEntradaDetalleViewModels
    {
        public int ventaDetalleId { get; set; }
        public string numeroBoleto { get; set; } = string.Empty;
        public decimal precio { get; set; }
        public decimal cantidad { get; set; }
        [NotMapped]
        public decimal totalEntrada { get { return cantidad * precio; } }
    }

    public class VentaProductoDetalleViewModels
    {
        public int ventaDetalleId { get; set; }
        public int productoId { get; set; }
        public decimal precio { get; set; }
        public decimal cantidad { get; set; }
        [NotMapped]
        public decimal totalProducto { get { return cantidad * precio; } }
    }
}
