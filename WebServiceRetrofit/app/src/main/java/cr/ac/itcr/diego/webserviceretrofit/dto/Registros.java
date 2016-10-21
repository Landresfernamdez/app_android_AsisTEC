package cr.ac.itcr.diego.webserviceretrofit.dto;

/**
 * Created by Andres on 9/20/2016.
 */
public class Registros {
    String id_ev;
    String carne;
    String fecha;
    String hora;

    public String getId_ev() {
        return id_ev;
    }

    public void setId_ev(String id_ev) {
        this.id_ev = id_ev;
    }

    public String getCarne() {
        return carne;
    }

    public void setCarne(String carne) {
        this.carne = carne;
    }

    public String getFecha() {
        return fecha;
    }

    public void setFecha(String fecha) {
        this.fecha = fecha;
    }

    public String getHora() {
        return hora;
    }

    public void setHora(String hora) {
        this.hora = hora;
    }
}
