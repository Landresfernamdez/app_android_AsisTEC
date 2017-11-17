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
            "AccesoActividad",
            "Actividades/Actividad/{id}",
            new
            {
                controller = "Actividades",
                action = "Evento",
                id = UrlParameter.Optional,
                pass = UrlParameter.Optional

            }
            );

            routes.MapRoute(
            "AccesoActividades",
            "Actividades",
            new
            {
                controller = "Actividades",
                action = "Actividades"
            }
        );
            routes.MapRoute(
                "AccesoRegistros",//Cambie de Registro a Registros
                "Registros",
                    new
                    {
                        controller = "Registros",
                        action = "Registros",
                        place = UrlParameter.Optional
                    }
                );
             routes.MapRoute(
            "AccesoRegisters",
            "Registers",
                new
                {
                    controller = "Registers",
                    action = "Registers",
                    place = UrlParameter.Optional
                }
            );
            routes.MapRoute(
                "AccesoActivitys",
                "Activitys/Activity/{id}",
                new
                {
                    controller = "Activitys",
                    action = "Evento",
                    id = UrlParameter.Optional,
                    pass = UrlParameter.Optional
                }
                );

            routes.MapRoute(
                "AccesoHost",
                "Hosts/Host/{id}/{pass}",
                new
                {
                    controller = "Hosts",
                    action = "Anfitrion",
                    id = UrlParameter.Optional,
                    pass = UrlParameter.Optional

                }
                );
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
                "AccesoEvento",
                "Eventos/Evento/{id}",
                    new
                    {
                        controller = "Eventos",
                        action = "Evento",
                        id = UrlParameter.Optional
                     
                    }
                );

            routes.MapRoute(
            "AccesoEdecan",
            "Edecanes/Edecan/{id}",
                new
                {
                    controller = "Edecanes",
                    action = "Edecan",
                    id = UrlParameter.Optional

                }
            );

            routes.MapRoute(
                "AccesosEvent",
                "sEvents/sEvent",
                    new
                    {
                        controller = "sEvents",
                        action = "sEvent",
                        id = UrlParameter.Optional

                    }
                );
            routes.MapRoute(
            "AccesoEvent",
            "Events/Event/{idA}/{idB}",
                new
                {
                    controller = "Events",
                    action = "Event",
                    idA = UrlParameter.Optional,
                    idB = UrlParameter.Optional

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
                controller = "Estudiantes",
                action = "Estudiantes",
                id = UrlParameter.Optional
            }
            );
            routes.MapRoute(
            "AccesoLugares",
            "Lugares",
            new
            {
                controller = "Lugares",
                action = "Lugares"
            }
        );
            routes.MapRoute(
                "AccesoEncargados",
                "Encargados",
                new
                {
                    controller = "Encargados",
                    action = "Encargados"
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
                "AccesoEdecanes",
                "Edecanes",
                new
                {
                    controller = "Edecanes",
                    action = "Edecanes"
                }
            );







            routes.MapRoute(
                "AccesoHorario",
                "Horarios/{id}",
                    new
                    {
                        controller = "Horarios",
                        action = "Horario",
                        id = UrlParameter.Optional
                    }
                );

            routes.MapRoute(
                "AccesoHorarios",
                "Horarios",
                    new
                    {
                        controller = "Horarios",
                        action = "Horario",
                        id = UrlParameter.Optional
                    }
                );
            routes.MapRoute(
                "AccesoFecha",
                "Fechas/{idRef}/{idC}",
                    new
                    {
                        controller = "Fechas",
                        action = "Fecha",
                        idRef = UrlParameter.Optional,
                        idC = UrlParameter.Optional
                    }
             );

                    routes.MapRoute(
                    "AccesoCurso",
                    "Cursos/{id}",
                        new
                        {
                            controller = "Cursos",
                            action = "Curso",
                            id = UrlParameter.Optional
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
                "AccesoEncargado",
                "Encargados/Encargado/{id}",
                new
                {
                    controller = "Encargados",
                    action = "Encargado",
                    id = UrlParameter.Optional,
                //    pass = UrlParameter.Optional

                }


                );

            routes.MapRoute(
                "AccesoProfesor",
                "Profesores/Profesor/{id}/{password}",
                    new
                    {
                        controller = "Profesores",
                        action = "Profesor",
                        id = UrlParameter.Optional,
                        password=UrlParameter.Optional
                    
                    }
                );


            routes.MapRoute(
                "AccesoPersona",
                "Personas/Persona/{id}",
                    new
                    {
                        controller = "Personas",
                        action = "Persona",
                        id = UrlParameter.Optional
                    }
                );
            //  int cod_ref, String codC,String fecha
            routes.MapRoute(
             "AccesoStudents",
             "Students/{cod_ref}/{codC}/{fecha}",
             new
             {
                 controller = "Students",
                 action = "Student",
                 cod_ref = UrlParameter.Optional,
                 codC= UrlParameter.Optional,
                 fecha = UrlParameter.Optional
             }
            );
       routes.MapRoute(
        "AccesoEstudianteGrupos",
        "Estudiantes/{id}",
        new
        {
            controller = "EstudiantesGrupos",
            action = "Estudiante",
            id = UrlParameter.Optional,
            password = UrlParameter.Optional
        }
    );
            routes.MapRoute(
                "AccesoEstudiantesGrupos",
                "Estudiantes",
                new
                {
                    controller = "EstudiantesGrupos",
                    action = "Estudiantes"
                }
            );

            routes.MapRoute(
                "AccesoProfesores",
                "Profesores",
                new
                {
                    controller = "Profesores",
                    action = "Profesores"
                }
            );
            routes.MapRoute(
                "AccesoPersonas",
                "Personas",
                new
                {
                    controller = "Personas",
                    action = "Personas"
                }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{pass}",
                defaults: new { controller = "Home", action = "Index", idB = UrlParameter.Optional, id = UrlParameter.Optional,pass=UrlParameter.Optional,place= UrlParameter.Optional }
            );
        }
    }
}