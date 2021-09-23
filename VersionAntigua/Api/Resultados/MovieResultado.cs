using System.Data;
using System;
using System.Data.SqlClient;

namespace Resultado
{
    public class MovieResultado
    {
        public bool Ok { get; set; }
        public string Error { get; set; }
        public string InfoAdicional { get; set; }
        public DataTable Peliculas { get; set; }
    }
}