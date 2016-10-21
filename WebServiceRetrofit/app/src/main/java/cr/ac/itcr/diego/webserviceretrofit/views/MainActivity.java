package cr.ac.itcr.diego.webserviceretrofit.views;
import android.content.Context;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;
import android.app.Activity;
import android.view.MenuItem;

import com.squareup.okhttp.OkHttpClient;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;

import cr.ac.itcr.diego.webserviceretrofit.R;
import cr.ac.itcr.diego.webserviceretrofit.dto.Anfitriones;
import cr.ac.itcr.diego.webserviceretrofit.dto.Eventos;
import cr.ac.itcr.diego.webserviceretrofit.dto.Registros;
import cr.ac.itcr.diego.webserviceretrofit.retrofit.ServerApi;
import retrofit.Callback;
import retrofit.RestAdapter;
import retrofit.RetrofitError;
import retrofit.client.OkClient;
import retrofit.client.Response;

public class MainActivity extends AppCompatActivity  {
    String baseurl = "http://172.24.43.105";//"http://172.24.42.5";
    public static ServerApi serverApi;
    private EditText txtId,txtNombre;
    private ListView menu;
    private String idGenerico;
    ArrayList<Eventos>listaEventos;
    private ListView menuEventos;
    private String IdEventClick;
    private Button btnStartlist;
    private Button btnOut;
    private TextView tvid;
    private TextView tvnombre;
    private TextView tvplace;
    private TextView tvdate;
    private TextView tvcupos;
    private TextView tvhI;
    private TextView tvhF;
    private TextView tvdetalles;
    public static  String id_ev;
    public static  String hore_E;
    public static  String hore_S;
    public static  String date;
    public static  String nombre;
    public static  String place;
    public static  String cup;
    public static  String detall;
    String [] opciones={"Eventos disponibles"};
    /*useLibrary 'org.apache.http.legacy'
    * @SuppressWarnings("deprecation")*/
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        txtId = (EditText)findViewById(R.id.txtId);
        txtNombre = (EditText)findViewById(R.id.txtNombre);
        listaEventos=new ArrayList<Eventos>();
    }
    public void regresaLogin(View view){
        setContentView(R.layout.activity_main);
    }
    public void cargaMenu(){
        String [] opcioness={"Eventos disponibles"};
        ArrayAdapter<String> array=new ArrayAdapter<String>(getApplication(),android.R.layout.simple_list_item_1,opcioness);
        menu=(ListView)findViewById(R.id.menu);
        menu.setAdapter(array);
                          }
    public void regresaEventosDisponibles(View view){
        setContentView(R.layout.main_events);
        serverApi.obtenerEventos(idGenerico, new Callback<ArrayList<Eventos>>() {
            @Override
            public void success(ArrayList<Eventos> eventoss, Response response) {
                listaEventos = eventoss;
                menuEventos = (ListView) findViewById(R.id.listEvents);
                menuEventos.setAdapter(new ViewAdapterEventos(MainActivity.this));
                menuEventos.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> parent, View view, final int position, long id) {
                        if (listaEventos != null) {
                            IdEventClick = listaEventos.get(position).getId_ev();
                            serverApi.obtenerEvento(IdEventClick, new Callback<ArrayList<Eventos>>() {
                                @Override
                                public void success(ArrayList<Eventos> eventos, Response response) {
                                    setContentView(R.layout.infoevent);
                                    btnStartlist = (Button) findViewById(R.id.btnstartlist);
                                    //Se enlazan los text view
                                    tvid = (TextView) findViewById(R.id.tvId);
                                    tvnombre = (TextView) findViewById(R.id.tvname);
                                    tvplace = (TextView) findViewById(R.id.tvplace);
                                    tvcupos = (TextView) findViewById(R.id.tvcupos);
                                    tvdate = (TextView) findViewById(R.id.tvdate);
                                    tvhI = (TextView) findViewById(R.id.tvhinicio);
                                    tvhF = (TextView) findViewById(R.id.tvhfinal);

                                    tvdetalles = (TextView) findViewById(R.id.tvdetalles);
                                    id_ev = eventos.get(position).getId_ev();
                                    hore_E = eventos.get(position).getHoraI();
                                    hore_S = eventos.get(position).getHoraF();
                                    date = eventos.get(position).getFecha();

                                    tvid.setText(eventos.get(position).getId_ev());
                                    tvnombre.setText(eventos.get(position).getNombre_ev());
                                    tvplace.setText(eventos.get(position).getLugar());
                                    tvdate.setText(eventos.get(position).getFecha().substring(0, 10));
                                    tvcupos.setText(eventos.get(position).getCUPO());
                                    tvhI.setText(eventos.get(position).getHoraI());
                                    tvhF.setText(eventos.get(position).getHoraF());
                                    tvdetalles.setText(eventos.get(position).getDetalle());
                                }

                                @Override
                                public void failure(RetrofitError error) {
                                    Toast.makeText(getApplicationContext(), error.getMessage().toString(), Toast.LENGTH_LONG).show();
                                }
                            });

                        }
                    }
                });
            }

            @Override
            public void failure(RetrofitError error) {
                Toast.makeText(getApplicationContext(), error.getMessage().toString(), Toast.LENGTH_LONG).show();
            }
        });
    }
    public void returninfoevent(View view){
        serverApi.obtenerEvento(IdEventClick, new Callback<ArrayList<Eventos>>() {
            @Override
            public void success(ArrayList<Eventos> eventos, Response response) {
                setContentView(R.layout.infoevent);
                btnStartlist=(Button)findViewById(R.id.btnstartlist);
                btnOut=(Button)findViewById(R.id.btnOut);
                //Se enlazan los text view
                tvid=(TextView)findViewById(R.id.tvId);
                tvnombre=(TextView)findViewById(R.id.tvname);
                tvplace=(TextView)findViewById(R.id.tvplace);
                tvcupos=(TextView)findViewById(R.id.tvcupos);
                tvdate=(TextView)findViewById(R.id.tvdate);
                tvhI=(TextView)findViewById(R.id.tvhinicio);
                tvhF=(TextView)findViewById(R.id.tvhfinal);
                tvdetalles=(TextView)findViewById(R.id.tvdetalles);

                tvid.setText(id_ev);//
                tvnombre.setText(nombre);
                tvplace.setText(place);
                tvdate.setText(date.substring(0, 10));//
                tvcupos.setText(cup);
                tvhI.setText(hore_E);//
                tvhF.setText(hore_S);//
                tvdetalles.setText(detall);
            }
            @Override
            public void failure(RetrofitError error) {
                Toast.makeText(getApplicationContext(), error.getMessage().toString(), Toast.LENGTH_LONG).show();
            }
        });
    }
    public void generaMENU(View view){
        setContentView(R.layout.login_layout);
        menu=(ListView)findViewById(R.id.menu);
        final ArrayAdapter<String> array=new ArrayAdapter<String>(getApplication(),android.R.layout.simple_list_item_1,opciones);
        menu.setAdapter(array);
        menu.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                if (position == 0) {
                    setContentView(R.layout.main_events);
                    serverApi.obtenerEventos(idGenerico, new Callback<ArrayList<Eventos>>() {
                        @Override
                        public void success(ArrayList<Eventos> eventoss, Response response) {
                            listaEventos = eventoss;
                            menuEventos = (ListView) findViewById(R.id.listEvents);
                            menuEventos.setAdapter(new ViewAdapterEventos(MainActivity.this));
                            menuEventos.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                                @Override
                                public void onItemClick(AdapterView<?> parent, View view, final int position, long id) {
                                    if (listaEventos != null) {
                                        IdEventClick = listaEventos.get(position).getId_ev();
                                        serverApi.obtenerEvento(IdEventClick, new Callback<ArrayList<Eventos>>() {
                                            @Override
                                            public void success(ArrayList<Eventos> eventos, Response response) {
                                                 setContentView(R.layout.infoevent);
                                                 btnStartlist=(Button)findViewById(R.id.btnstartlist);
                                                 btnOut=(Button)findViewById(R.id.btnOut);
                                                //Se enlazan los text view
                                                tvid=(TextView)findViewById(R.id.tvId);
                                                tvnombre=(TextView)findViewById(R.id.tvname);
                                                tvplace=(TextView)findViewById(R.id.tvplace);
                                                tvcupos=(TextView)findViewById(R.id.tvcupos);
                                                tvdate=(TextView)findViewById(R.id.tvdate);
                                                tvhI=(TextView)findViewById(R.id.tvhinicio);
                                                tvhF=(TextView)findViewById(R.id.tvhfinal);

                                                tvdetalles=(TextView)findViewById(R.id.tvdetalles);
                                                id_ev=eventos.get(position).getId_ev();
                                                hore_E=eventos.get(position).getHoraI();
                                                hore_S=eventos.get(position).getHoraF();
                                                date=eventos.get(position).getFecha();

                                                nombre=eventos.get(position).getNombre_ev();
                                                place=eventos.get(position).getLugar();
                                                cup=eventos.get(position).getCUPO();
                                                detall=eventos.get(position).getDetalle();

                                                tvid.setText(eventos.get(position).getId_ev());
                                                tvnombre.setText(eventos.get(position).getNombre_ev());
                                                tvplace.setText(eventos.get(position).getLugar());
                                                tvdate.setText(eventos.get(position).getFecha().substring(0,10));
                                                tvcupos.setText(eventos.get(position).getCUPO());
                                                tvhI.setText(eventos.get(position).getHoraI());
                                                tvhF.setText(eventos.get(position).getHoraF());
                                                tvdetalles.setText(eventos.get(position).getDetalle());
                                            }
                                            @Override
                                            public void failure(RetrofitError error) {
                                                Toast.makeText(getApplicationContext(), error.getMessage().toString(), Toast.LENGTH_LONG).show();
                                            }
                                        });

                                    }
                                }
                            });
                        }
                        @Override
                        public void failure(RetrofitError error) {
                            Toast.makeText(getApplicationContext(), error.getMessage().toString(), Toast.LENGTH_LONG).show();
                        }
                    });
                                   }
            }
        });
    }
    public class ViewAdapterEventos extends BaseAdapter {
        LayoutInflater mInflater;
        public ViewAdapterEventos(Context context) {
            mInflater = LayoutInflater.from(context);
        }
        @Override
        public int getCount() {
            return listaEventos.size();
        }
        @Override
        public Object getItem(int position) {
            return listaEventos.get(position);
        }
        @Override
        public long getItemId(int position) {
            return position;
        }
        @Override
        public View getView(final int position, View convertView, ViewGroup parent) {
            try{
                if (convertView == null) {
                    convertView = mInflater.inflate(R.layout.apoyoevents,null);
                }
                final TextView idText = (TextView) convertView.findViewById(R.id.tvEvents);
                idText.setText("Evento:"+ listaEventos.get(position).getNombre_ev());
                return convertView;
            }
            catch (NumberFormatException io)
            {
                Toast.makeText(getApplicationContext(), io.getMessage().toString(), Toast.LENGTH_LONG).show();
            }//Se retorna la vista
            return convertView;
        }
    }
    public void Iniciarlectura(final View view){
        btnStartlist.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    //Validar si puede tomar la lista osea si la fecha actual esta en el rango del evento
                    String HoraE=date.substring(0,10)+" "+hore_E;//Variable de hora de entrada al evento
                    HoraE=HoraE.replace("/","-");//Se encarga de reemplazar para que sirva el comparar la fecha por el tipo de formato
                    String HoraS=date.substring(0,10)+" "+hore_S;//Variable de hora de salida al evento
                    HoraS=HoraS.replace("/","-");
                    String pattern = "dd-MM-yyyy HH:mm:ss";//Formato de hora y fecha
                    Calendar cal = Calendar.getInstance();//Recupera la hora y fecha
                    SimpleDateFormat sdf = new SimpleDateFormat(pattern);
                    String strDate = sdf.format(cal.getTime());
                    SimpleDateFormat dateFormat = new SimpleDateFormat(pattern);
                    Date HE = null;
                    Date HS=null;
                    Date HA=null;
                    //casteo de las horas
                    HE = (Date) dateFormat.parse(HoraE);
                    HS = (Date) dateFormat.parse(HoraS);
                    HA = (Date) dateFormat.parse(strDate);
                    //Validacion si se encuentra en el rango
                    if(HA.after(HE) && HA.before(HS)){
                        Intent nForm= new Intent(MainActivity.this,Read.class);
                        finish();
                        startActivity(nForm);
                    }
                    else{
                        Toast.makeText(getApplicationContext(),"Boton no habilitado ,espere a que de inicio el evento", Toast.LENGTH_LONG).show();
                        }
                } catch (ParseException e){
                    e.printStackTrace();
                }
            }
        });
    }
    public void validarLogin(final View view){
        try{
            RestAdapter.Builder builder = new RestAdapter.Builder()
                    .setEndpoint(baseurl)
                    .setClient(new OkClient(new OkHttpClient()));
            serverApi = builder.build().create(ServerApi.class);
            String id = txtId.getText().toString();
            String pass=txtNombre.getText().toString();
            idGenerico=id;
            try{
                //Se valida la entrada a la aplicacion mediante los datos que se encuentran en la base de datos valga la redundancia
                serverApi.validarLogin(id,pass,new Callback<Anfitriones>() {
                    @Override//Response ....trae la respuesta del servidor
                    public void success(Anfitriones anfitriones, Response response) {
                        if(anfitriones != null){
                            Toast.makeText(getApplicationContext(),"Ingreso exitoso", Toast.LENGTH_LONG).show();
                            generaMENU(view);
                        }else {
                            Toast.makeText(getApplicationContext(), "Datos inconsistentes", Toast.LENGTH_LONG).show();
                        }
                    }
                    @Override
                    public void failure(RetrofitError error) {
                        Toast.makeText(getApplicationContext(), error.getMessage().toString(), Toast.LENGTH_LONG).show();
                    }
                });
            }catch (NumberFormatException io)
            {
                Toast.makeText(getApplicationContext(), io.getMessage().toString(), Toast.LENGTH_LONG).show();
            }
        }catch (NumberFormatException io)
        {
            Toast.makeText(getApplicationContext(), io.getMessage().toString(), Toast.LENGTH_LONG).show();
        }
    }
    @Override
    protected void onResume() {
        super.onResume();
    }
}