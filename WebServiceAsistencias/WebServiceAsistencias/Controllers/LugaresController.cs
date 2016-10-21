using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServiceAsistencias.Models;

namespace ServicioWebRest.Controllers
{
    public class LugaresController : Controller
    {
        private LugarManager lugaresManager;

        public LugaresController()
        {
            lugaresManager = new LugarManager();
        }

        // GET /Api/Clientes
        [HttpGet]
        public JsonResult Lugares()
        {
            return Json(lugaresManager.ObtenerClientes(), 
                       JsonRequestBehavior.AllowGet);
        }

        // POST    Clientes/Lugar    { Nombre:"nombre", Telefono:123456789 }
        // PUT     Clientes/Lugar/3  { Id:3, Nombre:"nombre", Telefono:123456789 }
        // GET     Clientes/Lugar/3
        // DELETE  Clientes/Lugar/3
        public JsonResult Lugar(string place, Lugar item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(lugaresManager.InsertarLugar(item));
               /* case "PUT":
                    return Json(lugaresManager.ActualizarCliente(item));*/
                case "GET":
                    return Json(lugaresManager.ObtenerCliente(place), 
                                JsonRequestBehavior.AllowGet);
              //  case "DELETE":
                //    return Json(lugaresManager.EliminarCliente(id.GetValueOrDefault()));
            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }
    }

}

