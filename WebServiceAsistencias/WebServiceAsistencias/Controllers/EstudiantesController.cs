using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServiceAsistencias.Models;

namespace WebServiceAsistencias.Controllers
{
    public class EstudiantesController:Controller
    {
        private EstudianteManager estudiantesManager;

        public EstudiantesController()
        {
            estudiantesManager = new EstudianteManager();
        }

        // GET /Api/Clientes
        

        // POST    Clientes/Lugar    { Nombre:"nombre", Telefono:123456789 }
        // PUT     Clientes/Lugar/3  { Id:3, Nombre:"nombre", Telefono:123456789 }
        // GET     Clientes/Lugar/3
        // DELETE  Clientes/Lugar/3
        public JsonResult Estudiante(string id, Estudiante item)
        {
            switch (Request.HttpMethod)
            {
             //   case "POST":
               //     return Json(anfitrionesManager.InsertarCliente(item));
                case "POST":
                    return Json(estudiantesManager.ActualizarEstudiante(item));
                case "GET":
                    return Json(estudiantesManager.ObtenerEstudiante(id), 
                                JsonRequestBehavior.AllowGet);
               // case "DELETE":
                //    return Json(anfitrionesManager.EliminarCliente(id.GetValueOrDefault()));
            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }
    }
}