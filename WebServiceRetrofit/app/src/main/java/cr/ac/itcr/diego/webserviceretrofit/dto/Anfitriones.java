package cr.ac.itcr.diego.webserviceretrofit.dto;

/**
 * Created by Andres on 8/17/2016.
 */
public class Anfitriones {
    String id;
    String password;

    public String getId(){
        return this.id;
    }
    public void setId(String Id){
        this.id=Id;
    }

    public String getPassword() {
        return password;
    }
    public void setPassword(String pass){
        this.password= pass;
    }
}
