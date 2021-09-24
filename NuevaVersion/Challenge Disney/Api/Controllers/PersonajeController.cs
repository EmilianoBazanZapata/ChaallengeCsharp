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
    public class PersonajeController : ControllerBase
    {
        private readonly DisneyContext _db;
        public PersonajeController(DisneyContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("Characters/ListCharacters")]
        public async Task<IActionResult> GetCharacters()
        {
            //ordeno los personajes por nombre
            var lista = await _db.Personajes.Where(p => p.Activo == true).OrderBy(p => p.Nombre).ToListAsync();
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
        [HttpPost]
        [Route("Characters/AddCharacter")]
        public async Task<IActionResult> AgregarPelicula([FromBody] Personaje personaje)
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


    }
}