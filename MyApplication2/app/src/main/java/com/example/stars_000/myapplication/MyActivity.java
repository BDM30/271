package com.example.stars_000.myapplication;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import org.apache.http.client.ResponseHandler;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.BasicResponseHandler;
import org.apache.http.impl.client.DefaultHttpClient;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.concurrent.ExecutionException;


public class MyActivity extends Activity implements View.OnClickListener {

    //private Socket client;
    private EditText myText;
    private Button send;
    Button http_btn;
    public String lol;
    TextView myTextView;
    Button gotosignpage;
    ///
    ServerSocket serverSocket;
    String input = "";
    ///

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
                client.SendMessage.message = (myText.getText().toString()); //забиваем текст в переменную из SendMessage
                myText.setText(""); //Очищаем наше поле ввода
                client.SendMessage sendMessageTask = new client.SendMessage();
                sendMessageTask.execute();
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
                c.HttpAsyncTask test = new c.HttpAsyncTask();
                test.execute(c.serverIP + "registration;email=starson450@yandex.ru;password=vlad;");
                try {
                    lol = test.get();
                } catch (InterruptedException e) {
                    e.printStackTrace();
                } catch (ExecutionException e) {
                    e.printStackTrace();
                }
                myTextView.setText(lol);
                break;

        default:
                break;
        }
    }

}