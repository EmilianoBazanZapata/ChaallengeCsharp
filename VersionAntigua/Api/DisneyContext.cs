using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class DisneyContext : DbContext
    {
        public DisneyContext(DbContextOptions<DisneyContext> options ):base(options)
        {

        }
        public DbSet<Personaje> Personajes {get;set;}
        public DbSet<Genero> Generos {get;set;}
        public DbSet<Pelicula> Peliculas {get;set;}
        public DbSet<Usuario> Usuarios {get;set;}
    }
}