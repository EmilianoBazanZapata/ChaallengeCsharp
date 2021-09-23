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
    }
}