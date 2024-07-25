﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinepolis.Core.Entities
{
    public class Pelicula
    {
        [Key]
        public int peliculaId { get; set; }
        public int generoId { get; set; }
        public int tipoPeliculaId { get; set; }
        public string titulo { get; set; } = string.Empty;
        public string sinopsis { get; set; } = string.Empty;
        public int hora { get; set; }
        public int minutos { get; set; }
        public DateTime fechaLanzamiento { get; set; }
        public string foto { get; set; } = string.Empty;
        public bool activo { get; set; }
        [NotMapped]
        public string imgBase64 { get; set; } = string.Empty;
        [ForeignKey("generoId")]
        public virtual Genero? Genero { get; set; }

        [ForeignKey("tipoPeliculaId")]
        public virtual TipoPelicula? TipoPelicula { get; set; }
    }
}
