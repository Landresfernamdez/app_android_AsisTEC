using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebServiceAsistencias.Models
{
    public class EventManager //Models views controllera  MVC4
    {
        private static string cadenaConexion =
          //@"Data Source=WIN-8TMISC6LAH5;Initial Catalog=DBCLIENTES;User Id=drojas;Password=drojas";//\SQLEXPRESS
          @"Data Source=172.24.29.16;Initial Catalog=EventosTEC;User Id=sa;Password=71a0ses3919";

        public bool InsertarCliente(Event cli)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "INSERT INTO Clientes (IdCliente,Nombre, Telefono) VALUES (@id,@nombre, @telefono)";


            

            con.Close();

            return true;
        }
        public bool eliminarEvento(string id)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "DELETE FROM Evento WHERE id_ev =@idEvento";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@idEvento", System.Data.SqlDbType.NVarChar).Value = id;

            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
        }
        public List<Evento> ObtenerEventosFiltro(string idA, string idB)
        {
            List<Evento> lista = new List<Evento>();

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();
            string sql = "select e.id_ev, e.nombre_ev,e.detalle,e.duracion,e.fecha,e.horaI,e.horaF,e.lugar,e.horario,e.CUPO,e.fechaStart,e.cedula,p.nombre,p.apellidos,e.estado from Evento as e inner join Personas as p on (p.cedula=e.cedula) where " + idA + "= @idB";            //if (id.Equals("id_ev"))
            //{
            //    sql = "select id_ev from Evento";

            //}
            //else if (id.Equals("nombre_ev"))
            //{
            //    sql = "select nombre_ev from Evento";
            //}


            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@idB", System.Data.SqlDbType.NVarChar).Value = idB;

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Evento Event = new Evento();

                Event = new Evento();

                //Event.id_ev = reader.GetString(0);
                Event.nombre_ev = reader.GetString(1) + "°" + reader.GetInt32(0).ToString();
                Event.detalle = reader.GetString(2) + "°" + reader.GetString(12) + "  " + reader.GetString(13);
                Event.duracion = reader.GetString(3);
                Event.fecha = reader.GetDateTime(4).Day.ToString() + "/" + reader.GetDateTime(4).Month.ToString() + "/" + reader.GetDateTime(4).Year.ToString();
                Event.horaI = reader.GetTimeSpan(5).ToString();
                Event.horaF = reader.GetTimeSpan(6).ToString();
                Event.lugar = reader.GetString(7).ToString();
                Event.horario = reader.GetString(8).ToString();
                Event.CUPO = reader.GetInt32(9);
                Event.fechaStart = reader.GetDateTime(10).Day.ToString() + "/" + reader.GetDateTime(10).Month.ToString() + "/" + reader.GetDateTime(10).Year.ToString();
                Event.cedula = reader.GetString(11) + "°" + reader.GetString(14);






                lista.Add(Event);
            }

            reader.Close();

            return lista;
        }


        public Event ObtenerCliente(int id)
        {
            Event cli = null;

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "SELECT Nombre, Telefono FROM Clientes WHERE IdCliente = @idcliente";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@idcliente", System.Data.SqlDbType.NVarChar).Value = id;

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            if (reader.Read())
            {
                cli = new Event();
                //cli.Id = id;
                //cli.Nombre = reader.GetString(0);
                //cli.Telefono = reader.GetString(1);
            }

            reader.Close();

            return cli;
        }

        public List<Event> ObtenerClientes()
        {
            List<Event> lista = new List<Event>();

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