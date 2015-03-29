package com.geo.Lymnologic;

import android.app.Activity;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;
import org.json.JSONException;
import org.json.JSONObject;

/**
 * Created by Colored Lime on 23.10.2014.
 */
public class Sign_in extends Activity implements View.OnClickListener {
    public Button signin_button;
    public EditText loginEdit;
    public EditText passwordEdit;
    public TextView register;
    public TextView forgot_pass;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.login_or_reg);
        signin_button = (Button) findViewById(R.id.signin_button);
        signin_button.setOnClickListener(this);
        loginEdit = (EditText) findViewById(R.id.login);
        passwordEdit = (EditText) findViewById(R.id.password);
        register = (TextView) findViewById(R.id.register);
        register.setOnClickListener(this);
        forgot_pass = (TextView) findViewById(R.id.forgot_pass);
        forgot_pass.setOnClickListener(this);
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            //Кнопка входа в аккаунт. Нужно запретить использование некоторых знаков в логине и пароле.
            //Например, точку с запятой.
            case R.id.signin_button:
                String email = loginEdit.getText().toString();
                String password = passwordEdit.getText().toString();
                String query = "login=" + email + "&password=" + password;
                String allQuery = Request.serverIP + "api/users/entrance?" +  query;
                //Toast.makeText(this, allQuery, Toast.LENGTH_LONG).show();
                new SignIn().execute(allQuery);
                break;

            case R.id.register:
                Intent intent = new Intent(this, Registration.class);
                startActivity(intent);
                break;

            case R.id.forgot_pass:
                Intent forgot_pass_intent = new Intent(this, Forgot_pass.class);
                startActivity(forgot_pass_intent);
                break;

            default:
                break;
        }
    }

    public class SignIn extends AsyncTask<String, Void, String> {
        @Override
        protected String doInBackground(String... urls) {
            return Request.GET(urls[0]);
        }
        protected void onPostExecute(String res) {
            String method = "";
            String result = "";
            try {
                JSONObject dataJsonObj = new JSONObject(res);
                result = dataJsonObj.getString("Result");
                method = dataJsonObj.getString("Function");

                //email_edit.setText(method + result);
                if ("entrance success".equals(method +" "+ result)) {
                    Intent intent = new Intent(getBaseContext(), Notifications.class);
                    startActivity(intent);
                }
                else {
                    Toast.makeText(getBaseContext(), "Неверный пароль или ошибка.", Toast.LENGTH_LONG).show();
                }
            } catch (JSONException ex) {
                ex.printStackTrace();
            }
        }
    }
}


