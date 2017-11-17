using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServiceAsistencias.Models;

namespace WebServiceAsistencias.Controllers
{
    public class ActividadesController : Controller
    {
        private ActividadManager eventosManager;

        public ActividadesController()
        {
            eventosManager = new ActividadManager();
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