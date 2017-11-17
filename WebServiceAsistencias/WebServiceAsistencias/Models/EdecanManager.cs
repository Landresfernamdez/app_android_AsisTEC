using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebServiceAsistencias.Models
{
    public class EdecanManager //Models views controllera  MVC4
    {
        private static string cadenaConexion =
          //@"Data Source=WIN-8TMISC6LAH5;Initial Catalog=DBCLIENTES;User Id=drojas;Password=drojas";//\SQLEXPRESS
          @"Data Source=172.24.29.16;Initial Catalog=EventosTEC;User Id=sa;Password=71a0ses3919";

        public bool InsertarCliente(Edecan cli)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "INSERT INTO Clientes (IdCliente,Nombre, Telefono) VALUES (@id,@nombre, @telefono)";

            SqlCommand cmd = new SqlCommand(sql, con);

            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
        }

        public bool ActualizarCliente(Edecan cli)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "UPDATE Clientes SET Nombre = @nombre, Telefono = @telefono WHERE IdCliente = @idcliente";

            SqlCommand cmd = new SqlCommand(sql, con);

            //cmd.Parameters.Add("@nombre", System.Data.SqlDbType.NVarChar).Value = cli.Nombre;
            //cmd.Parameters.Add("@telefono", System.Data.SqlDbType.NVarChar).Value = cli.Telefono;
            //cmd.Parameters.Add("@idcliente", System.Data.SqlDbType.Int).Value = cli.Id;

            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
        }

        public List<Dato> consultarEdecan(string id)
        {
            List<Dato> lista = new List<Dato>();

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "select cedula from evento where cedula= @idcliente";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@idcliente", System.Data.SqlDbType.NVarChar).Value = id;

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Dato cli = new Dato();

               // cli = new Edecan();
                cli.dato = reader.GetString(0);
       

                lista.Add(cli);
            }

            reader.Close();
           

            return lista;
        }

        public List<Edecan> ObtenerEdecanes()
        {
            List<Edecan> lista = new List<Edecan>();

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "select a.cedula,p.nombre,p.apellidos from Anfitriones as a inner join Personas p on(a.cedula=p.cedula and a.tipoCuenta='e') ";

            SqlCommand cmd = new SqlCommand(sql, con);
     
            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Edecan cli = new Edecan();

                cli = new Edecan();
                cli.nombre = reader.GetString(0);
                cli.cedula = reader.GetString(1);
                cli.apellidos = reader.GetString(2);
          

                lista.Add(cli);
            }

            reader.Close();

            return lista;
        }
        public bool eliminarEdecan(string id)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "delete from anfitriones where cedula=@idEdecan delete from personas where cedula=@idEdecan";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@idEdecan", System.Data.SqlDbType.NVarChar).Value = id;

            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
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