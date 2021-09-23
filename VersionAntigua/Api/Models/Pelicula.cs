using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class Pelicula
    {
        [Key]
        public int Id{get;set;}
        public string Titulo{get;set;}
        public DateTime FechaCreacion{get;set;}
        public int Calificacion {get;set;}
        public string imagen {get;set;}
        public Boolean Activo{get;set;}

        public virtual ICollection<Personaje> Personajes{get;set;}
    }
}