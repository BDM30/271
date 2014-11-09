package com.example.stars_000.myapplication;

import android.os.AsyncTask;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;

/**
 * Created by Colored Lime on 24.10.2014.
 */
public class client {
    //Класс, который отправляет сообщения на сервер
    public static class SendMessage extends AsyncTask<Void, Void, Void> {
        public static String message = ""; //сообщение
        @Override
        protected Void doInBackground(Void... params) {
            try {

                Socket client = new Socket("217.197.4.107", 11000); // подключаемся к серверу; 111-server: 217.197.4.107
                PrintWriter printwriter = new PrintWriter(client.getOutputStream(), true);
                printwriter.write(message); // write the message to output stream

                printwriter.flush();
                printwriter.close();
                client.close(); // closing the connection

            } catch (UnknownHostException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
            return null;
        }
    }
}
