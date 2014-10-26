package com.example.stars_000.myapplication;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

/**
 * Created by Colored Lime on 23.10.2014.
 */
public class Sign_in extends Activity implements View.OnClickListener {
    public Button signin_button;
    public EditText loginEdit;
    public EditText passwordEdit;
    public String login;
    public String password;
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
                login = loginEdit.getText().toString();
                password = passwordEdit.getText().toString();
                client.SendMessage.message = "func=entrace;email=" + login + ";password=" + password + ";";
                client.SendMessage sendMessageTask = new client.SendMessage();
                sendMessageTask.execute();
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
}
