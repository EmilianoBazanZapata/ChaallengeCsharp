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
    public class GeneroController : ControllerBase
    {
        private readonly DisneyContext _db;
        public GeneroController(DisneyContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("Genders")]
        public async Task<IActionResult> GetGeneros()
        {
            //ordeno los generos por nombre
            var lista = await _db.Generos.OrderBy(p => p.Nombre).ToListAsync();
            return Ok(lista);
        }
        
    }
}