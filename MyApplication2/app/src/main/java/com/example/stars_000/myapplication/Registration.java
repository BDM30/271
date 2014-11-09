package com.example.stars_000.myapplication;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

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
                client.SendMessage.message = "func=registration;email=" + email + ";password=" + password + ";";
                client.SendMessage sendMessageTask = new client.SendMessage();
                sendMessageTask.execute();
                break;

            default:
                break;
        }
    }
}
