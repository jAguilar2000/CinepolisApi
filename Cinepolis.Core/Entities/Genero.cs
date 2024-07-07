using System.ComponentModel.DataAnnotations;

namespace Cinepolis.Core.Entities
{
    public class Genero
    {
        [Key]
        public int generoId { get; set; }
        public string descripcion { get; set; } = string.Empty;
        public bool activo { get; set; }
    }
}
