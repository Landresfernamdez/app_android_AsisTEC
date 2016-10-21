using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServiceAsistencias.Models;

namespace WebServiceAsistencias.Controllers
{
    public class AnfitrionesController : Controller
    {
        private AnfitrionManager anfitrionesManager;

        public AnfitrionesController()
        {
            anfitrionesManager = new AnfitrionManager();
        }

        // GET /Api/Clientes
        [HttpGet]
        public JsonResult Anfitriones()
        {
            return Json(anfitrionesManager.ObtenerClientes(), 
                        JsonRequestBehavior.AllowGet);
        }

        // POST    Clientes/Lugar    { Nombre:"nombre", Telefono:123456789 }
        // PUT     Clientes/Lugar/3  { Id:3, Nombre:"nombre", Telefono:123456789 }
        // GET     Clientes/Lugar/3
        // DELETE  Clientes/Lugar/3
        public JsonResult Anfitrion(string id,string pass, Anfitrion item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(anfitrionesManager.InsertarCliente(item));
             //   case "PUT":
                 //   return Json(anfitrionesManager.ActualizarCliente(item));
                case "GET":
                    return Json(anfitrionesManager.ObtenerCliente(id,pass), 
                                JsonRequestBehavior.AllowGet);
               // case "DELETE":
                //    return Json(anfitrionesManager.EliminarCliente(id.GetValueOrDefault()));
            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }
    }

    }

