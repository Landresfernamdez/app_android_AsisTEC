using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebServiceAsistencias.Models
{
    public class EncargadoManager //Models views controllera  MVC4
    {
        private static string cadenaConexion =
            //@"Data Source=WIN-8TMISC6LAH5;Initial Catalog=DBCLIENTES;User Id=drojas;Password=drojas";//\SQLEXPRESS
            @"Data Source=172.24.29.16;Initial Catalog=EventosTEC;User Id=sa;Password=71a0ses3919";

        public bool ModEdecan(Encargado cli)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "UPDATE personas  set nombre=@nombre, apellidos=@apellidos, procedencia=@procedencia, edad=@edad where cedula=@cedula UPDATE anfitriones set contraseña=@contraseña,tipoCuenta=@tipoCuenta where cedula=@cedula";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@cedula", System.Data.SqlDbType.NVarChar).Value = cli.cedula;
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


        public bool ActualizarCliente(Encargado cli)
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

        public Encargado ObtenerCliente(int id)
        {
            Encargado cli = null;

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "SELECT Nombre, Telefono FROM Clientes WHERE IdCliente = @idcliente";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@idcliente", System.Data.SqlDbType.NVarChar).Value = id;

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            if (reader.Read())
            {
                cli = new Encargado();
                //cli.Id = id;
                //cli.Nombre = reader.GetString(0);
                //cli.Telefono = reader.GetString(1);
            }

            reader.Close();

            return cli;
        }

        public List<Encargado> ObtenerEncargados()
        {
            List<Encargado> lista = new List<Encargado>();

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "select a.cedula,p.nombre,p.apellidos,p.procedencia,p.edad,a.contraseña, a.tipoCuenta from Personas as p inner join Anfitriones as a on(a.cedula=p.cedula) ";

            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Encargado encargad = new Encargado();

                encargad = new Encargado();
                encargad.cedula = reader.GetString(0);
                encargad.nombre = reader.GetString(1);
                encargad.apellidos = reader.GetString(2);
                encargad.procedencia = reader.GetString(3);
                encargad.edad = reader.GetInt32(4);
                encargad.contraseña = reader.GetString(5);
                encargad.tipoCuenta = reader.GetString(6);
                    //cli.Id = reader.GetInt32(0);
                //cli.Nombre = reader.GetString(1);
                //cli.Telefono = reader.GetString(2);

                lista.Add(encargad);
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