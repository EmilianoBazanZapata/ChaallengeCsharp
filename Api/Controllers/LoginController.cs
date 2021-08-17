using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using API.Comandos.ComandosPelicula;
using Resultado;

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
                                resultado.InfoAdicional = "Se Registro el Usuario Exitosamente";
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