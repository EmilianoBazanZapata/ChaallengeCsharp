using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Genero
    {
        [Key]
        public int Id{get;set;}
        [Required]
        public string Nombre{get;set;}    
        public string imagen {get;set;}   
        [Required] 
        public Boolean Activo{get;set;}    
        public virtual ICollection<Pelicula> Peliculas {get;set;}
    }
}