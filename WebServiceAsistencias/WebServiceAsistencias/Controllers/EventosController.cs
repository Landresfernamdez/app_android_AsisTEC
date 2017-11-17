using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServiceAsistencias.Models;

namespace WebServiceAsistencias.Controllers
{
    public class EventosController : Controller
    {
        private EventoManager eventosManager;

        public EventosController()
        {
            eventosManager = new EventoManager();
        }

        // GET /Api/Clientes
        [HttpGet]
        public JsonResult Eventos()
        {
            return Json(eventosManager.ObtenerEventos(), 
                        JsonRequestBehavior.AllowGet);
        }

        // POST    Clientes/Lugar    { Nombre:"nombre", Telefono:123456789 }
        // PUT     Clientes/Lugar/3  { Id:3, Nombre:"nombre", Telefono:123456789 }
        // GET     Clientes/Lugar/3
        // DELETE  Clientes/Lugar/3
        public JsonResult Evento(string id,Evento item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(eventosManager.InsertarEvento(item));
                //   case "PUT":
                //   return Json(anfitrionesManager.ActualizarCliente(item));
                case "GET":
                    return Json(eventosManager.ObtenerEventosDeAnfitrion(id),
                                JsonRequestBehavior.AllowGet);
                case "PUT":
           
                    return Json(eventosManager.ObtenerEventosDeAnfitrion(id),
                                JsonRequestBehavior.AllowGet);

                case "DELETE":
                     return Json(eventosManager.eliminarEvento(id));
            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }
    }
}