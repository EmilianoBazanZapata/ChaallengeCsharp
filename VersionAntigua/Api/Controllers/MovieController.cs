using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Comandos.ComandosPelicula;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Resultado;

namespace Api.Controllers
{
    public class MovieController:ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MovieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("Movies")]
        [Authorize]
        public JsonResult ObtenerPeliculasActivas()
        {
            var resultado = new MovieResultado();
            try
            {

                //genero un string con la consulta hacia la BD
                string Consulta = @"SELECT Titulo, imagen , FechaCreacion
                                    FROM Peliculas;";
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
                        resultado.Peliculas = tb;
                        return new JsonResult(resultado.Peliculas);
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
        [Route("Movie/AddMovie")]
        [Authorize]
        public JsonResult AgregarPelicula([FromBody] ComandoAgregarPelicula pelicula)
        {
            var resultado = new CharacterResultado();
            //genero un string con la consulta hacia la BD
                                                          

            string Consulta = @"EXEC UP_AGREGAR_PELICULA @TITULO, @FECHA , @CALIFICACION, @IMAGEN";
            //CREO UNA INSTANCIA NUEVA DE UN DATATABLE
            DataTable tb = new DataTable();
            //creo una variable reader para capturar los datos
            SqlDataReader MyReader;
            //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
            string sqlDataSource = _configuration.GetConnectionString("BD");

            if (pelicula.Titulo.Equals(""))
            {
                resultado.Error = "Debe Ingresar un Titulo";
                return new JsonResult(resultado.Error);
            }
            if (pelicula.imagen.Equals(""))
            {
                resultado.Error = "Debe Ingresar una imagen";
                return new JsonResult(resultado.Error);
            }
            if (pelicula.Calificacion.Equals(""))
            {
                resultado.Error = "Debe Ingreesar una Calificacion";
                return new JsonResult(resultado.Error);
            }
            if (pelicula.FechaCreacion.Equals(""))
            {
                resultado.Error = "Debe Ingreesar una Fecha de Creacion";
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
                            myCommand.Parameters.AddWithValue("@TITULO", pelicula.Titulo);
                            myCommand.Parameters.AddWithValue("@FECHA", pelicula.FechaCreacion);
                            myCommand.Parameters.AddWithValue("@CALIFICACION", pelicula.Calificacion);
                            myCommand.Parameters.AddWithValue("@IMAGEN", pelicula.imagen);
                            MyReader = myCommand.ExecuteReader();
                            //la tabla la cargo con los datos obtenidos de mi sentencia
                            tb.Load(MyReader);
                            //cierro las conexiones
                            MyReader.Close();
                            sqlcon.Close();
                            resultado.InfoAdicional = "Se Agrego la Pelicula Exitosamente";
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
        [Route("Movie/UpdateMovie")]
        [Authorize]
        public JsonResult ActualizarPersonaje([FromBody] ComandoActualizarPelicula pelicula)
        {
            var resultado = new MovieResultado();
            //genero un string con la consulta hacia la BD
            string Consulta = @"EXEC UP_ACTUALIZAR_PELICULA @TITULO, @FECHA , @CALIFICACION , @IMAGEN , @ID";
            //CREO UNA INSTANCIA NUEVA DE UN DATATABLE
            DataTable tb = new DataTable();
            //creo una variable reader para capturar los datos
            SqlDataReader MyReader;
            //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
            //string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
            string sqlDataSource = _configuration.GetConnectionString("BD");

            if (pelicula.Titulo.Equals(""))
            {
                resultado.Error = "Debe Ingresar un Titulo";
                return new JsonResult(resultado.Error);
            }
            if (pelicula.imagen.Equals(""))
            {
                resultado.Error = "Debe Ingresar una imagen";
                return new JsonResult(resultado.Error);
            }
            if (pelicula.Calificacion.Equals(""))
            {
                resultado.Error = "Debe Ingreesar una Calificacion";
                return new JsonResult(resultado.Error);
            }
            if (pelicula.FechaCreacion.Equals(""))
            {
                resultado.Error = "Debe Ingreesar una Fecha de Creacion";
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
                            myCommand.Parameters.AddWithValue("@TITULO", pelicula.Titulo);
                            myCommand.Parameters.AddWithValue("@FECHA", pelicula.FechaCreacion);
                            myCommand.Parameters.AddWithValue("@CALIFICACION", pelicula.Calificacion);
                            myCommand.Parameters.AddWithValue("@IMAGEN", pelicula.imagen);
                            myCommand.Parameters.AddWithValue("@ID", pelicula.Id);
                            MyReader = myCommand.ExecuteReader();
                            //la tabla la cargo con los datos obtenidos de mi sentencia
                            tb.Load(MyReader);
                            //cierro las conexiones
                            MyReader.Close();
                            sqlcon.Close();
                            resultado.InfoAdicional = "La Pelicula se Actualizo Exitosamente";
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
        [Route("Movie/DeleteMovie")]
        [Authorize]
        public JsonResult EliminarPelicula([FromBody] ComandoEliminarPelicula pelicula)
        {
            var resultado = new CharacterResultado();
            //genero un string con la consulta hacia la BD
            string Consulta = @"EXEC UP_ELIMINAR_PELICULA @ID";
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
                            myCommand.Parameters.AddWithValue("@ID", pelicula.Id);
                            MyReader = myCommand.ExecuteReader();
                            //la tabla la cargo con los datos obtenidos de mi sentencia
                            tb.Load(MyReader);
                            //cierro las conexiones
                            MyReader.Close();
                            sqlcon.Close();
                            resultado.InfoAdicional = "Se Elimino Exitosamente la Pelicula";
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
        [Route("SearchMovieDesc")]
        [Authorize]
        public JsonResult OrdenarPeliculasDeFormaDescendente()
        {
            var resultado = new CharacterResultado();
            try
            {
                //genero un string con la consulta hacia la BD
                string Consulta = @"SELECT *
                                    FROM peliculas P
                                    ORDER BY P.FechaCreacion DESC";
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
        [HttpGet]
        [Route("SearchMovieAsc")]
        [Authorize]
        public JsonResult OrdenarPeliculasDeFormaAscendente()
        {
            var resultado = new CharacterResultado();
            try
            {
                //genero un string con la consulta hacia la BD
                string Consulta = @"SELECT *
                                    FROM peliculas P
                                    ORDER BY P.FechaCreacion ASC";
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
        [Route("Movie/AddMovieAndCharacter")]
        [Authorize]
        public JsonResult RelacionarPeliculayActores([FromBody] ComandoRelacionarPeliculayActores comando)
        {
            var resultado = new CharacterResultado();
            //genero un string con la consulta hacia la BD
                                                          

            string Consulta = @"EXEC UP_RELACIONAR_PELICULA_Y_ACTORES @IDPELICULA , @IDACTOR";
            //CREO UNA INSTANCIA NUEVA DE UN DATATABLE
            DataTable tb = new DataTable();
            //creo una variable reader para capturar los datos
            SqlDataReader MyReader;
            //TOMO LA CADENA CONEXXION QUE SE UBICA EN APPSETINGS.JSON
            string sqlDataSource = _configuration.GetConnectionString("BD");


            try
            {
                using (SqlConnection sqlcon = new SqlConnection(sqlDataSource))
                {

                    sqlcon.Open();
                    using (SqlCommand myCommand = new SqlCommand(Consulta, sqlcon))
                    {
                        myCommand.Parameters.AddWithValue("@IDPELICULA", comando.IdPelidula);
                        myCommand.Parameters.AddWithValue("@IDACTOR", comando.IdActor);
                        MyReader = myCommand.ExecuteReader();
                        //la tabla la cargo con los datos obtenidos de mi sentencia
                        tb.Load(MyReader);
                        //cierro las conexiones
                        MyReader.Close();
                        sqlcon.Close();
                        resultado.InfoAdicional = "Se Agrego Exitosamente el Actor a la Pelicula";
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
        [Route("SearchCharacterForMovie/Movie/{IdMovie}")]
        [Authorize]
        public JsonResult BuscarPersonajePorPelicula(int IdMovie)
        {
            var resultado = new CharacterResultado();
            try
            {

                //genero un string con la consulta hacia la BD
                string Consulta = @" SELECT * FROM V_VER_ACTORES_DE_UNA_PELICULA (@IDPELICULA)";
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
                        myCommand.Parameters.AddWithValue("@IDPELICULA", IdMovie);
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