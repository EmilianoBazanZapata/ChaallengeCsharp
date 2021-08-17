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
        [HttpPut]
        [Route("Character/DeleteCharacter")]
        public JsonResult EliminarPersonaje([FromBody] ComandoEliminarPersonaje personaje)
        {
            var resultado = new CharacterResultado();
            //genero un string con la consulta hacia la BD
            string Consulta = @"EXEC UP_ELIMINAR_PERSONAJE @ID";
            //CREO UNA INSTANCIA NUEVA DE UN DATATABLE
            DataTable tb = new DataTable();
            //creo una variable reader para capturar los datos
            SqlDataReader MyReader;
            //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
            //string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
            string sqlDataSource = _configuration.GetConnectionString("BD");


            try
            {
                using (SqlConnection sqlcon = new SqlConnection(sqlDataSource))
                {

                    sqlcon.Open();
                        using (SqlCommand myCommand = new SqlCommand(Consulta, sqlcon))
                        {
                            myCommand.Parameters.AddWithValue("@ID", personaje.Id);
                            MyReader = myCommand.ExecuteReader();
                            //la tabla la cargo con los datos obtenidos de mi sentencia
                            tb.Load(MyReader);
                            //cierro las conexiones
                            MyReader.Close();
                            sqlcon.Close();
                            resultado.InfoAdicional = "Se Elimino Exitosamente el Personaje";
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
        [HttpGet]
        [Route("SearchCharacterForName/characters/{name}")]
        public JsonResult BuscarPersonajePorNombre(string name)
        {
            var resultado = new CharacterResultado();
            try
            {

                //genero un string con la consulta hacia la BD
                string Consulta = @"SELECT * FROM V_PER(@NOMBRE)";
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
                        myCommand.Parameters.AddWithValue("@NOMBRE", name);
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
        [HttpGet]
        [Route("SearchCharacterForAge/characters/{age}")]
        public JsonResult BuscarPersonajePorEdad(int age)
        {
            var resultado = new CharacterResultado();
            try
            {

                //genero un string con la consulta hacia la BD
                string Consulta = @"SELECT * FROM V_PER_POR_EDAD(@EDAD)";
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
                        myCommand.Parameters.AddWithValue("@EDAD", age);
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
        [HttpGet]
        [Route("SearchDetail/characters/{id}")]
        public JsonResult VerDetallePorActor(int id)
        {
            var resultado = new CharacterResultado();
            try
            {

                //genero un string con la consulta hacia la BD
                string Consulta = @"SELECT * FROM V_DETALLE_DEL_ACTOR(@ID)";
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
                        myCommand.Parameters.AddWithValue("@ID", id);
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
        
    }
}