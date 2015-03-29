package com.geo.Lymnologic;

/**
 * Created by Colored on 01.12.2014.
 */

import android.app.Activity;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import org.json.JSONException;
import org.json.JSONObject;

/**
 * Created by Colored Lime on 24.10.2014.
 */
public class Forgot_pass extends Activity implements View.OnClickListener{
    public EditText email_edit;
    public Button help;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.forgot_pass);

        email_edit = (EditText) findViewById(R.id.forgotPass_email);
        help = (Button) findViewById(R.id.forgotPass_help_button);
        help.setOnClickListener(this);

    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            //Кнопка восстановления пароля. Нужно запретить использование некоторых знаков в логине и пароле.
            //Например, точку с запятой.

            case R.id.forgotPass_help_button:
                /*
                email = email_edit.getText().toString();
                //Тестовый вариант восстановления пароля
                String result = "No answer =(";
                Request.HttpAsyncTask pass = new Request.HttpAsyncTask();
                pass.execute(Request.serverIP + "func=remind;email=" + email + ";");
                try {
                    result = pass.get();
                } catch (InterruptedException e) {
                    e.printStackTrace();
                } catch (ExecutionException e) {
                    e.printStackTrace();
                }
                email_edit.setText(result);

                break;
                */
                String email = email_edit.getText().toString();
                String query = "login=" + email;
                String allQuery = Request.serverIP + "api/users/recover?" +  query;
                new ForgotPassReq().execute(allQuery);
                break;

            default:
                break;
        }
    }

    public class ForgotPassReq extends AsyncTask<String, Void, String> {
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
                if ("recover success".equals(method +" "+ result)) {
                    Toast.makeText(getBaseContext(), "Письмо отправлено.", Toast.LENGTH_LONG).show();
                }
                else {
                    Toast.makeText(getBaseContext(), "Пользователя с таким email не существует.", Toast.LENGTH_LONG).show();
                }
            } catch (JSONException ex) {
                ex.printStackTrace();
            }
        }
    }
}
