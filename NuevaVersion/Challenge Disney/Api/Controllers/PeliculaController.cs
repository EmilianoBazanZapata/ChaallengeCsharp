using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class PeliculaController : ControllerBase
    {
        private readonly DisneyContext _db;
        public PeliculaController(DisneyContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("Movies")]
        public async Task<IActionResult> GetPeliculas()
        {
            //ordeno las Peliculas por nombre
            var lista = await _db.Peliculas.Where(p => p.Activo == true).OrderBy(p => p.Titulo).ToListAsync();
            return Ok(lista);
        }
        [HttpPost]
        [Route("Movie/AddMovie")]
        public async Task<IActionResult> AgregarPelicula([FromBody] Pelicula pelicula)
        {
            //diferentes validaciones
            if (pelicula == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //si todo sale bien podre agregar una pelicula
            await _db.AddAsync(pelicula);
            //por ultimo gurado los cambios
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("Movie/UpdateMovie")]
        public async Task<IActionResult> ActualizarPelicula([FromBody] Pelicula pelicula)
        {
            //selecciono la pelicula que tenga el mismo id
            var query = (from Pel in _db.Peliculas
                         where Pel.Id == pelicula.Id
                         select Pel).FirstOrDefault();
            if(query == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //una vez encontrado alcualizo los datos que deseo
            query.Titulo = pelicula.Titulo;
            query.FechaCreacion = pelicula.FechaCreacion;
            query.Calificacion = pelicula.Calificacion;
            query.imagen = pelicula.imagen;
            //guardo los cambios
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("Movie/DeleteMovie")]
        public async Task<IActionResult> EliminarPelicula(int id)
        {
            //selecciono la pelicula que tenga el mismo id
            var query = (from Pel in _db.Peliculas
                         where Pel.Id == id
                         select Pel).FirstOrDefault();
            if(query == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //una vez encontrado alcualizo los datos que deseo
            query.Activo = false;
            //guardo los cambios
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}