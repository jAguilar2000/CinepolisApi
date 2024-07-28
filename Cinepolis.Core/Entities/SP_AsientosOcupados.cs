using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinepolis.Core.Entities
{
    public class SP_AsientosOcupados
    {
        [Key]
        public string Asiento { get; set; } = string.Empty;
    }
}
