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
    public class LoginController: ControllerBase
    {
        private readonly DisneyContext _db;
        public LoginController(DisneyContext db)
        {
            _db = db;
        }
        [HttpPost]
        [Route("Users/AddUser")]
        public async Task<IActionResult> AgregarUsuario([FromBody] Usuario usuario)
        {
            //diferentes validaciones
            if (usuario == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //si todo sale bien podre agregar un usuario
            await _db.AddAsync(usuario);
            //por ultimo gurado los cambios
            await _db.SaveChangesAsync();
            return Ok();
        }
        
    }
}