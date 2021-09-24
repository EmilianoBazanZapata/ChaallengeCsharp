using System;
using System.Collections.Generic;
using System.IO;
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
        [Route("Movies/Movies")]
        public async Task<IActionResult> GetPeliculas()
        {
            var query = (from Pel in _db.Peliculas
                        select new {Pel.Titulo , Pel.imagen});
            var lista = await query.ToListAsync();
            return Ok(lista);
        }
        [HttpGet]
        [Route("Movies/ListMoviesDesc")]
        public async Task<IActionResult> GetPeliculasDesc()
        {
            //ordeno las Peliculas por nombre
            var lista = await _db.Peliculas.Where(p => p.Activo == true).OrderByDescending(p => p.FechaCreacion).ToListAsync();
            return Ok(lista);
        }
        [HttpGet]
        [Route("Movies/ListMoviesAsc")]
        public async Task<IActionResult> GetPeliculasAsc()
        {
            //ordeno las Peliculas por nombre
            var lista = await _db.Peliculas.Where(p => p.Activo == true).OrderBy(p => p.FechaCreacion).ToListAsync();
            return Ok(lista);
        }
        [HttpGet]
        [Route("Movies/SearchMovieGender")]
        public async Task<IActionResult> GetPeliculaPorGenero(int Genero)
        {

            var lista = await _db.Peliculas.Where(p => p.Activo == true && p.IdGenero == Genero).OrderBy(p => p.Id).ToListAsync();
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
            if (query == null)
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
            if (query == null)
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
        [HttpPut]
        [Route("Movie/ReactivateMovie")]
        public async Task<IActionResult> ReactivarPelicula(int id)
        {
            //selecciono la pelicula que tenga el mismo id
            var query = (from Pel in _db.Peliculas
                         where Pel.Id == id
                         select Pel).FirstOrDefault();
            if (query == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //una vez encontrado alcualizo los datos que deseo
            query.Activo = true;
            //guardo los cambios
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        [Route("Movie/SaveFileMovie")]
        public JsonResult SaveFile()
        {
            try
            {
                var HttpRequest = Request.Form;
                var PostedFile = HttpRequest.Files[0];
                string FileName = PostedFile.FileName;
                var PhysicalPath = "wwwroot/Peliculas/" + FileName;
                using (var stream = new FileStream(PhysicalPath, FileMode.Create))
                {
                    PostedFile.CopyTo(stream);
                }
                return new JsonResult(FileName);
            }
            catch (Exception)
            {
                return new JsonResult("Anonymous.png");
            }
        }
    }
}