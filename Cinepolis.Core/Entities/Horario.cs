using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinepolis.Core.Entities
{
    public class Horario
    {
        [Key]
        public int horarioId { get; set; }
        public DateTime horaInicio { get; set; }
        public DateTime horaFinal { get; set; }
        public int peliculaId { get; set; }
        public int salaId { get; set; }
        public int tipoProyeccionId { get; set; }
        public bool activo { get; set; }
        [ForeignKey("salaId")]
        public virtual Salas? Sala { get; set; }
        [ForeignKey("tipoProyeccionId")]
        public virtual TipoProyeccion? TipoProyeccion { get; set; }
    }
}
