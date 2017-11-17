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
           //@"Data Source=WIN-8TMISC6LAH5;Initial Catalog=DBCLIENTES;User Id=drojas;Password=drojas";//\SQLEXPRESS
           @"Data Source=172.24.29.16;Initial Catalog=EventosTEC;User Id=sa;Password=71a0ses3919";




        /* public Curso ObtenerEstudiante(int id)
         {
             Curso cur = null;

             SqlConnection con = new SqlConnection(cadenaConexion);

             con.Open();


             string sql = "select p.per_ced ,p.nombre ,e.carne , e.carrera  from Personas p,Estudiantes e where p.per_ced=e.per_ced and e.carne =@idcliente";

             SqlCommand cmd = new SqlCommand(sql, con);

             cmd.Parameters.Add("@idcliente", System.Data.SqlDbType.NVarChar).Value = id;

             SqlDataReader reader =
                 cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

             if (reader.Read())
             {
                 cli = new Estudiante();
                 cli.Id = reader.GetInt32(0); ;
                 cli.Nombre = reader.GetString(1);
                 cli.Carne = id;
                 cli.Carrera = reader.GetString(3);
             }

             reader.Close();

             return cli;
         }*/
        public bool InsertarEvento(Evento evt)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "insert into Evento(nombre_ev,detalle,duracion,fecha,horaI,horaF,lugar,horario,CUPO,fechaStart,cedula) values (@nombre_ev, @detalle, @duracion, @fecha, @horaI, @horaF, @lugar, @horario, @CUPO, @fechaStart,@cedula)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@nombre_ev", System.Data.SqlDbType.NVarChar).Value = evt.nombre_ev;
            cmd.Parameters.Add("@detalle", System.Data.SqlDbType.NVarChar).Value = evt.detalle;
            cmd.Parameters.Add("@duracion", System.Data.SqlDbType.NVarChar).Value = evt.duracion;
            cmd.Parameters.Add("@fecha", System.Data.SqlDbType.NVarChar).Value = evt.fecha;
            cmd.Parameters.Add("@horaI", System.Data.SqlDbType.NVarChar).Value = evt.horaI;
            cmd.Parameters.Add("@horaF", System.Data.SqlDbType.NVarChar).Value = evt.horaF;
            cmd.Parameters.Add("@lugar", System.Data.SqlDbType.NVarChar).Value = evt.lugar;
            cmd.Parameters.Add("@horario", System.Data.SqlDbType.NVarChar).Value = evt.horario;
            cmd.Parameters.Add("@CUPO", System.Data.SqlDbType.Int).Value = evt.CUPO;
            cmd.Parameters.Add("@fechaStart", System.Data.SqlDbType.NVarChar).Value = evt.fechaStart;
            cmd.Parameters.Add("@cedula", System.Data.SqlDbType.NVarChar).Value = evt.cedula;




            int res = cmd.ExecuteNonQuery();

            con.Close();

            return (res == 1);
        }
        public List<Evento> ObtenerEventosFiltro(string idA,string idB)
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
        public List<Dato> ObtenerEventosDeAnfitrion(string id)
        {
            List<Dato> lista = new List<Dato>();

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();
            string sql = "select "+id+" from Evento";
            //if (id.Equals("id_ev"))
            //{
            //    sql = "select id_ev from Evento";

            //}
            //else if (id.Equals("nombre_ev"))
            //{
            //    sql = "select nombre_ev from Evento";
            //}
            

            SqlCommand cmd = new SqlCommand(sql, con);
        //    cmd.Parameters.Add("@idcliente", System.Data.SqlDbType.NVarChar).Value = id;

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Dato evento = new Dato();
                evento = new Dato();
                try
                {
                    evento.dato = reader.GetInt32(0).ToString();

                }
                catch(Exception e)
                {
                    evento.dato = reader.GetString(0).ToString();

                }
                



                //evento.duracion = reader.GetString(4);
                //evento.fecha = reader.GetDateTime(5).Date.ToString(); 
                //evento.horario = reader.GetString(6);
                //evento.horaI = reader.GetDateTime(7).Hour.ToString()+":"+ reader.GetDateTime(7).Minute.ToString()+":"+ reader.GetDateTime(7).Second.ToString();
                //evento.horaF = reader.GetDateTime(8).Hour.ToString() + ":" + reader.GetDateTime(8).Minute.ToString() + ":" + reader.GetDateTime(8).Second.ToString();
                //evento.CUPO = reader.GetInt32(9);
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

            string sql = "select e.id_ev, e.nombre_ev,e.detalle,e.duracion,e.fecha,e.horaI,e.horaF,e.lugar,e.horario,e.CUPO,e.fechaStart,e.cedula,p.nombre,p.apellidos,e.estado from Evento as e inner join Personas as p on (p.cedula=e.cedula)";

            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Evento Event= new Evento();

                Event = new Evento();
                
                //Event.id_ev = reader.GetString(0);
                Event.nombre_ev= reader.GetString(1)+"°"+reader.GetInt32(0).ToString();
                Event.detalle = reader.GetString(2) + "°" + reader.GetString(12) + "  " + reader.GetString(13);
                Event.duracion = reader.GetString(3);
                Event.fecha = reader.GetDateTime(4).Day.ToString() + "/" + reader.GetDateTime(4).Month.ToString() + "/" + reader.GetDateTime(4).Year.ToString();
                Event.horaI = reader.GetTimeSpan(5).ToString();
                Event.horaF = reader.GetTimeSpan(6).ToString();
                Event.lugar = reader.GetString(7).ToString();
                Event.horario = reader.GetString(8).ToString();
                Event.CUPO = reader.GetInt32(9);
                Event.fechaStart= reader.GetDateTime(10).Day.ToString() + "/" + reader.GetDateTime(10).Month.ToString() + "/" + reader.GetDateTime(10).Year.ToString();
                Event.cedula = reader.GetString(11) + "°" + reader.GetString(14);

               


 

                lista.Add(Event);
            }

            reader.Close();

            return lista;
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
        public List<Evento> ObtenerCursosDeProf(int id)
        {
            List<Evento> lista = new List<Evento>();

            SqlConnection con = new SqlConnection(cadenaConexion);

            con.Open();

            string sql = "select c.codc, c.nombrecur from (grupos as g inner join cursos as c  on(g.per_ced=@idcliente and c.codC=g.codC ))";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@idcliente", System.Data.SqlDbType.NVarChar).Value = id;

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Evento cur = new Evento();

                cur = new Evento();
              //  cur.IdCur = reader.GetString(0);
              //  cur.NombreCur = reader.GetString(1);


                lista.Add(cur);
            }

            reader.Close();

            return lista;
        }
    }
}