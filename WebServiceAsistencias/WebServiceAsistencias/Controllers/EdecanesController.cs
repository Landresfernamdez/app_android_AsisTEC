using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServiceAsistencias.Models;

namespace WebServiceAsistencias.Controllersa
{
    public class EdecanesController : Controller
    {
        private EdecanManager edecanesManager;

        public EdecanesController()
        {
            edecanesManager = new EdecanManager();
        }

        // GET /Api/Clientes
        [HttpGet]
        public JsonResult Edecanes()
        {
            return Json(edecanesManager.ObtenerEdecanes(), 
                        JsonRequestBehavior.AllowGet);
        }

        // POST    Clientes/Cliente    { Nombre:"nombre", Telefono:123456789 }
        // PUT     Clientes/Cliente/3  { Id:3, Nombre:"nombre", Telefono:123456789 }
        // GET     Clientes/Cliente/3
        // DELETE  Clientes/Cliente/3
        public JsonResult Edecan(string id, Edecan item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(edecanesManager.eliminarEdecan(id));
                case "PUT":
                    return Json(edecanesManager.ActualizarCliente(item));
                case "GET":
                    return Json(edecanesManager.consultarEdecan(id), 
                                JsonRequestBehavior.AllowGet);

            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }
    }

    }

