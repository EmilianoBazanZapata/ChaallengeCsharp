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
    public class PersonajeController : ControllerBase
    {
        private readonly DisneyContext _db;
        public PersonajeController(DisneyContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("Characters/Characters")]
        public async Task<IActionResult> GetCharacters()
        {
            var query = (from Pel in _db.Personajes
                         where Pel.Activo == true 
                         select new { Pel.Nombre, Pel.imagen });
            var lista = await query.ToListAsync();
            return Ok(lista);
        }
        [HttpGet]
        [Route("Characters/SearchCharacterForName")]
        public async Task<IActionResult> GetCharactersForName(string Nombre)
        {
            //ordeno los personajes por nombre
            var lista = await _db.Personajes.Where(p => p.Activo == true && p.Nombre == Nombre).OrderBy(p => p.Nombre).ToListAsync();
            return Ok(lista);
        }
        [HttpGet]
        [Route("Characters/SearchCharacterForAge")]
        public async Task<IActionResult> GetCharactersForAge(int Edad)
        {
            //ordeno los personajes por nombre
            var lista = await _db.Personajes.Where(p => p.Activo == true && p.Edad == Edad).OrderBy(p => p.Nombre).ToListAsync();
            return Ok(lista);
        }
        [HttpGet]
        [Route("Characters/SearchCharacterForMovie")]
        public async Task<IActionResult> GetCharactersForMovie(int IdPelicula)
        {
            var query = (from Per in _db.Personajes
                         join PP in _db.PeliculaPorPersonajes on Per.Id equals PP.IdPersonaje
                         join Ps in _db.Peliculas on PP.IdPelicula equals Ps.Id
                         where PP.IdPelicula == IdPelicula
                         select Per.Nombre);
            //ordeno los personajes por nombre
            var lista = await query.ToListAsync();
            return Ok(lista);
        }
        [HttpPost]
        [Route("Characters/AddCharacter")]
        public async Task<IActionResult> AddCharacter([FromBody] Personaje personaje)
        {
            //diferentes validaciones
            if (personaje == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //si todo sale bien podre agregar un personaje
            await _db.AddAsync(personaje);
            //por ultimo gurado los cambios
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("Character/DeleteCharacter")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            //selecciono la pelicula que tenga el mismo id
            var query = (from Per in _db.Personajes
                         where Per.Id == id
                         select Per).FirstOrDefault();
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
        


        public JsonResult SaveFile()
        {
            try
            {
                var HttpRequest = Request.Form;
                var PostedFile = HttpRequest.Files[0];
                string FileName = PostedFile.FileName;
                var PhysicalPath = "wwwroot/Personajes/" + FileName;
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