using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Resultado
{
    public class LoginResultado
    {
        public bool Ok { get; set; }
        public string Error { get; set; }
        public string InfoAdicional { get; set; }
        public DataTable Usuario { get; set; }
        public bool VerificarUsuario(SqlConnection sqlcon, string Email, string Password)
        {
            var verificar = string.Format("SELECT * FROM V_VERIFICAR_EXISTENCIA_DE_USUARIO(@EMAIL,@PASSWORD)");
            SqlCommand comando = new SqlCommand(verificar, sqlcon);
            comando.Parameters.AddWithValue("@EMAIL", Email);
            comando.Parameters.AddWithValue("@PASSWORD", Password);
            SqlDataReader dr = null;
            dr = comando.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                return true;
            }
            else
            {
                dr.Close();
                return false;
            }
        }
    }
}