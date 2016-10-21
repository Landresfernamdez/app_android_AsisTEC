using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebServiceAsistencias.Models
{
    public class EventoManager
    {
        private static string cadenaConexion =
             @"Data Source=172.24.43.105;Initial Catalog=AsiVenTEC;User Id=sa;Password=86374844botas";
        public List<Evento> ObtenerEventosDeAnfitrion(string id)
        {
            List<Evento> lista = new List<Evento>();

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "select e.nombre_ev,ea.id_ev,e.lugar,e.detalle,e.duracion,e.fecha,e.horario,e.horaI,e.horaF,e.CUPO from (Eventos as e inner join Evento_Anfitriones as ea  on(e.id_ev=ea.id_ev and ea.cedAnfitrion=@idanfitrion ))";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@idanfitrion", System.Data.SqlDbType.NVarChar).Value = id;

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Evento evento = new Evento();
                evento = new Evento();
                evento.nombre_ev = reader.GetString(0);
                evento.id_ev = reader.GetString(1);
                evento.lugar = reader.GetString(2);
                try
                {
                    evento.detalle = reader.GetString(3);
                }
                catch(Exception e)
                {
                    evento.detalle = e.Message;
                    

                }
                evento.duracion = reader.GetString(4);
                evento.fecha = reader.GetDateTime(5).Date.ToString(); 
                evento.horario = reader.GetString(6);
                evento.horaI = reader.GetTimeSpan(7).ToString();
                evento.horaF = reader.GetTimeSpan(8).ToString();
                evento.CUPO = reader.GetInt32(9);
                lista.Add(evento);
            }
            reader.Close();
            return lista;
        }
        
        public List<Evento> ObtenerEventos()
        {
            List<Evento> lista = new List<Evento>();

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "select *from Eventos";

            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Evento Event= new Evento();

                Event = new Evento();
                Event.nombre_ev = reader.GetString(0);
                Event.id_ev= reader.GetString(1);
                try
                {
                    Event.detalle = reader.GetString(2);
                }
                catch (Exception e)
                {
                    Event.detalle =e.Message ;
                }
                Event.duracion = reader.GetString(3);
                Event.fecha = reader.GetDateTime(4).Date.ToString();
                Event.horaI = reader.GetTimeSpan(5).ToString();
                Event.horaF = reader.GetTimeSpan(6).ToString();
                Event.lugar = reader.GetString(7);
                Event.horario = reader.GetString(8);
                Event.CUPO = reader.GetInt32(9);
                lista.Add(Event);
            }
            reader.Close();
            return lista;
        }
        
    }
}