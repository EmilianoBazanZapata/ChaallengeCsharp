using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Resultado;
using API.Comandos.ComandosPersonaje;

namespace Api.Controllers
{
    public class CharacterController:ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CharacterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("Characters")]
        public JsonResult ObtenerPersonajes()
        {
            var resultado = new CharacterResultado();
            try
            {

                //genero un string con la consulta hacia la BD
                string Consulta = @"SELECT Nombre,imagen
                                    FROM Personajes;";
                //CREO UNA INSTANCIA NUEVA DE UN DATATABLE
                DataTable tb = new DataTable();
                //creo una variable reader para capturar los datos
                SqlDataReader MyReader;
                //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
                string sqlDataSource = _configuration.GetConnectionString("BD");

                using (SqlConnection sqlcon = new SqlConnection(sqlDataSource))
                {

                    sqlcon.Open();
                    using (SqlCommand myCommand = new SqlCommand(Consulta, sqlcon))
                    {
                        MyReader = myCommand.ExecuteReader();
                        //la tabla la cargo con los datos obtenidos de mi select 
                        tb.Load(MyReader);
                        //cierro las conexiones
                        MyReader.Close();
                        sqlcon.Close();
                        resultado.Personajes = tb;
                        return new JsonResult(resultado.Personajes);
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
        [HttpPost]
        [Route("Character/AddCharacter")]
        public JsonResult AgregarPersonaje([FromBody] ComandoAgregarPersonaje personaje)
        {
            var resultado = new CharacterResultado();
            //genero un string con la consulta hacia la BD
                                                          

            string Consulta = @"EXEC UP_AGREGAR_PERSONAJE @NOMBRE, @EDAD, @PESO, @HISTORIA ,@IMAGEN";
            //CREO UNA INSTANCIA NUEVA DE UN DATATABLE
            DataTable tb = new DataTable();
            //creo una variable reader para capturar los datos
            SqlDataReader MyReader;
            //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
            string sqlDataSource = _configuration.GetConnectionString("BD");

            if (personaje.Nombre.Equals(""))
            {
                resultado.Error = "Debe Ingreesar un Nombre";
                return new JsonResult(resultado.Error);
            }
            if (personaje.Edad.Equals(""))
            {
                resultado.Error = "Debe Ingreesar una Edad";
                return new JsonResult(resultado.Error);
            }
            if (personaje.Peso.Equals(""))
            {
                resultado.Error = "Debe Ingreesar un Peso";
                return new JsonResult(resultado.Error);
            }
            if (personaje.Historia.Equals(""))
            {
                resultado.Error = "Debe Ingreesar una Historia";
                return new JsonResult(resultado.Error);
            }
            if (personaje.Imagen.Equals(""))
            {
                resultado.Error = "Debe Ingreesar una Imagen";
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
                            myCommand.Parameters.AddWithValue("@NOMBRE", personaje.Nombre);
                            myCommand.Parameters.AddWithValue("@EDAD", personaje.Edad);
                            myCommand.Parameters.AddWithValue("@PESO", personaje.Peso);
                            myCommand.Parameters.AddWithValue("@HISTORIA", personaje.Historia);
                            myCommand.Parameters.AddWithValue("@IMAGEN", personaje.Imagen);
                            MyReader = myCommand.ExecuteReader();
                            //la tabla la cargo con los datos obtenidos de mi sentencia
                            tb.Load(MyReader);
                            //cierro las conexiones
                            MyReader.Close();
                            sqlcon.Close();
                            resultado.InfoAdicional = "Se Agrego el personaje Exitosamente";
                            return new JsonResult(resultado.InfoAdicional);
                            
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
        [HttpPut]
        [Route("Character/UpdateCharacter")]
        public JsonResult ActualizarPersonaje([FromBody] ComandoActualizarPersonaje personaje)
        {
            var resultado = new CharacterResultado();
            //genero un string con la consulta hacia la BD
            string Consulta = @"EXEC UP_ACTUALIZAR_PERSONAJE @NOMBRE, @EDAD, @PESO, @HISTORIA, @IMAGEN, @ID";
            //CREO UNA INSTANCIA NUEVA DE UN DATATABLE
            DataTable tb = new DataTable();
            //creo una variable reader para capturar los datos
            SqlDataReader MyReader;
            //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
            //string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
            string sqlDataSource = _configuration.GetConnectionString("BD");

            if (personaje.Nombre.Equals(""))
            {
                resultado.Error = "Debe Ingreesar un Nombre";
                return new JsonResult(resultado.Error);
            }
            if (personaje.Edad.Equals(""))
            {
                resultado.Error = "Debe Ingreesar una Edad";
                return new JsonResult(resultado.Error);
            }
            if (personaje.Peso.Equals(""))
            {
                resultado.Error = "Debe Ingreesar un Peso";
                return new JsonResult(resultado.Error);
            }
            if (personaje.Historia.Equals(""))
            {
                resultado.Error = "Debe Ingreesar una Historia";
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
                            myCommand.Parameters.AddWithValue("@NOMBRE", personaje.Nombre);
                            myCommand.Parameters.AddWithValue("@EDAD", personaje.Edad);
                            myCommand.Parameters.AddWithValue("@PESO", personaje.Peso);
                            myCommand.Parameters.AddWithValue("@HISTORIA", personaje.Historia);
                            myCommand.Parameters.AddWithValue("@IMAGEN", personaje.Imagen);
                            myCommand.Parameters.AddWithValue("@ID", personaje.Id);
                            MyReader = myCommand.ExecuteReader();
                            //la tabla la cargo con los datos obtenidos de mi sentencia
                            tb.Load(MyReader);
                            //cierro las conexiones
                            MyReader.Close();
                            sqlcon.Close();
                            resultado.InfoAdicional = "El Personaje se Actualizo Exitosamente";
                            return new JsonResult(resultado.InfoAdicional);
                            
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