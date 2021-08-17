using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        
    }
}