package cr.ac.itcr.diego.webserviceretrofit.retrofit;

import java.util.ArrayList;

import cr.ac.itcr.diego.webserviceretrofit.dto.Anfitriones;
import cr.ac.itcr.diego.webserviceretrofit.dto.Estudiantes;
import cr.ac.itcr.diego.webserviceretrofit.dto.Eventos;
import cr.ac.itcr.diego.webserviceretrofit.dto.Registros;
import retrofit.Callback;
import retrofit.http.Body;
import retrofit.http.DELETE;
import retrofit.http.GET;
import retrofit.http.POST;
import retrofit.http.PUT;
import retrofit.http.Path;

/**
 * Created by Estudiante on 10/10/2015.
 */
public interface ServerApi {


    //////
    @GET("/Anfitriones/Anfitrion/{id}/{password}")
    void validarLogin(@Path("id")String id, @Path("password") String password, Callback<Anfitriones> usersCallback);

    @GET("/Eventos/Evento/{id}")
    void obtenerEventos(@Path("id")String id, Callback<ArrayList<Eventos>> usersCallback);

    @GET("/Events/Evento/{id}")
    void obtenerEvento(@Path("id")String id, Callback<ArrayList<Eventos>> usersCallback);

    @POST("/Registros/Registro")
    void postRegistrosInformation(@Body Registros post, Callback<Boolean> postCallback);

    @POST("/Registers/Registro")
    void postRegistrosSInformation(@Body Registros post, Callback<Boolean> postCallback);


    @POST("/Estudiantes/Estudiante")
    void putEstudiantesInformation(@Body Estudiantes estudiantes, Callback<Boolean> postCallback);


    @GET("/Estudiantes/Estudiante/{id}")
    void obtenerEstudiante(@Path("id")String id,Callback<Estudiantes> usersCallback);
}//webp comprimir imagenes/formato de google
