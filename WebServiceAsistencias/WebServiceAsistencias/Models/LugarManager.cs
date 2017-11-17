using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;

namespace WebServiceAsistencias.Models
{
    public class LugarManager //Models views controllera  MVC4
    {
        private static string cadenaConexion =
          //@"Data Source=WIN-8TMISC6LAH5;Initial Catalog=DBCLIENTES;User Id=drojas;Password=drojas";//\SQLEXPRESS
          @"Data Source=172.24.29.16;Initial Catalog=EventosTEC;User Id=sa;Password=71a0ses3919";

        public bool InsertarLugar(Lugar cli)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "INSERT INTO lugares (lugar) values (@place)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@place", System.Data.SqlDbType.NVarChar).Value = cli.lugar;
        //    cmd.Parameters.Add("@nombre", System.Data.SqlDbType.NVarChar).Value = cli.Nombre;
      //      cmd.Parameters.Add("@telefono", System.Data.SqlDbType.NVarChar).Value = cli.Telefono;

            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
        }

        public bool ActualizarCliente(Lugar cli)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "UPDATE Clientes SET Nombre = @nombre, Telefono = @telefono WHERE IdCliente = @idcliente";

            SqlCommand cmd = new SqlCommand(sql, con);

          //  cmd.Parameters.Add("@nombre", System.Data.SqlDbType.NVarChar).Value = cli.Nombre;
     //       cmd.Parameters.Add("@telefono", System.Data.SqlDbType.NVarChar).Value = cli.Telefono;
      //      cmd.Parameters.Add("@idcliente", System.Data.SqlDbType.Int).Value = cli.Id;

            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
        }

        public Lugar ObtenerCliente(string place)
        {
            Lugar cli = null;

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "SELECT *from lugares where lugar=@idcliente";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@idcliente", System.Data.SqlDbType.NVarChar).Value = place;

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            if (reader.Read())
            {
                cli = new Lugar();
                cli.lugar = reader.GetString(0);
          
            }

            reader.Close();
            if (cli!=null)
            {
                return cli;
            }

                return null;
            


           
        }

        public List<Lugar> ObtenerClientes()
        {
            List<Lugar> lista = new List<Lugar>();

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "SELECT*from lugares";

            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Lugar cli = new Lugar();

              
                cli.lugar =reader.GetString(0);
       

                lista.Add(cli);
            }

            reader.Close();

            return lista;
        }

        public bool EliminarCliente(int id)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "DELETE FROM Clientes WHERE IdCliente = @idcliente";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@idcliente", System.Data.SqlDbType.Int).Value = id;

            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
        }
       

    }
}