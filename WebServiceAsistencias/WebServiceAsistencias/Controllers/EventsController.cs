using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServiceAsistencias.Models;

namespace ServicioWebRest.Controllers
{
    public class EventsController : Controller
    {
        private EventManager eventsManager;

        public EventsController()
        {
            eventsManager = new EventManager();
        }

        // GET /Api/Clientes
        [HttpGet]
        public JsonResult Events()
        {
            return Json(eventsManager.ObtenerClientes(), 
                        JsonRequestBehavior.AllowGet);
        }

        // POST    Clientes/Cliente    { Nombre:"nombre", Telefono:123456789 }
        // PUT     Clientes/Cliente/3  { Id:3, Nombre:"nombre", Telefono:123456789 }
        // GET     Clientes/Cliente/3
        // DELETE  Clientes/Cliente/3
        public JsonResult Event(string idA,string idB, Event item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(eventsManager.eliminarEvento(idA));

                case "GET":
                    return Json(eventsManager.ObtenerEventosFiltro(idA,idB), 
                                JsonRequestBehavior.AllowGet);
          
            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }
    }

    }

