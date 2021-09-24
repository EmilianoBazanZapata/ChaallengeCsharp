using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data
{
    public class DisneyContext : DbContext
    {
        public DisneyContext(DbContextOptions<DisneyContext> option) : base(option)
        {

        }
        public DbSet<Personaje> Personajes {get;set;}
        public DbSet<Genero> Generos {get;set;}
        public DbSet<Pelicula> Peliculas {get;set;}
        public DbSet<Usuario> Usuarios {get;set;}
        public DbSet<PeliculaPorPersonajes> PeliculaPorPersonajes {get;set;}
    }
}