using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cinepolis.Core.Entities
{
    public class VentaProductoDetalle
    {
        [Key]
        public int ventaDetalleId { get; set; }
        public int ventaId { get; set; }
        public int? productoId { get; set; }
        public decimal precio { get; set; }
        public decimal cantidad { get; set; }
        [ForeignKey("ventaId")]
        [JsonIgnore]
        public virtual Venta? Venta { get; set; }
        [ForeignKey("productoId")]
        [JsonIgnore]
        public virtual Producto? Producto { get; set; }
    }
}
