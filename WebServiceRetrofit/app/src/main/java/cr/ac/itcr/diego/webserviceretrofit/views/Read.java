package cr.ac.itcr.diego.webserviceretrofit.views;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.PendingIntent;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.nfc.NdefMessage;
import android.nfc.NdefRecord;
import android.nfc.NfcAdapter;
import android.nfc.Tag;
import android.nfc.tech.Ndef;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.squareup.okhttp.OkHttpClient;

import cr.ac.itcr.diego.webserviceretrofit.dto.Personas;
import cr.ac.itcr.diego.webserviceretrofit.dto.Registros;
import cr.ac.itcr.diego.webserviceretrofit.retrofit.ServerApi;
import retrofit.Callback;
import retrofit.RestAdapter;
import retrofit.RetrofitError;
import retrofit.client.OkClient;
import retrofit.client.Response;

import java.io.UnsupportedEncodingException;
import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.Calendar;

import cr.ac.itcr.diego.webserviceretrofit.R;
public class Read extends AppCompatActivity {
    //Se declaran todad las variables y componentes a utilizar
    public static int contador=0;
    public static final String MIME_TEXT_PLAIN = "text/plain";
    public static final String TAG = "NfcDemo";
    private TextView mTextView;
    private TextView InfoTextView;
    private NfcAdapter mNfcAdapter;
    public String cedula;
    private Button btnBack;
    static public ServerApi serverApi;
    public static boolean bandera;
    public static String id_evento;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.menu_listando);
        bandera=false;
        RestAdapter.Builder builder = new RestAdapter.Builder()
                .setEndpoint(MainActivity.baseurl)
                .setClient(new OkClient(new OkHttpClient()));
        serverApi = builder.build().create(ServerApi.class);
        mNfcAdapter = NfcAdapter.getDefaultAdapter(this);
        mTextView=(TextView)findViewById(R.id.tvmsj);
        btnBack=(Button)findViewById(R.id.btnback);
        handleIntent(getIntent());
        btnBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                bandera=true;
                MainActivity m=new MainActivity();
                id_evento=m.idGenerico;
                Intent nForm = new Intent(Read.this, MainActivity.class);
                finish();
                startActivity(nForm);
            }
        });
        if (mNfcAdapter == null) {
            // Stop here, we definitely need NFC
            Toast.makeText(this, "This device doesn't support NFC.", Toast.LENGTH_LONG).show();
            finish();
            return;
        }
        //Verifica si el NFC en el dispositivo esta activado

            if (!mNfcAdapter.isEnabled()) {
                mTextView.setText("Active el NFC");
            } else {
                mTextView.setText("leyendo......");
            }


    }

    @Override
    protected void onResume() {
        super.onResume();

        /**
         * It's important, that the activity is in the foreground (resumed). Otherwise
         * an IllegalStateException is thrown.
         */
        setupForegroundDispatch(this, mNfcAdapter);
    }
    @Override
    protected void onPause() {
        /**
         * Call this before onPause, otherwise an IllegalArgumentException is thrown as well.
         */
        stopForegroundDispatch(this, mNfcAdapter);

        super.onPause();
    }
    @Override
    protected void onNewIntent(Intent intent) {
        /**
         * This method gets called, when a new Intent gets associated with the current activity instance.
         * Instead of creating a new activity, onNewIntent will be called. For more information have a look
         * at the documentation.
         *
         * In our case this method gets called, when the user attaches a Tag to the device.
         */
        handleIntent(intent);
    }
    /**
     * @param activity The corresponding {@link Activity} requesting the foreground dispatch.
     * @param adapter The {@link NfcAdapter} used for the foreground dispatch.
     */
    public static void setupForegroundDispatch(final Activity activity, NfcAdapter adapter) {
        final Intent intent = new Intent(activity.getApplicationContext(), activity.getClass());
        intent.setFlags(Intent.FLAG_ACTIVITY_SINGLE_TOP);

        final PendingIntent pendingIntent = PendingIntent.getActivity(activity.getApplicationContext(), 0, intent, 0);

        IntentFilter[] filters = new IntentFilter[1];
        String[][] techList = new String[][]{};

        // Notice that this is the same filter as in our manifest.
        filters[0] = new IntentFilter();
        filters[0].addAction(NfcAdapter.ACTION_NDEF_DISCOVERED);
        filters[0].addCategory(Intent.CATEGORY_DEFAULT);
        try {
            filters[0].addDataType(MIME_TEXT_PLAIN);
        } catch (IntentFilter.MalformedMimeTypeException e) {
            throw new RuntimeException("Check your mime type.");
        }

        adapter.enableForegroundDispatch(activity, pendingIntent, filters, techList);
    }

    /**

     * @param adapter The {@link NfcAdapter} used for the foreground dispatch.
     */
    public static void stopForegroundDispatch(final Activity activity, NfcAdapter adapter) {
        adapter.disableForegroundDispatch(activity);
    }
    private void handleIntent(Intent intent) {
        String action = intent.getAction();
        if (NfcAdapter.ACTION_NDEF_DISCOVERED.equals(action)) {

            String type = intent.getType();
            if (MIME_TEXT_PLAIN.equals(type)) {

                Tag tag = intent.getParcelableExtra(NfcAdapter.EXTRA_TAG);
                new NdefReaderTask().execute(tag);

            } else {
                Log.d(TAG, "Wrong mime type: " + type);
            }
        } else if (NfcAdapter.ACTION_TECH_DISCOVERED.equals(action)) {

            // In case we would still use the Tech Discovered Intent
            Tag tag = intent.getParcelableExtra(NfcAdapter.EXTRA_TAG);
            String[] techList = tag.getTechList();
            String searchedTech = Ndef.class.getName();

            for (String tech : techList) {
                if (searchedTech.equals(tech)) {
                    new NdefReaderTask().execute(tag);
                    break;
                }
            }
        }
    }
    private class NdefReaderTask extends AsyncTask<Tag, Void, String> {

        @Override
        protected String doInBackground(Tag... params) {
            Tag tag = params[0];

            Ndef ndef = Ndef.get(tag);
            if (ndef == null) {
                // NDEF is not supported by this Tag.
                return null;
            }

            NdefMessage ndefMessage = ndef.getCachedNdefMessage();

            NdefRecord[] records = ndefMessage.getRecords();
            for (NdefRecord ndefRecord : records) {
                if (ndefRecord.getTnf() == NdefRecord.TNF_WELL_KNOWN && Arrays.equals(ndefRecord.getType(), NdefRecord.RTD_TEXT)) {
                    try {
                        return readText(ndefRecord);
                    } catch (UnsupportedEncodingException e) {
                        Log.e(TAG, "Unsupported Encoding", e);
                    }
                }
            }

            return null;
        }

        private String readText(NdefRecord record) throws UnsupportedEncodingException {
        /*
         * See NFC forum specification for "Text Record Type Definition" at 3.2.1
         *
         * http://www.nfc-forum.org/specs/
         *
         * bit_7 defines encoding
         * bit_6 reserved for future use, must be 0
         * bit_5..0 length of IANA language code
         */

            byte[] payload = record.getPayload();

            // Get the Text Encoding
            String textEncoding = ((payload[0] & 128) == 0) ? "UTF-8" : "UTF-16";

            // Get the Language Code
            int languageCodeLength = payload[0] & 0063;

            // String languageCode = new String(payload, 1, languageCodeLength, "US-ASCII");
            // e.g. "en"

            // Get the Text
            return new String(payload, languageCodeLength + 1, payload.length - languageCodeLength - 1, textEncoding);
        }
        public void mensaje(String mens) {
            new AlertDialog.Builder(Read.this)
                    .setTitle("Atencion")
                    .setMessage(mens)
                    .setCancelable(false)
                    .setPositiveButton("ok", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialog, int which) {


                        }
                    }).create().show();

        }
        @Override
        protected void onPostExecute(String result) {
            if (result != null) {
                cedula= result.substring(0,9);
                try{
                    serverApi.obtenerEstudiante(cedula,new Callback<Personas>() {
                        @Override//Response ....trae la respuesta del servidor
                        public void success(Personas personas, Response response) {
                            if (personas != null) {
                                //Valida si el personas biene de entrada
                                if (personas.getEstado().equals("1")) {
                                    //Inserta en el registro de entrada
                                    try{
                                        Registros registros=new Registros();
                                        registros.setCedula(cedula);
                                        String pattern = "yyyy-MM-dd HH:mm:ss";//Formato de hora y fecha
                                        Calendar cal = Calendar.getInstance();//Recupera la hora y fecha
                                        SimpleDateFormat sdf = new SimpleDateFormat(pattern);

                                        String strDate = sdf.format(cal.getTime());
                                        String fecha=strDate.substring(0,10);
                                        String hora=strDate.substring(11);
                                        registros.setHora(hora);
                                        registros.setFecha(fecha);
                                        registros.setIdActividad(MainActivity.id_Actividad);

                                        //Se encarga de meter informacion en el registro de entrada en sql
                                        serverApi.postRegistrosInformation(registros, new Callback<Boolean>() {
                                            @Override
                                            public void success(Boolean aBoolean, Response response) {
                                                mensaje("Se ha insertado con exito");

                                            }

                                            @Override
                                            public void failure(RetrofitError error) {
                                                mensaje("Opps ha fallado la insercion");
                                            }
                                        });
                                        personas.setEstado("0");
                                        //Se encarga de actualizar la informacion del estado el cual indica que la persona entro
                                        serverApi.putEstudiantesInformation(personas, new Callback<Boolean>() {
                                            @Override
                                            public void success(Boolean aBoolean, Response response) {
                                                    mensaje("se actualizo la salida con exito");
                                            }
                                            @Override
                                            public void failure(RetrofitError error) {
                                                mensaje("Opps error en la actualizacion");
                                            }
                                        });
                                    }catch (NumberFormatException io)
                                    {
                                        mensaje("Opps error de conexion");
                                    }

                                } else {
                                    try{
                                        //Inserta en el registro de entrada
                                        Registros registros=new Registros();
                                        registros.setCedula(cedula);
                                        String pattern = "yyyy-MM-dd HH:mm:ss";//Formato de hora y fecha
                                        Calendar cal = Calendar.getInstance();//Recupera la hora y fecha
                                        SimpleDateFormat sdf = new SimpleDateFormat(pattern);
                                        String strDate = sdf.format(cal.getTime());
                                        String fecha=strDate.substring(0,10);
                                        String hora=strDate.substring(11);
                                        registros.setHora(hora);
                                        registros.setFecha(fecha);
                                        registros.setIdActividad(MainActivity.id_Actividad);
                                        //Se encarga de meter informacion en el registro de entrada en sql
                                        serverApi.postRegistrosSInformation(registros, new Callback<Boolean>() {
                                            @Override
                                            public void success(Boolean aBoolean, Response response) {

                                                mensaje("se inserto el asistente con exito");

                                            }

                                            @Override
                                            public void failure(RetrofitError error) {
                                                mensaje("Opps ha fallado la insercion");
                                            }
                                        });

                                        personas.setEstado("1");
                                        //Se encarga de actualizar la informacion del estado el cual indica que la persona salio
                                        serverApi.putEstudiantesInformation(personas, new Callback<Boolean>() {
                                            @Override
                                            public void success(Boolean aBoolean, Response response) {

                                                mensaje("se actualizo la entrada con exito");

                                            }

                                            @Override
                                            public void failure(RetrofitError error) {
                                                mensaje("Opps ha fallado la actualizacion");
                                            }
                                        });
                                    }
                                    catch (NumberFormatException io)
                                    {
                                        mensaje("Error , en la conexion");
                                    }

                                }

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
            }
        }
    }
}
