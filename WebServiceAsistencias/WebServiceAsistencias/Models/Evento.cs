﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceAsistencias.Models
{
    public class Evento
    {
        public string id_ev { get; set; }
        public string nombre_ev { get; set; }
        public string detalle { get; set; }
        public string duracion { get; set; }
        public string fecha { get; set; }
        public string horaI { get; set; }
        public string horaF { get; set; }
        public string lugar{ get; set; }
        public string horario { get; set; }
        public int  CUPO { get; set; }


        


    }
}