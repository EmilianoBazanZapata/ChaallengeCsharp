using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EASendMail;
using Api.Helper;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly DisneyContext _db;
        private readonly IConfiguration _configuration;
        public LoginController(DisneyContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
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

            string email = "";
            string contraseña = "";
            SmtpMail objeto_correo = new SmtpMail("TryIt");
            //desde donde lo envio
            objeto_correo.From = "Disney";
            //hacia donde lo envio
            objeto_correo.To = usuario.Email;
            //asunto
            objeto_correo.Subject = "Bienvenido a Disney : " + usuario.Email;
            //mensaje
            objeto_correo.TextBody = "si logras ver este mensaje es que pudiste registrarte con existo a nuestro sistema";

            SmtpServer objetoservidor = new SmtpServer("smtp.gmail.com");

            //mail con el que me logeo
            objetoservidor.User = email;
            objetoservidor.Password = contraseña;

            //puerto que uso
            objetoservidor.Port = 587;
            //conexion que voy a usar y la cifro
            objetoservidor.ConnectType = SmtpConnectType.ConnectSSLAuto;

            //poder enviar el correo
            EASendMail.SmtpClient objetocliente = new EASendMail.SmtpClient();
            objetocliente.SendMail(objetoservidor, objeto_correo);


            return Ok();
        }
        [HttpPost]
        [Route("User/LoginUser")]
        public async Task<IActionResult> IngresarAlSistema([FromBody] Usuario usuario)
        {
            /*var query = (from U in _db.Usuarios
                         where U.Email == usuario.Email &&
                         U.Password == usuario.Password
                         select U.Email);*/
            var obj = await _db.Usuarios.FirstOrDefaultAsync(c => c.Email == usuario.Email && c.Password == usuario.Password);
            if (obj == null)
            {
                return NotFound();
            }
            string secret = this._configuration.GetValue<string>("Secret");
            var JwtHelper = new JWTHelper(secret);
            var Token = JwtHelper.CreateToken(usuario.Email);
            return Ok(new { Ok = true, msg = "Logeado con Exito", Token });
        }
    }
}