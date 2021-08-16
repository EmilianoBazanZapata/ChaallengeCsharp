using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class Personaje
    {
        public int Id{get;set;}
        public string Nombre{get;set;}
        public int Edad{get;set;}
        public float Peso {get;set;}
        public string Historia {get;set;}
        public string imagen {get;set;}
        public Boolean Activo{get;set;}

        public virtual ICollection<Pelicula> Peliculas {get;set;}
    }
}