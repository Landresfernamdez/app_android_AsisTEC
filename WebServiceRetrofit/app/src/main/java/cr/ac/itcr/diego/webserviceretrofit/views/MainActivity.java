package cr.ac.itcr.diego.webserviceretrofit.views;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;
import android.widget.Toast;

import com.squareup.okhttp.OkHttpClient;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;

import cr.ac.itcr.diego.webserviceretrofit.R;
import cr.ac.itcr.diego.webserviceretrofit.dto.Events;
import cr.ac.itcr.diego.webserviceretrofit.dto.Usuarios;
import cr.ac.itcr.diego.webserviceretrofit.dto.Eventos;
import cr.ac.itcr.diego.webserviceretrofit.retrofit.ServerApi;

import retrofit.Callback;
import retrofit.RestAdapter;
import retrofit.RetrofitError;
import retrofit.client.OkClient;
import retrofit.client.Response;

public class MainActivity extends AppCompatActivity  {
    public static String baseurl ="http://172.24.28.177";//La direccion ip de mi servidor local
    //Se declaran todos los componentes y variables a utilizar
    public static ServerApi serverApi;
    private EditText txtId,txtNombre;
    private Button menu;
    public static String idGenerico;
    ArrayList<Eventos> listaActividades;
    ArrayList<Events>listaEventos;
    private ListView menuActividades;
    private ListView menuEventos;
    public static String IdEventClick,id_Actividad;
    private Button btnStartlist;
    private Button btnOut;

    //Se encargan de almacenar datos temporalmente
    private TextView tvid;
    private TextView tvnombre;
    private TextView tvplace;
    private TextView tvdate;
    private TextView tvcupos;
    private TextView tvhI;
    private TextView tvhF;
    private TextView tvdetalles;

    private TextView txt1;
    private TextView txt2;
    private TextView txt3;
    private TextView txt4;
    private TextView txt5;
    private TextView txt6;
    private TextView txt7;
    private TextView txt8;
    //Se utiliza para alinear la informacion en forma de una tabla
    //Tables
    private TableLayout tbdatos;
    private TableRow tbbuttons;
    //Rows
    private TableRow tbrid;
    private TableRow tbrhe;
    private TableRow tbrhs;
    private TableRow tbrdate;
    private TableRow tbrname;
    private TableRow tbrplavce;
    private TableRow tbrcup;
    private TableRow tbrdetall;

    public static  String hore_E;
    public static  String hore_S;
    public static  String date;
    public static  String nombre;
    public static  String place;
    public static  String cup;
    public static  String detall;
    public static boolean banderalog;
    String [] opciones={"Eventos disponibles"};
    /*useLibrary 'org.apache.http.legacy'
    * @SuppressWarnings("deprecation")*/
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
        Read r=new Read();
        banderalog=false;
        if(r.bandera==false){//Con esta comparacion se sabe si se visito el Read
            setContentView(R.layout.activity_main);
            txtId = (EditText)findViewById(R.id.txtId);
            txtNombre = (EditText)findViewById(R.id.txtNombre);
            banderalog=true;
        }
        else{
            setContentView(R.layout.infoevent);
            btnStartlist = (Button) findViewById(R.id.btnstartlist);
            btnOut = (Button) findViewById(R.id.btnOut);
            //Se enlazan los text view
            tvnombre = (TextView) findViewById(R.id.tvname);
            tvplace = (TextView) findViewById(R.id.tvplace);
            tvcupos = (TextView) findViewById(R.id.tvcupos);
            tvdate = (TextView) findViewById(R.id.tvdate);
            tvhI = (TextView) findViewById(R.id.tvhinicio);
            tvhF = (TextView) findViewById(R.id.tvhfinal);
            tvdetalles = (TextView) findViewById(R.id.tvdetalles);
            //se enlaza la tabla
            tbdatos = (TableLayout) findViewById(R.id.tbdatos);
            tbrname = (TableRow) findViewById(R.id.tbname);
            tbrplavce = (TableRow) findViewById(R.id.tblugar);
            tbrcup = (TableRow) findViewById(R.id.tbcupos);
            tbrdate = (TableRow) findViewById(R.id.tbfecha);
            tbrhe = (TableRow) findViewById(R.id.tbhinicio);
            tbrhs = (TableRow) findViewById(R.id.tbhfinal);
            tbrdetall = (TableRow) findViewById(R.id.tbdetalles);

            txt2 = (TextView) findViewById(R.id.txt2);
            txt3 = (TextView) findViewById(R.id.txt3);
            txt4 = (TextView) findViewById(R.id.txt4);
            txt5 = (TextView) findViewById(R.id.txt5);
            txt6 = (TextView) findViewById(R.id.txt6);
            txt7 = (TextView) findViewById(R.id.txt7);
            txt8 = (TextView) findViewById(R.id.txt8);


            tvnombre.setText(nombre);
            tvplace.setText(place);
            tvdate.setText(date.substring(0, 10));//
            tvcupos.setText(cup);
            tvhI.setText(hore_E);//
            tvhF.setText(hore_S);//
            tvdetalles.setText(detall);
            r.bandera=false;
            idGenerico=r.id_evento;
            }
    }
    //Se encarga de validar si los datos que ingreso el usuario se encuentran en la base de datos
    public void validarLogin(final View view) {
        try {
            RestAdapter.Builder builder = new RestAdapter.Builder()
                    .setEndpoint(baseurl)
                    .setClient(new OkClient(new OkHttpClient()));
            setContentView(R.layout.activity_main);
            listaActividades =new ArrayList<Eventos>();
            serverApi = builder.build().create(ServerApi.class);
            String id = txtId.getText().toString();
            String pass = txtNombre.getText().toString();
            idGenerico = id;
            //Se valida la entrada a la aplicacion mediante los datos que se encuentran en la base de datos valga la redundancia
            serverApi.validarLogin(id,pass, new Callback<Usuarios>(){
                @Override//Response ....trae la respuesta del servidor
                public void success(Usuarios usuarios, Response response) {
                    if (usuarios != null) {
                        mensaje("Ingreso exitoso");
                        generaMENU(view);
                    } else {
                        mensaje("Datos inconsistentes");
                        regresaLogin(view);
                    }
                }
                @Override
                public void failure(RetrofitError error) {
                    mensaje("Revise su conexion a Internet");
                    regresaLogin(view);
                }
            });
        } catch (NumberFormatException io){
            mensaje("Revise su conexion a Internet ");
            regresaLogin(view);
        }
    }
    //Se encarga de regresar a la vista del login
    public void regresaLogin(View view){
        setContentView(R.layout.activity_main);
        txtId = (EditText)findViewById(R.id.txtId);
        txtNombre = (EditText)findViewById(R.id.txtNombre);

    }
    //Se encarga de regresar a las vista donde se muestra el menu de opciones a excoger
    public void regresaActividadesDisponibles(View view){
        setContentView(R.layout.main_events);
        listaActividades =new ArrayList<Eventos>();
        setContentView(R.layout.main_events);
        serverApi.ObtenerActividadesDeAnfitrion(IdEventClick,new Callback<ArrayList<Eventos>>() {
            @Override
            public void success(ArrayList<Eventos> eventoss, Response response) {
                listaActividades = eventoss;
                menuActividades = (ListView) findViewById(R.id.listActivities);
                menuActividades.setAdapter(new ViewAdapterActividades(MainActivity.this));
                menuActividades.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> parent, View view, final int position, long id) {
                        if (listaActividades != null) {

                            id_Actividad = listaActividades.get(position).getIdActividad();

                            setContentView(R.layout.infoevent);
                            btnStartlist = (Button) findViewById(R.id.btnstartlist);
                            btnOut = (Button) findViewById(R.id.btnOut);

                            Eventos evento = listaActividades.get(position);
                            //Se enlazan los text view
                            tvnombre = (TextView) findViewById(R.id.tvname);
                            tvplace = (TextView) findViewById(R.id.tvplace);
                            tvcupos = (TextView) findViewById(R.id.tvcupos);
                            tvdate = (TextView) findViewById(R.id.tvdate);
                            tvhI = (TextView) findViewById(R.id.tvhinicio);
                            tvhF = (TextView) findViewById(R.id.tvhfinal);
                            tvdetalles = (TextView) findViewById(R.id.tvdetalles);


                            hore_E = evento.getHoraInicio();
                            hore_S = evento.getHoraFinal();
                            nombre = evento.getNombre();
                            place = evento.getLugar();
                            date=evento.getFecha();
                            cup = evento.getCupo();
                            detall = evento.getDescripcion();

                            tvnombre.setText(nombre);
                            tvplace.setText(place);
                            tvdate.setText(date.substring(0, 10));
                            tvcupos.setText(cup);
                            tvhI.setText(hore_E);
                            tvhF.setText(hore_S);
                            tvdetalles.setText(detall);

                        }
                    }
                });
            }

            @Override
            public void failure(RetrofitError error){
                Toast.makeText(getApplicationContext(), error.getMessage().toString(), Toast.LENGTH_LONG).show();
            }
        });
    }
    //Esta funcion se encarga de generar el menu
            public void generaMENU(View view) {
               setContentView(R.layout.login_layout);
                serverApi.obtenerEvento(idGenerico,new Callback<ArrayList<Events>>() {
                    @Override
                    public void success(ArrayList<Events> eventses, Response response) {
                        listaEventos = eventses;
                        menuEventos = (ListView) findViewById(R.id.listEvents);
                        menuEventos.setAdapter(new ViewAdapterEventos(MainActivity.this));
                        menuEventos.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                            @Override
                            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                                if(listaEventos!=null){
                                    setContentView(R.layout.main_events);
                                    IdEventClick = listaEventos.get(position).getIdEvento();
                                    serverApi.ObtenerActividadesDeAnfitrion(IdEventClick,new Callback<ArrayList<Eventos>>() {
                                        @Override
                                        public void success(ArrayList<Eventos> eventoss, Response response) {
                                            listaActividades = eventoss;
                                            menuActividades = (ListView) findViewById(R.id.listActivities);
                                            menuActividades.setAdapter(new ViewAdapterActividades(MainActivity.this));
                                            menuActividades.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                                                @Override
                                                public void onItemClick(AdapterView<?> parent, View view, final int position, long id) {
                                                    if (listaActividades != null) {

                                                        id_Actividad = listaActividades.get(position).getIdActividad();

                                                        setContentView(R.layout.infoevent);
                                                        btnStartlist = (Button) findViewById(R.id.btnstartlist);
                                                        btnOut = (Button) findViewById(R.id.btnOut);

                                                        Eventos evento = listaActividades.get(position);
                                                        //Se enlazan los text view
                                                        tvnombre = (TextView) findViewById(R.id.tvname);
                                                        tvplace = (TextView) findViewById(R.id.tvplace);
                                                        tvcupos = (TextView) findViewById(R.id.tvcupos);
                                                        tvdate = (TextView) findViewById(R.id.tvdate);
                                                        tvhI = (TextView) findViewById(R.id.tvhinicio);
                                                        tvhF = (TextView) findViewById(R.id.tvhfinal);
                                                        tvdetalles = (TextView) findViewById(R.id.tvdetalles);


                                                        hore_E = evento.getHoraInicio();
                                                        hore_S = evento.getHoraFinal();
                                                        nombre = evento.getNombre();
                                                        place = evento.getLugar();
                                                        date=evento.getFecha();
                                                        cup = evento.getCupo();
                                                        detall = evento.getDescripcion();

                                                        tvnombre.setText(nombre);
                                                        tvplace.setText(place);
                                                        tvdate.setText(date.substring(0, 10));
                                                        tvcupos.setText(cup);
                                                        tvhI.setText(hore_E);
                                                        tvhF.setText(hore_S);
                                                        tvdetalles.setText(detall);

                                                    }
                                                }
                                            });
                                        }

                                        @Override
                                        public void failure(RetrofitError error){
                                            Toast.makeText(getApplicationContext(), error.getMessage().toString(), Toast.LENGTH_LONG).show();
                                        }
                                    });
                                }
                                else{
                                    mensaje("El evento aun no tiene actividades asignadas");
                                }

                            }
                        });
                    }

                    @Override
                    public void failure(RetrofitError error) {

                    }
                });



            }

    public void mensaje(String mens) {
        new AlertDialog.Builder(MainActivity.this)
                            .setTitle("Atencion   ")
                            .setMessage(mens)
                            .setCancelable(false)
                            .setIcon(R.drawable.dangerous1)
                            .setPositiveButton("ok", new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialog, int which) {

                                }
                            }).create().show();

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
                    try {
                        if (convertView == null) {
                            convertView = mInflater.inflate(R.layout.apoyoevents, null);
                        }
                        final TextView idText = (TextView) convertView.findViewById(R.id.tvEvents);
                        idText.setText("  " + listaEventos.get(position).getNombre());
                        final ImageView imViewEvent=(ImageView)convertView.findViewById(R.id.imageViewEvento);
                        imViewEvent.setImageResource(R.drawable.logaa);
                        return convertView;
                    } catch (NumberFormatException io) {
                        Toast.makeText(getApplicationContext(), io.getMessage().toString(), Toast.LENGTH_LONG).show();
                    }//Se retorna la vista
                    return convertView;
                }
            }
    public class ViewAdapterActividades extends BaseAdapter {
        LayoutInflater mInflater;
        public ViewAdapterActividades(Context context) {
            mInflater = LayoutInflater.from(context);
        }
        @Override
        public int getCount() {
            return listaActividades.size();
        }

        @Override
        public Object getItem(int position) {
            return listaActividades.get(position);
        }

        @Override
        public long getItemId(int position) {
            return position;
        }

        @Override
        public View getView(final int position, View convertView, ViewGroup parent) {
            try {
                if (convertView == null) {
                    convertView = mInflater.inflate(R.layout.apoyoevents, null);
                }
                final TextView idText = (TextView) convertView.findViewById(R.id.tvEvents);
                idText.setText("  " + listaActividades.get(position).getNombre());

                final ImageView imViewEvent=(ImageView)convertView.findViewById(R.id.imageViewEvento);
                imViewEvent.setImageResource(R.drawable.loga);
                return convertView;
            } catch (NumberFormatException io) {
                Toast.makeText(getApplicationContext(), io.getMessage().toString(), Toast.LENGTH_LONG).show();
            }//Se retorna la vista
            return convertView;
        }
    }
            //se encarga de iniciar la ectura en el NFC
            public void Iniciarlectura(final View view) {
                btnStartlist.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        try {
                            //Validar si puede tomar la lista osea si la fecha actual esta en el rango del evento
                            String HoraE = date.substring(0, 10) + " " + hore_E;//Variable de hora de entrada al evento
                            HoraE = HoraE.replace("/", "-");//Se encarga de reemplazar para que sirva el comparar la fecha por el tipo de formato
                            String HoraS = date.substring(0, 10) + " " + hore_S;//Variable de hora de salida al evento
                            HoraS = HoraS.replace("/", "-");
                            String pattern = "dd-MM-yyyy HH:mm:ss";//Formato de hora y fecha
                            Calendar cal = Calendar.getInstance();//Recupera la hora y fecha
                            SimpleDateFormat sdf = new SimpleDateFormat(pattern);
                            String strDate = sdf.format(cal.getTime());
                            SimpleDateFormat dateFormat = new SimpleDateFormat(pattern);
                            Date HE = null;
                            Date HS = null;
                            Date HA = null;
                            //casteo de las horas
                            HE = (Date) dateFormat.parse(HoraE);
                            HS = (Date) dateFormat.parse(HoraS);
                            HA = (Date) dateFormat.parse(strDate);
                            //Validacion si se encuentra en el rango
                            if (HA.after(HE) && HA.before(HS)) {
                                //Pasa de un activity a otro
                                Intent nForm = new Intent(MainActivity.this, Read.class);
                                finish();
                                startActivity(nForm);
                            } else {
                                mensaje("Boton no habilitado ,espere a que de inicio el evento");
                            }
                        } catch (ParseException e) {
                            e.printStackTrace();
                        }
                    }
                });
            }

            @Override
            protected void onResume() {
                super.onResume();
            }
        }