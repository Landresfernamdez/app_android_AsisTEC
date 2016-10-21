using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServiceAsistencias.Models;

namespace WebServiceAsistencias.Controllers
{
    public class EventsController : Controller
    {
        private EventManager eventsManager;

        public EventsController()
        {
            eventsManager = new EventManager();
        }

        public JsonResult Evento(string id, string pass, Anfitrion item)
        {
            switch (Request.HttpMethod)
            {
                
                case "GET":
                    return Json(eventsManager.ObtenerEvento(id), JsonRequestBehavior.AllowGet);
                    
            }
            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }
    }
}