package com.example.stars_000.myapplication;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import java.util.concurrent.ExecutionException;

/**
 * Created by Colored Lime on 24.10.2014.
 */
public class Registration extends Activity implements View.OnClickListener{
    public EditText email_edit;
    public EditText password_edit;
    public Button register_button;
    public String email;
    public String password;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.registration);

        email_edit = (EditText) findViewById(R.id.reg_email);
        password_edit = (EditText) findViewById(R.id.reg_password);
        register_button = (Button) findViewById(R.id.reg_btn);
        register_button.setOnClickListener(this);
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            //Кнопка регистрации. Нужно запретить использование некоторых знаков в логине и пароле.
            //Например, точку с запятой.
            case R.id.reg_btn:
                email = email_edit.getText().toString();
                password = password_edit.getText().toString();

                //Тестовый вариант регистрации
                String result = "No answer =(";
                Request.HttpAsyncTask reg = new Request.HttpAsyncTask();
                reg.execute(com.example.stars_000.myapplication.Request.serverIP + "func=registration;email=" + email + ";password=" + password + ";");
                try {
                    result = reg.get();
                } catch (InterruptedException e) {
                    e.printStackTrace();
                } catch (ExecutionException e) {
                    e.printStackTrace();
                }
                email_edit.setText(result);
                break;

            default:
                break;
        }
    }
}
