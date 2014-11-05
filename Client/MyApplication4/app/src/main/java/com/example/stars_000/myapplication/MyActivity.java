package com.example.stars_000.myapplication;

import android.app.Activity;
import android.content.Context;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.utils.URLEncodedUtils;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.List;

public class MyActivity extends Activity implements View.OnClickListener {

    TextView result;
    Button registration;
    Button enter;
    Button forget;
    EditText email;
    EditText pass;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_my);
        result = (TextView) findViewById(R.id.textview_out);
        registration = (Button) findViewById(R.id.button_registration);
        enter = (Button) findViewById(R.id.button_entrace);
        forget = (Button) findViewById(R.id.button_forget);
        email = (EditText) findViewById(R.id.edittext_email);
        pass = (EditText) findViewById(R.id.edittext_password);
        registration.setOnClickListener(this);
        enter.setOnClickListener(this);
        forget.setOnClickListener(this);
        email.setOnClickListener(this);
        pass.setOnClickListener(this);


    }

    @Override
    public void onClick(View v) {
        String query = "http://217.197.2.70:11000/func=";
        // по id определеяем кнопку, вызвавшую этот обработчик
        switch (v.getId()) {
            case R.id.button_entrace:
                // кнопка ОК
               // tvOut.setText("Нажата кнопка ОК");
                result.setText("entrace!");
                query +="entrace;"+"email="+email.getText()+";password="+pass.getText()+";";
                new HttpAsyncTask().execute(query);
                break;
            case R.id.button_registration:
                // кнопка Cancel
                //tvOut.setText("Нажата кнопка Cancel");
                result.setText("registration!");
                query +="registration;"+"email="+email.getText()+";password="+pass.getText()+";";
                new HttpAsyncTask().execute(query);
                break;
            case R.id.button_forget:
                // кнопка Cancel
                //tvOut.setText("Нажата кнопка Cancel");
                query +="remind;"+"email="+email.getText()+";";
                result.setText("forget!");
                new HttpAsyncTask().execute(query);
                break;
            case R.id.edittext_email:
                // кнопка Cancel
                //tvOut.setText("Нажата кнопка Cancel");
                email.setText("");
                break;
            case R.id.edittext_password:
                // кнопка Cancel
                //tvOut.setText("Нажата кнопка Cancel");
                pass.setText("");
                break;
        }
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.my, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    public static String GET(String url){
        InputStream inputStream = null;
        String result = "";
        try {

            // create HttpClient
            HttpClient httpclient = new DefaultHttpClient();

            // make GET request to the given URL
            HttpResponse httpResponse = httpclient.execute(new HttpGet(url));

            // receive response as inputStream
            inputStream = httpResponse.getEntity().getContent();

            // convert inputstream to string
            if(inputStream != null)
                result = convertInputStreamToString(inputStream);
            else
                result = "Did not work!";

        } catch (Exception e) {
            Log.d("InputStream", e.getLocalizedMessage());
        }

        return result;
    }

    // convert inputstream to String
    private static String convertInputStreamToString(InputStream inputStream) throws IOException{
        BufferedReader bufferedReader = new BufferedReader( new InputStreamReader(inputStream));
        String line = "";
        String result = "";
        while((line = bufferedReader.readLine()) != null)
            result += line;

        inputStream.close();
        return result;

    }

    private class HttpAsyncTask extends AsyncTask<String, Void, String> {
        @Override
        protected String doInBackground(String... urls) {

            return GET(urls[0]);
        }
        // onPostExecute displays the results of the AsyncTask.
        @Override
        protected void onPostExecute(String res) {
            Toast.makeText(getBaseContext(), "Received!", Toast.LENGTH_LONG).show();
            result.setText(res);
        }
    }
}
