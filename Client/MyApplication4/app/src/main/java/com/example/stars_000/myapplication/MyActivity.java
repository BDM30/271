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

    // элементы формы
    TextView result;
    Button registration;
    Button enter;
    Button forget;
    EditText email;
    EditText pass;

    //логи
    private static final String common_view_tags = "Common view logs:";

    private void set_onclick_action() {
        registration.setOnClickListener(this);
        enter.setOnClickListener(this);
        forget.setOnClickListener(this);
        email.setOnClickListener(this);
        pass.setOnClickListener(this);
    }

    private void initialization() {
        // нашли их на форме
        Log.d(common_view_tags, "Нашли(инициализировали) элементы на форме");
        result = (TextView) findViewById(R.id.textview_out);
        registration = (Button) findViewById(R.id.button_registration);
        enter = (Button) findViewById(R.id.button_entrace);
        forget = (Button) findViewById(R.id.button_forget);
        email = (EditText) findViewById(R.id.edittext_email);
        pass = (EditText) findViewById(R.id.edittext_password);

        //вписали в них текст из ресурсом my_res.xml
        Log.d(common_view_tags, "Задали текст по умолчанию");
        result.setText(R.string.text_result);
        registration.setText(R.string.text_registration);
        enter.setText(R.string.text_entrace);
        forget.setText(R.string.text_remind);
        email.setText(R.string.text_email);
        pass.setText(R.string.text_password);

        // вписали в них цвета из ресурсов my_res.xml
        Log.d(common_view_tags, "Задали цвета кнопок по умолчанию");
        enter.setBackgroundResource(R.color.color_entrace);
        registration.setBackgroundResource(R.color.color_registration);
        forget.setBackgroundResource(R.color.color_remind);

    }



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_my);

        // инициализируем элементы на форме
        initialization();

        // действия по клику
        set_onclick_action();



    }

    @Override
    public void onClick(View v) {
        String query = "http://217.197.2.70:11000/func=";
        // по id определеяем кнопку, вызвавшую этот обработчик
        switch (v.getId()) {
            case R.id.button_entrace:
                Log.d(common_view_tags, "Нажата кпока entrace");
                result.setText("entrace!");
                query +="entrace;"+"email="+email.getText()+";password="+pass.getText()+";";
                new HttpAsyncTask().execute(query);
                break;
            case R.id.button_registration:
                Log.d(common_view_tags, "Нажата кпока registration");
                result.setText("registration!");
                query +="registration;"+"email="+email.getText()+";password="+pass.getText()+";";
                new HttpAsyncTask().execute(query);
                break;
            case R.id.button_forget:
                Log.d(common_view_tags, "Нажата кпока forget");
                query +="remind;"+"email="+email.getText()+";";
                result.setText("forget!");
                new HttpAsyncTask().execute(query);
                break;
            case R.id.edittext_email:
                Log.d(common_view_tags, "Нажата эдит email");

                email.setText("");
                break;
            case R.id.edittext_password:
                Log.d(common_view_tags, "Нажата эдит password");
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
            String answer = "wrong answer";
            ArrayList <String> good_answers = new ArrayList<String>();
            good_answers.add("func=registration;result=1;"); // 0
            good_answers.add("func=registration;result=0;"); // 1
            good_answers.add("func=entrace;result=1;");      // 2
            good_answers.add("func=entrace;result=0;");      // 3
            good_answers.add("func=remind;result=1;");       // 4
            good_answers.add("func=remind;result=0;");       // 5
            int number = good_answers.indexOf(res);
            switch(number) {
                case 0: answer="Успешная регистарация!";  break;
                case 1: answer="Такой пользователь уже есть!"; break;
                case 2: answer="Добро пожаловать!"; break;
                case 3: answer="Такого пользователя нет!"; break;
                case 4: answer="Мы выслали ваш пароль!"; break;
                case 5: answer="Нет такого пользователя!"; break;
                default:
                    answer = "Не найдено обработчика ответа!"; break;
            }
            Toast.makeText(getBaseContext(), answer, Toast.LENGTH_LONG).show();
            result.setText(res);
        }
    }
}
