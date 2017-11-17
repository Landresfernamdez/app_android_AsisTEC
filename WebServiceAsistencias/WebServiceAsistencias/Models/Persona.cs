using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceAsistencias.Models
{
    public class Persona
    {
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string procedencia{ get; set; }
        public string edad { get; set; }
    }
}