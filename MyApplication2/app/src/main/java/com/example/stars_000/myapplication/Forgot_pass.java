package com.example.stars_000.myapplication;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

/**
 * Created by Colored Lime on 24.10.2014.
 */
public class Forgot_pass extends Activity implements View.OnClickListener{
    public EditText email_edit;
    public String email;
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
                email = email_edit.getText().toString();
                client.SendMessage.message = "func=remind;email=" + email + ";";
                client.SendMessage sendMessageTask = new client.SendMessage();
                sendMessageTask.execute();
                break;
            default:
                break;
        }
    }
}