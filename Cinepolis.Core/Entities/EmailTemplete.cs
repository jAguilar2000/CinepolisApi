using System.ComponentModel.DataAnnotations;

namespace Cinepolis.Core.Entities
{
    public class EmailTemplete
    {
        [Key]
        public int plantillaId { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
    }
}
