using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;

namespace WebServiceAsistencias.Models
{
    public class AnfitrionManager //Models views controllera  MVC4
    {
        private static string cadenaConexion = @"Data Source=172.24.29.16;Initial Catalog=EventosTEC;User Id=sa;Password=71a0ses3919";

        public bool InsertarEdecan(Encargado cli)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "insert into personas(cedula,nombre,apellidos,procedencia,edad) values (@cedula,@nombre,@apellidos,@procedencia,@edad) insert into Anfitriones(cedula,contraseña,tipoCuenta) values(@cedula,@contraseña,@tipoCuenta)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@cedula", System.Data.SqlDbType.NVarChar).Value =cli.cedula;
            cmd.Parameters.Add("@nombre", System.Data.SqlDbType.NVarChar).Value = cli.nombre;
            cmd.Parameters.Add("@apellidos", System.Data.SqlDbType.NVarChar).Value = cli.apellidos;
            cmd.Parameters.Add("@procedencia", System.Data.SqlDbType.NVarChar).Value = cli.procedencia;
            cmd.Parameters.Add("@edad", System.Data.SqlDbType.Int).Value = cli.edad;
            cmd.Parameters.Add("@contraseña", System.Data.SqlDbType.NVarChar).Value = cli.contraseña;
            cmd.Parameters.Add("@tipoCuenta", System.Data.SqlDbType.NVarChar).Value = cli.tipoCuenta;





            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
        }


        /* public bool ActualizarCliente(Anfitrion cli)
         {
             SqlConnection con = new SqlConnection(cadenaConexion);

             con.Open();

             string sql = "UPDATE Clientes SET Nombre = @nombre, Telefono = @telefono WHERE IdCliente = @idcliente";

             SqlCommand cmd = new SqlCommand(sql, con);

             cmd.Parameters.Add("@nombre", System.Data.SqlDbType.NVarChar).Value = cli.Nombre;
             cmd.Parameters.Add("@telefono", System.Data.SqlDbType.NVarChar).Value = cli.Telefono;
             cmd.Parameters.Add("@idcliente", System.Data.SqlDbType.Int).Value = cli.Id;

             int res = cmd.ExecuteNonQuery();

             con.Close();

             return (res == 1);
         }*/

        public Anfitrion ObtenerCliente(string id,string pass)
        {
            Anfitrion cli = null;

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "SELECT a.cedula, a.contraseña from Anfitriones as a where a.cedula= @idcliente and a.tipoCuenta='a'";

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