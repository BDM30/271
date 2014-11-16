package com.example.stars_000.myapplication;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;


public class MyActivity extends Activity implements View.OnClickListener {

    //private Socket client;
    private EditText myText;
    private Button send;
    Button http_btn;
    public String lol;
    TextView myTextView;
    Button gotosignpage;
    ///тест 2

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_my);

        myText = (EditText) findViewById(R.id.editText);
        myText.setText("type smth");

        myTextView = (TextView) findViewById(R.id.textView);

        send = (Button) findViewById(R.id.button);
        send.setText("Send");

        http_btn = (Button) findViewById(R.id.http_btn);
        http_btn.setOnClickListener(this);

        send.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                String sndtest = "";
                Request.HttpAsyncTask sendtest = new Request.HttpAsyncTask();
                sendtest.execute(com.example.stars_000.myapplication.Request.serverIP + myText.getText().toString());
                try {
                    sndtest = sendtest.get();
                } catch (InterruptedException e) {
                    e.printStackTrace();
                } catch (ExecutionException e) {
                    e.printStackTrace();
                }
                myText.setText(""); //Очищаем наше поле ввода
                myTextView.setText(sndtest);
            }
        });

//Переходим на страницу регистрации
        gotosignpage = (Button) findViewById(R.id.gotosignpage);
        gotosignpage.setOnClickListener(this);

    }

    //Это наш активити-листенер. Скоро мы будем обрабатывать нажатие любой кнопки тут.
    @Override
    public void onClick(View v) {
        switch (v.getId()) {

            case R.id.gotosignpage:
                Intent intent = new Intent(this, Sign_in.class);
                startActivity(intent);
                break;

            case R.id.http_btn:
                //Пример http-запроса
                Request.HttpAsyncTask test = new Request.HttpAsyncTask();
                test.execute(com.example.stars_000.myapplication.Request.serverIP + "func=registration;email=starson450@yandex.ru;password=vlad;");
                try {
                    lol = test.get(10, TimeUnit.SECONDS);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                } catch (ExecutionException e) {
                    e.printStackTrace();
                } catch (TimeoutException e) {
                    Toast.makeText(this, "Сервер не отвечает.", Toast.LENGTH_LONG).show();
                    e.printStackTrace();
                }
                myTextView.setText(lol);
                break;

            default:
                break;
        }
    }
}