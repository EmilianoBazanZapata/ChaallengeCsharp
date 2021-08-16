using System.Data;
using System;
using System.Data.SqlClient;

namespace Resultado
{
    public class CharacterResultado
    {
        public bool Ok { get; set; }
        public string Error { get; set; }
        public string InfoAdicional { get; set; }
        public DataTable Personajes { get; set; }
    }
}