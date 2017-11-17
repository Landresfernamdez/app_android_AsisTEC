using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;

namespace WebServiceAsistencias.Models
{
    public class HostManager //Models views controllera  MVC4
    {
        private static string cadenaConexion =
            @"Data Source=172.24.29.16;Initial Catalog=EventosTEC;User Id=sa;Password=71a0ses3919";


        public Anfitrion ObtenerCliente(string id,string pass)
        {
            Anfitrion anfi = null;

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "SELECT a.cedula, a.contraseña,a.tipoCuenta from Anfitriones as a where a.cedula= @idanfitrion and a.contraseña=@idvalida";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@idanfitrion", System.Data.SqlDbType.NVarChar).Value = id;

            cmd.Parameters.Add("@idvalida", System.Data.SqlDbType.NVarChar).Value = pass;

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            if (reader.Read())
            {
                anfi = new Anfitrion();
                anfi.id = reader.GetString(0);
                anfi.contra = reader.GetString(1);
                anfi.tipoCuenta = reader.GetString(2);
            }
            reader.Close();
            if (anfi.contra.Equals(pass) && anfi.tipoCuenta.Equals("e"))
            {
                return anfi;
            }
            return null;
        }
       

    }
}