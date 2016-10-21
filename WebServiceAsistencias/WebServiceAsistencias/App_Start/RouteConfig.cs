using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebServiceAsistencias
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "AccesoLugar",
                "Lugares/Lugar/{place}",
                    new
                    {
                        controller = "Lugares",
                        action = "Lugar",
                        place = UrlParameter.Optional
                    }
                );
            routes.MapRoute(
                "AccesoPosLugares",
                "Lugares/Lugar/POS/{place}",
                    new
                    {
                        controller = "Lugares",
                        action = "Lugar",
                        place = UrlParameter.Optional
                    }
                );
            routes.MapRoute(
                "AccesoEvento",
                "Eventos/Evento/{id}",
                new
                {
                    controller = "Eventos",
                    action = "Evento",
                    id = UrlParameter.Optional,
                    pass = UrlParameter.Optional

                }
                );
            routes.MapRoute(
                "AccesoEventos",
                "Eventos",
                new
                {
                    controller = "Eventos",
                    action = "Eventos"
                }
            );
            routes.MapRoute(
                "AccesoRegistros",//Cambie de Registro a Registros
                "Registros",
                    new
                    {
                        controller ="Registros",
                        action = "Registros",
                        place = UrlParameter.Optional
                    }
                );
            routes.MapRoute(
                "AccesoRegisters",//Cambie de Registro a Registros
                "Registers",
                    new
                    {
                        controller = "Registers",
                        action = "Registers",
                        place = UrlParameter.Optional
                    }
                );
            routes.MapRoute(//Cambie evento por events
                "AccesoEvent",
                "Events/Evento/{id}",
                new
                {
                    controller ="Events",
                    action = "Evento",
                    id = UrlParameter.Optional,
                    pass = UrlParameter.Optional
                }
                );
                    routes.MapRoute(
                        "AccesoAnfitrion",
                        "Anfitriones/Anfitrion/{id}/{pass}",
                        new
                        {
                            controller = "Anfitriones",
                            action = "Anfitrion",
                            id = UrlParameter.Optional,
                            pass = UrlParameter.Optional

                        }
                        );
                    routes.MapRoute(
                                "AccesoEstudiante",
                                "Estudiantes/Estudiante/{id}",
                                new
                                {
                                    controller = "Estudiantes",
                                    action = "Estudiante",
                                    id = UrlParameter.Optional
                                }
                                );

                    routes.MapRoute(
                                "AccesoEstudiantes",
                                "Estudiantes",
                                new
                                {
                                    controller ="Estudiantes",
                                    action ="Estudiantes",
                                    id = UrlParameter.Optional
                                }
                                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{pass}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional,pass=UrlParameter.Optional,place= UrlParameter.Optional }
            );
        }
    }
}