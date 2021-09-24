using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace Api.Models
{
    public class PeliculaPorPersonajes
    {
        [Key]
        public int IdPeliculaPorPersonaje { get; set; }
        public int IdPersonaje { get; set; }
        public int IdPelicula { get; set; }

        [ForeignKey("IdPelicula")]
        public virtual ICollection<Pelicula> Peliculas { get; set; }
        [ForeignKey("IdPersonaje")]
        public virtual ICollection<Personaje> PPesonajes { get; set; }
    }
}