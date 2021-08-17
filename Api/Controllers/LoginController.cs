using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using API.Comandos.ComandosPelicula;
using Resultado;
using EASendMail;

namespace Api.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("User/AddUser")]
        public JsonResult AgregarPersonaje([FromBody] ComandoRegistrarUsuario usuario)
        {
            var resultado = new LoginResultado();
            //genero un string con la consulta hacia la BD
                                                          

            string Consulta = @"EXEC UP_REGISTRAR_USUARIO @EMAIL, @PASSWORD";
            //CREO UNA INSTANCIA NUEVA DE UN DATATABLE
            DataTable tb = new DataTable();
            //creo una variable reader para capturar los datos
            SqlDataReader MyReader;
            //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
            string sqlDataSource = _configuration.GetConnectionString("BD");

            if (usuario.Email.Equals(""))
            {
                resultado.Error = "Debe Ingresar un Email";
                return new JsonResult(resultado.Error);
            }
            if (usuario.Password.Equals(""))
            {
                resultado.Error = "Debe Ingresar una Contraseña";
                return new JsonResult(resultado.Error);
            }
            else
            {
                try
                {
                    using (SqlConnection sqlcon = new SqlConnection(sqlDataSource))
                    {

                        sqlcon.Open();
                        using (SqlCommand myCommand = new SqlCommand(Consulta, sqlcon))
                        {
                            if(resultado.VerificarUsuario(sqlcon,usuario.Email,usuario.Password))
                            {
                                resultado.InfoAdicional = "Estos Datos Estan Registrados";
                                return new JsonResult(resultado.InfoAdicional);
                            }
                            else
                            {
                                myCommand.Parameters.AddWithValue("@EMAIL", usuario.Email);
                                myCommand.Parameters.AddWithValue("@PASSWORD", usuario.Password);
                                MyReader = myCommand.ExecuteReader();
                                //la tabla la cargo con los datos obtenidos de mi sentencia
                                tb.Load(MyReader);
                                //cierro las conexiones
                                MyReader.Close();
                                sqlcon.Close();


                                //envio el email al administrador
                                string email = "aca va el email del dueño";
                                string contraseña = "aca va la contraseña del email del dueño";
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
                                SmtpClient objetocliente = new SmtpClient();
                                objetocliente.SendMail(objetoservidor, objeto_correo);


                                resultado.InfoAdicional = "mensaje enviado correctamente";
                                return new JsonResult(resultado.InfoAdicional);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultado.Error = "Ocurrio un Error Inesperado";
                    resultado.InfoAdicional = ex.ToString();
                    return new JsonResult(resultado.Error + resultado.InfoAdicional);
                }
            }
        }
        
        
    }
}