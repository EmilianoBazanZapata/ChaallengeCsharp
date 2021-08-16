using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class Genero
    {
        [Key]
        public int Id{get;set;}
        public string Nombre{get;set;}    
        public string imagen {get;set;}    
        public Boolean Activo{get;set;}

        public virtual ICollection<Pelicula> Peliculas {get;set;}
    }
}