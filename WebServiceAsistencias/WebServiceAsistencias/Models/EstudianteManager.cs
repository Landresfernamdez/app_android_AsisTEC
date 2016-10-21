using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
namespace WebServiceAsistencias.Models
{
    public class EstudianteManager
    {
        private static string cadenaConexion =
            //@"Data Source=WIN-8TMISC6LAH5;Initial Catalog=DBCLIENTES;User Id=drojas;Password=drojas";//\SQLEXPRESS
            @"Data Source=172.24.43.105;Initial Catalog=AsiVenTEC;User Id=sa;Password=86374844botas";// Integrated Security=True";
        public Estudiante ObtenerEstudiante(string id)
        {
            Estudiante student = null;

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "SELECT a.estado , a.carne, a.cedula from Estudiantes as a where a.carne=@carne";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@carne",System.Data.SqlDbType.NVarChar).Value = id;

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            if (reader.Read())
            {
                student = new Estudiante();
                student.estado= reader.GetString(0);
                student.carne = reader.GetString(1);
                student.cedula = reader.GetString(2);
            }
            reader.Close();
            if (student.carne.Equals(id))
            {
                return student;
            }
            return null;
        }
        public bool ActualizarEstudiante(Estudiante student)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql ="UPDATE Estudiantes SET estado=@estado,cedula=@cedula WHERE carne =@carne";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@carne", System.Data.SqlDbType.NVarChar).Value =student.carne;
            cmd.Parameters.Add("@estado", System.Data.SqlDbType.NVarChar).Value = student.estado;
            cmd.Parameters.Add("@cedula", System.Data.SqlDbType.NVarChar).Value = student.cedula;
            
            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
        }
    }
}