using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebServiceAsistencias.Models
{
    public class sEventManager //Models views controllera  MVC4
    {
        private static string cadenaConexion =
           //@"Data Source=WIN-8TMISC6LAH5;Initial Catalog=DBCLIENTES;User Id=drojas;Password=drojas";//\SQLEXPRESS
           @"Data Source=172.24.29.16;Initial Catalog=EventosTEC;User Id=sa;Password=71a0ses3919";

        public bool modificarEvento(sEvent cli,string id)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "INSERT INTO Clientes (IdCliente,Nombre, Telefono) VALUES (@id,@nombre, @telefono)";

            SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = cli.Id;
            //cmd.Parameters.Add("@nombre", System.Data.SqlDbType.NVarChar).Value = cli.Nombre;
            //cmd.Parameters.Add("@telefono", System.Data.SqlDbType.NVarChar).Value = cli.Telefono;

            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
        }

        public bool ActualizarsEvent(sEvent cli)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "UPDATE  Evento set nombre_ev = @nombre_ev, detalle = @detalle, duracion = @duracion, fecha =@fecha, horaI = @horaI, horaF = @horaF, lugar = @lugar, horario = @horario, CUPO = @CUPO, fechaStart = @fechaStart, cedula = @cedula where id_ev =@id_ev";

            SqlCommand cmd = new SqlCommand(sql, con);



            cmd.Parameters.Add("@nombre_ev", System.Data.SqlDbType.NVarChar).Value = cli.nombre_ev;
            cmd.Parameters.Add("@detalle", System.Data.SqlDbType.NVarChar).Value = cli.detalle;
            cmd.Parameters.Add("@duracion", System.Data.SqlDbType.NVarChar).Value = cli.duracion;
            cmd.Parameters.Add("@fecha", System.Data.SqlDbType.NVarChar).Value = cli.fecha;
            cmd.Parameters.Add("@horaI", System.Data.SqlDbType.NVarChar).Value = cli.horaI;
            cmd.Parameters.Add("@horaF", System.Data.SqlDbType.NVarChar).Value = cli.horaF;
            cmd.Parameters.Add("@lugar", System.Data.SqlDbType.NVarChar).Value = cli.lugar;
            cmd.Parameters.Add("@horario", System.Data.SqlDbType.NVarChar).Value = cli.horario;
            cmd.Parameters.Add("@CUPO", System.Data.SqlDbType.NVarChar).Value = cli.CUPO;
            cmd.Parameters.Add("@fechaStart", System.Data.SqlDbType.NVarChar).Value = cli.fechaStart;
            cmd.Parameters.Add("@cedula", System.Data.SqlDbType.NVarChar).Value = cli.cedula; 
            cmd.Parameters.Add("@id_ev", System.Data.SqlDbType.NVarChar).Value = cli.id_ev;

            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
        }

        public sEvent ObtenerCliente(int id)
        {
            sEvent cli = null;

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "SELECT Nombre, Telefono FROM Clientes WHERE IdCliente = @idcliente";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@idcliente", System.Data.SqlDbType.NVarChar).Value = id;

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            if (reader.Read())
            {
                cli = new sEvent();

            }

            reader.Close();

            return cli;
        }

        public List<sEvent> ObtenerClientes()
        {
            List<sEvent> lista = new List<sEvent>();

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "SELECT IdCliente, Nombre, Telefono FROM Clientes";

            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                sEvent cli = new sEvent();

                cli = new sEvent();


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