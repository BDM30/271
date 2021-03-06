package com.example.stars_000.myapplication;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.ContextMenu;
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


    // Константы для контекстного меню. Оно определено для 3х кнопок.
    final int MENU_COLOR_RED = 1;
    final int MENU_COLOR_GREEN = 2;
    final int MENU_COLOR_BLUE = 3;
    final int MENU_SIZE_22 = 4;
    final int MENU_SIZE_26 = 5;
    final int MENU_SIZE_30 = 6;

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

        // теперь укажем для них контекстное меню
        Log.d(common_view_tags, "Установили костекстое меню для кнопок по умолчанию");
        registerForContextMenu(enter);
        registerForContextMenu(registration);
        registerForContextMenu(forget);

    }

    // создали контекстное меню
    @Override
    public void onCreateContextMenu(ContextMenu menu, View v,
                                    ContextMenu.ContextMenuInfo menuInfo) {
        // TODO Auto-generated method stub
        if (v.getId() == R.id.button_registration || v.getId() == R.id.button_entrace ||
            v.getId() == R.id.button_forget)
        {
            /*
            - groupId - идентификатор группы, частью которой является пункт меню
            - itemId - ID пункта меню
            - order - для задания последовательности показа пунктов меню
            - title - текст, который будет отображен
            костанты определны выше
            */
            menu.add(0, MENU_COLOR_RED, 0, "Red");
            menu.add(0, MENU_COLOR_GREEN, 0, "Green");
            menu.add(0, MENU_COLOR_BLUE, 0, "Blue");
            menu.add(0, MENU_SIZE_22, 0, "22");
            menu.add(0, MENU_SIZE_26, 0, "26");
            menu.add(0, MENU_SIZE_30, 0, "30");
        }
    }

    // а теперь будет обрабатывать его.
    @Override
    public boolean onContextItemSelected(MenuItem item) {
        // TODO Auto-generated method stub
        switch (item.getItemId()) {
            // пункты меню для tvColor
            case MENU_COLOR_RED:
                enter.setTextColor(Color.RED);
                registration.setTextColor(Color.RED);
                forget.setTextColor(Color.RED);
                break;
            case MENU_COLOR_GREEN:
                enter.setTextColor(Color.GREEN);
                registration.setTextColor(Color.GREEN);
                forget.setTextColor(Color.GREEN);
                break;
            case MENU_COLOR_BLUE:
                enter.setTextColor(Color.BLUE);
                registration.setTextColor(Color.BLUE);
                forget.setTextColor(Color.BLUE);
                break;
            // пункты меню для tvSize
            case MENU_SIZE_22:
                email.setTextSize(22);
                pass.setTextSize(22);
                break;
            case MENU_SIZE_26:
                email.setTextSize(26);
                pass.setTextSize(26);
                break;
            case MENU_SIZE_30:
                email.setTextSize(30);
                pass.setTextSize(30);
                break;
        }
        return super.onContextItemSelected(item);
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
    // оправляем запросы серваку и получаем ответы на них
    public void onClick(View v) {
        String query = "http://192.168.0.101:11000/";
        // по id определеяем кнопку, вызвавшую этот обработчик
        switch (v.getId()) {

            case R.id.button_entrace:
                Log.d(common_view_tags, "Нажата кпока entrace");
                result.setText("entrace!");
                JSONObject ent_query = new JSONObject();
                try {
                    ent_query.put("function", "entrance");
                    ent_query.put("email",email.getText().toString());
                    ent_query.put("password", pass.getText().toString());
                }
                catch(JSONException ex) {
                    ex.printStackTrace();
                }
                query +=  Uri.encode(ent_query.toString());
                result.setText(query); // временно
                new HttpAsyncTask().execute(query);
                break;

            case R.id.button_registration:
                Log.d(common_view_tags, "Нажата кпока registration");
                result.setText("registration!");
                JSONObject reg_query = new JSONObject();
                try {
                    reg_query .put("function", "registration");
                    reg_query .put("email", email.getText().toString());
                    reg_query .put("password", pass.getText().toString());
                }
                catch(JSONException ex) {
                    ex.printStackTrace();
                }
                query +=  Uri.encode(reg_query .toString());
                result.setText(query); // временно
                new HttpAsyncTask().execute(query);
                break;
            case R.id.button_forget:
                Log.d(common_view_tags, "Нажата кпока forget");
                result.setText("forget!");
                JSONObject rem_query = new JSONObject();
                try {
                    rem_query.put("function", "remind");
                    rem_query.put("email", email.getText().toString());
                }
                catch(JSONException ex) {
                    ex.printStackTrace();
                }
                query +=  Uri.encode(rem_query.toString());
                result.setText(query); // временно
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
            String method = "";
            String answer = "";
            try {
                JSONObject dataJsonObj = new JSONObject(res);
                method = (dataJsonObj.getString("function"));
                answer = (dataJsonObj.getString("result"));
            }
            catch(JSONException ex) {
                ex.printStackTrace();
            }

            String toUser = "";
            ArrayList <String> methods = new ArrayList<String>();
            methods.add("entrance");
            methods.add("registration");
            methods.add("remind");
            switch(methods.indexOf(method)) {
                case 0 : toUser += "Вход!\n"; break;
                case 1 : toUser += "Регистрация!\n"; break;
                case 2 : toUser += "Восстановление!\n"; break;
                default: toUser += "Ошбика в названии метода\n"; break;
            }

            ArrayList <String> answers = new ArrayList<String>();
            answers.add("0");
            answers.add("1");
            answers.add("2");
            switch(answers.indexOf(answer)) {
                case 0 : toUser += "Неудача!\n"; break;
                case 1 : toUser += "Успешно!\n"; break;
                case 2 : toUser += "Неверные данные!\n"; break;
                default: toUser += "Ошбика в названии результата\n"; break;
            }
            Toast.makeText(getBaseContext(), toUser, Toast.LENGTH_LONG).show();
            result.setText(res);
        }
    }
}
