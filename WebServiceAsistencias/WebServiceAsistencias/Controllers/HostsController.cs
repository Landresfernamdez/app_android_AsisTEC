using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServiceAsistencias.Models;

namespace WebServiceAsistencias.Controllers
{
    public class HostsController : Controller
    {
        private HostManager anfitrionesManager;

        public HostsController()
        {
            anfitrionesManager = new HostManager();
        }

        // GET /Api/Clientes
        

        public JsonResult Anfitrion(string id,string pass, Anfitrion item)
        {
            switch (Request.HttpMethod)
            {
                case "GET":
                    return Json(anfitrionesManager.ObtenerCliente(id,pass), 
                                JsonRequestBehavior.AllowGet);
            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }
    }

    }

