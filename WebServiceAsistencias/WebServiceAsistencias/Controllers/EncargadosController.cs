using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServiceAsistencias.Models;

namespace ServicioWebRest.Controllers
{
    public class EncargadosController : Controller
    {
        private EncargadoManager encargadosManager;

        public EncargadosController()
        {
            encargadosManager = new EncargadoManager();
        }

        // GET /Api/Clientes
        [HttpGet]
        public JsonResult Encargados()
        {
            return Json(encargadosManager.ObtenerEncargados(), 
                        JsonRequestBehavior.AllowGet);
        }

        // POST    Clientes/Cliente    { Nombre:"nombre", Telefono:123456789 }
        // PUT     Clientes/Cliente/3  { Id:3, Nombre:"nombre", Telefono:123456789 }
        // GET     Clientes/Cliente/3
        // DELETE  Clientes/Cliente/3
        public JsonResult Encargado(string id,Encargado item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(encargadosManager.ModEdecan(item));
                case "PUT":
                    return Json(encargadosManager.ActualizarCliente(item));
                //case "GET":
                //    return Json(encargadosManager.ObtenerCliente(id.GetValueOrDefault()), 
                //                JsonRequestBehavior.AllowGet);
                //case "DELETE":
                //    return Json(encargadosManager.EliminarCliente(id.GetValueOrDefault()));
            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }
    }

    }

