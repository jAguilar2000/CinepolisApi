using System.ComponentModel.DataAnnotations;

namespace Cinepolis.Core.Entities
{
    public class TipoProyeccion
    {
        [Key]
        public int tipoProyeccionId { get; set; }
        public string descripcion { get; set; } = string.Empty;
        public bool activo { get; set; }
    }
}
