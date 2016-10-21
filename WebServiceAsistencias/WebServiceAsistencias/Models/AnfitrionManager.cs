using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;

namespace WebServiceAsistencias.Models
{
    public class AnfitrionManager //Models views controllera  MVC4
    {
        private static string cadenaConexion =
            //@"Data Source=WIN-8TMISC6LAH5;Initial Catalog=DBCLIENTES;User Id=drojas;Password=drojas";//\SQLEXPRESS
            @"Data Source=172.24.43.105;Initial Catalog=AsiVenTEC;User Id=sa;Password=86374844botas";// Integrated Security=True";

        public bool InsertarCliente(Anfitrion cli)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "INSERT INTO Clientes (IdCliente,Nombre, Telefono) VALUES (@id,@nombre, @telefono)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = cli.id;
            cmd.Parameters.Add("@nombre", System.Data.SqlDbType.NVarChar).Value = cli.contra;


            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
        }


        public Anfitrion ObtenerCliente(string id,string pass)
        {
            Anfitrion cli = null;

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "SELECT a.cedula, a.contraseña from Anfitriones as a where a.cedula= @idcliente";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@idcliente", System.Data.SqlDbType.NVarChar).Value = id;

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            if (reader.Read())
            {
                cli = new Anfitrion();
                cli.id = reader.GetString(0);
                cli.contra = reader.GetString(1);
            }
            reader.Close();
            if (cli.contra.Equals(pass))
            {
                return cli;
            }

            return null;
        }

        public List<Anfitrion> ObtenerClientes()
        {
            List<Anfitrion> lista = new List<Anfitrion>();

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "SELECT IdCliente, Nombre, Telefono FROM Clientes";

            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Anfitrion cli = new Anfitrion();

                cli = new Anfitrion();
                cli.id = reader.GetString(0);
                cli.contra = reader.GetString(1);
    

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