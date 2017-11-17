using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServiceAsistencias.Models;

namespace WebServiceAsistencias.Controllers
{
    public class sEventsController : Controller
    {
        private sEventManager seventsManager;

        public sEventsController()
        {
            seventsManager = new sEventManager();
        }

        // GET /Api/Clientes
        [HttpGet]
        public JsonResult sEvents()
        {
            return Json(seventsManager.ObtenerClientes(), 
                        JsonRequestBehavior.AllowGet);
        }

        // POST    Clientes/Cliente    { Nombre:"nombre", Telefono:123456789 }
        // PUT     Clientes/Cliente/3  { Id:3, Nombre:"nombre", Telefono:123456789 }
        // GET     Clientes/Cliente/3
        // DELETE  Clientes/Cliente/3
        public JsonResult sEvent(int? id, sEvent item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(seventsManager.ActualizarsEvent(item));
                case "PUT":
                    return Json(seventsManager.ActualizarsEvent(item));
                case "GET":
                    return Json(seventsManager.ObtenerCliente(id.GetValueOrDefault()), 
                                JsonRequestBehavior.AllowGet);
                case "DELETE":
                    return Json(seventsManager.EliminarCliente(id.GetValueOrDefault()));
            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }
    }

    }

