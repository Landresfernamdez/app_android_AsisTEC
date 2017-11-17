package cr.ac.itcr.diego.webserviceretrofit.dto;

/**
 * Created by Andres on 8/17/2016.
 */
public class Usuarios {
    String cedula;
    String contraseña;

    public String getCedula() {
        return cedula;
    }

    public void setCedula(String cedula) {
        this.cedula = cedula;
    }

    public String getContraseña() {
        return contraseña;
    }

    public void setContraseña(String contraseña) {
        this.contraseña = contraseña;
    }
}
