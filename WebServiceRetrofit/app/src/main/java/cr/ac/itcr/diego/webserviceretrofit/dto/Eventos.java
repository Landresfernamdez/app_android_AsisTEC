package cr.ac.itcr.diego.webserviceretrofit.dto;

/**
 * Created by Andres on 9/8/2016.
 */
public class Eventos {
    String id_ev;
    String nombre_ev;
    String detalle;
    String duracion;
    String fecha;
    String horaI;
    String horaF;
    String lugar;
    String horario;
    String CUPO;

    public String getId_ev() {
        return id_ev;
    }

    public String getNombre_ev() {
        return nombre_ev;
    }

    public String getDetalle() {
        return detalle;
    }

    public String getDuracion() {
        return duracion;
    }

    public String getFecha() {
        return fecha;
    }

    public String getHoraI() {
        return horaI;
    }

    public String getHoraF() {
        return horaF;
    }

    public String getLugar() {
        return lugar;
    }

    public String getHorario() {
        return horario;
    }

    public String getCUPO() {
        return CUPO;
    }
}
