using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WebServiceAsistencias.Models
{
    public class EventManager
    {
        private static string cadenaConexion =
        @"Data Source=172.24.43.105;Initial Catalog=AsiVenTEC;User Id=sa;Password=86374844botas";
        public List<Evento> ObtenerEvento(string id)
        {
            List<Evento> lista = new List<Evento>();

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "SELECT e.nombre_ev,e.id_ev,e.lugar,e.detalle,e.duracion,e.fecha,e.horario,e.horaI,e.horaF,e.CUPO from Eventos as e where e.id_ev=@idevent";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@idevent", System.Data.SqlDbType.NVarChar).Value = id;

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
                catch (Exception e)
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


    }
}