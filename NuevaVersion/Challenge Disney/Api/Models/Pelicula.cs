using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public int Calificacion { get; set; }
        [Required]
        public string imagen { get; set; }
        [Required]
        public int IdGenero { get; set; }
        [Required]
        public Boolean Activo { get; set; }
        public virtual ICollection<Personaje> Personajes { get; set; }
        [ForeignKey("IdGenero")]
        public virtual ICollection<Genero> Generos { get; set; }
    }
}