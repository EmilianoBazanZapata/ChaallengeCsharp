using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API.Comandos.ComandosPelicula;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Resultado;

namespace Api.Controllers
{
    public class GenderController:ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GenderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("Genders")]
        public JsonResult ObtenerGeneros()
        {
            var resultado = new GenderResultado();
            try
            {

                //genero un string con la consulta hacia la BD
                string Consulta = @"SELECT Nombre,imagen
                                    FROM Generos;";
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
                        resultado.Generos = tb;
                        return new JsonResult(resultado.Generos);
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
        [Route("Gender/AddGender")]
        public JsonResult AgregarGenero([FromBody] ComandoAgregarGenero genero)
        {
            var resultado = new CharacterResultado();
            //genero un string con la consulta hacia la BD
                                                          

            string Consulta = @"EXEC UP_AGREGAR_GENERO @NOMBRE,@IMAGEN";
            //CREO UNA INSTANCIA NUEVA DE UN DATATABLE
            DataTable tb = new DataTable();
            //creo una variable reader para capturar los datos
            SqlDataReader MyReader;
            //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
            string sqlDataSource = _configuration.GetConnectionString("BD");

            if (genero.Titulo.Equals(""))
            {
                resultado.Error = "Debe Ingresar un Genero";
                return new JsonResult(resultado.Error);
            }
            if (genero.imagen.Equals(""))
            {
                resultado.Error = "Debe Ingresar una Imagen";
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
                            myCommand.Parameters.AddWithValue("@NOMBRE", genero.Titulo);
                            myCommand.Parameters.AddWithValue("@IMAGEN", genero.imagen);
                            MyReader = myCommand.ExecuteReader();
                            //la tabla la cargo con los datos obtenidos de mi sentencia
                            tb.Load(MyReader);
                            //cierro las conexiones
                            MyReader.Close();
                            sqlcon.Close();
                            resultado.InfoAdicional = "Se Agrego el Genero Exitosamente";
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
        [Route("Gender/UpdateGender")]
        public JsonResult ActualizarPersonaje([FromBody] ComandoActualizarGenero genero)
        {
            var resultado = new CharacterResultado();
            //genero un string con la consulta hacia la BD
            string Consulta = @"EXEC UP_ACTUALIZAR_GENERO @GENERO,@IMAGEN , @ID";
            //CREO UNA INSTANCIA NUEVA DE UN DATATABLE
            DataTable tb = new DataTable();
            //creo una variable reader para capturar los datos
            SqlDataReader MyReader;
            //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
            //string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
            string sqlDataSource = _configuration.GetConnectionString("BD");

            if (genero.Titulo.Equals(""))
            {
                resultado.Error = "Debe Ingreesar un Nombre";
                return new JsonResult(resultado.Error);
            }
            if (genero.imagen.Equals(""))
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
                            myCommand.Parameters.AddWithValue("@GENERO", genero.Titulo);
                            myCommand.Parameters.AddWithValue("@IMAGEN", genero.imagen);
                            myCommand.Parameters.AddWithValue("@ID", genero.Id);
                            MyReader = myCommand.ExecuteReader();
                            //la tabla la cargo con los datos obtenidos de mi sentencia
                            tb.Load(MyReader);
                            //cierro las conexiones
                            MyReader.Close();
                            sqlcon.Close();
                            resultado.InfoAdicional = "El Genero se Actualizo Exitosamente";
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