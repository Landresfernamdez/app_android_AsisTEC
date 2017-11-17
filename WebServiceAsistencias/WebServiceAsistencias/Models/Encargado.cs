using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceAsistencias.Models
{
    public class Encargado
    {
        public string cedula { get; set; }
        public string nombre { get; set; }
        public String apellidos { get; set; }
        public String procedencia { get; set; }
        public  int edad  { get; set; }
        public String contraseña { get; set; }
        public String tipoCuenta { get; set; }
    }
}