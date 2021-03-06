package com.geo.Lymnologic;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.concurrent.ExecutionException;

public class MyActivity extends Activity implements View.OnClickListener {

    private EditText myText;
    private Button send;
    Button http_btn;
    public String lol;
    Button gotosignpage;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_my);

        myText = (EditText) findViewById(R.id.editText);
        myText.setText("api/users/registration?login=11&password=kool&nickname=bdm&gender=false");

        send = (Button) findViewById(R.id.button);
        send.setText("Send");

        http_btn = (Button) findViewById(R.id.http_btn);
        http_btn.setOnClickListener(this);

        gotosignpage = (Button) findViewById(R.id.gotosignpage);
        gotosignpage.setOnClickListener(this);


        send.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                String sndtest = "";
                Request.HttpAsyncTask sendtest = new Request.HttpAsyncTask();
                sendtest.execute(Request.serverIP + myText.getText().toString());
                try {
                    sndtest = sendtest.get();
                } catch (InterruptedException e) {
                    e.printStackTrace();
                } catch (ExecutionException e) {
                    e.printStackTrace();
                }
                myText.setText(""); //Очищаем наше поле ввода
                Toast.makeText(getBaseContext(), sndtest, Toast.LENGTH_LONG).show();
            }
        });
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {

            case R.id.gotosignpage:
                Intent intent = new Intent(this, Sign_in.class);
                startActivity(intent);
                break;

            case R.id.http_btn:


                JSONObject reg_query = new JSONObject();
                try {
                    /**reg_query.put("Block", "users");
                    reg_query.put("Function", "registration");
                    reg_query.put("gender", "false");
                    reg_query.put("nickname", "android");
                    reg_query.put("password", "dddd");
                    reg_query.put("login", "andriod3000");
                     */
                    reg_query.put("nickname", "android");
                }

                catch(JSONException ex) {
                    ex.printStackTrace();
                }
                String allQuery = Request.serverIP + "api/users/registration?" +  Uri.encode(reg_query.toString());
                myText.setText(allQuery);
                Toast.makeText(this, allQuery, Toast.LENGTH_LONG).show();
                new Register().execute(allQuery);
                break;

            default:
                break;
        }
    }

    public class Register extends AsyncTask<String, Void, String> {
        @Override
        protected String doInBackground(String... urls) {
            return Request.GET(urls[0]);
        }
        protected void onPostExecute(String res) {
            String method = "";
            String answer = "";
            try {
                JSONObject dataJsonObj = new JSONObject(res);
                method = (dataJsonObj.getString("function"));
                answer = (dataJsonObj.getString("result"));
                myText.setText(method + answer);
                if ("registration;1".equals(method + ";" + answer)) {
                    Intent intent = new Intent(getBaseContext(), Notifications.class);
                    startActivity(intent);
                }
            } catch (JSONException ex) {
                ex.printStackTrace();
            }
        }
    }
}

