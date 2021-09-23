using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Api.Models
{
    public class Personaje
    {
        [Key]
        public int Id{get;set;}
        [Required]
        public string Nombre{get;set;}
        [Required]
        public int Edad{get;set;}
        [Required]
        public float Peso {get;set;}
        [Required]
        public string Historia {get;set;}
        public string imagen {get;set;}
        [Required]
        public Boolean Activo{get;set;}

        public virtual ICollection<Pelicula> Peliculas {get;set;}
    }
}