package cr.ac.itcr.diego.webserviceretrofit.retrofit;

import java.util.ArrayList;

import cr.ac.itcr.diego.webserviceretrofit.dto.Events;
import cr.ac.itcr.diego.webserviceretrofit.dto.Personas;
import cr.ac.itcr.diego.webserviceretrofit.dto.Usuarios;
import cr.ac.itcr.diego.webserviceretrofit.dto.Eventos;
import cr.ac.itcr.diego.webserviceretrofit.dto.Registros;
import retrofit.Callback;
import retrofit.http.Body;
import retrofit.http.GET;
import retrofit.http.POST;
import retrofit.http.Path;

/**
 * Created by Estudiante on 10/10/2015.
 */
public interface ServerApi {
    //Se encarga de validar si el edecan existe
    @GET("/Edecanes/Edecan/{id}/{pass}")
    void validarLogin(@Path("id")String id, @Path("pass") String pass, Callback<Usuarios> usersCallback);
    //Se encarga de recuperar los eventos de un edecan
    @GET("/Eventos/Evento/{id}")
    void obtenerEvento(@Path("id")String id,Callback<ArrayList<Events>>usersCallback);

    @GET("/Actividades/Actividad/{ida}")
    void ObtenerActividadesDeAnfitrion(@Path("ida")String ida, Callback<ArrayList<Eventos>> usersCallback);


    @POST("/Registros/Registro")
    void postRegistrosInformation(@Body Registros post, Callback<Boolean> postCallback);

    @POST("/Registers/Registro")
    void postRegistrosSInformation(@Body Registros post, Callback<Boolean> postCallback);


    @POST("/Personas/Persona")
    void putEstudiantesInformation(@Body Personas personas, Callback<Boolean> postCallback);


    @GET("/Personas/Persona/{id}")
    void obtenerEstudiante(@Path("id")String id,Callback<Personas> usersCallback);
}//webp comprimir imagenes/formato de google
