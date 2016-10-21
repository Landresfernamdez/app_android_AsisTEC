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
        [HttpGet]
        public JsonResult Eventos()
        {
            return Json(eventosManager.ObtenerEventos(), 
                        JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult Evento(string id, string pass, Anfitrion item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(eventosManager.ObtenerEventosDeAnfitrion(id), JsonRequestBehavior.AllowGet);
                
                case "GET":
                    return Json(eventosManager.ObtenerEventosDeAnfitrion(id),
                                JsonRequestBehavior.AllowGet);

                    // case "DELETE":
                    //    return Json(anfitrionesManager.EliminarCliente(id.GetValueOrDefault()));
            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }
    }
}