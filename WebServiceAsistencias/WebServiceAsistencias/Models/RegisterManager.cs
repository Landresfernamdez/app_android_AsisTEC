using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebServiceAsistencias.Models
{
    public class RegisterManager
    {
        private static string cadenaConexion = @"Data Source=172.24.29.16;Initial Catalog=EventosTEC;User Id=sa;Password=71a0ses3919";

        public bool InsertarRegistro(Registro regi)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);
            con.Open();
            string sql = "INSERT INTO Rsalida (id_ev,carne,fecha,hora) VALUES (@id,@car,@fech,@hor)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.NVarChar).Value = regi.id_ev;
            cmd.Parameters.Add("@car", System.Data.SqlDbType.NVarChar).Value = regi.carne;
            cmd.Parameters.Add("@fech", System.Data.SqlDbType.NVarChar).Value = regi.fecha;
            cmd.Parameters.Add("@hor", System.Data.SqlDbType.NVarChar).Value = regi.hora;
            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
        }
    }
}