using System.ComponentModel.DataAnnotations.Schema;

namespace Cinepolis.Core.Entities
{
    public class Precio
    {
        public int precioId { get; set; }
        public int categoriaId { get; set; }
        public int tipoProyeccionId { get; set; }
        public decimal monto { get; set; }
        public bool activo { get; set; }
        [ForeignKey("tipoProyeccionId")]
        public virtual TipoProyeccion? TipoProyeccion { get; set; }
    }
}
