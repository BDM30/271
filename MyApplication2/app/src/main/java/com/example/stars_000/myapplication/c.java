package com.example.stars_000.myapplication;

/**
 * Created by Colored Lime on 02.11.2014.
 */
import java.io.*;
import java.net.*;

public class c {

    public static String getHTML(String urlToRead) {
        URL url;
        HttpURLConnection conn;
        BufferedReader rd;
        String line;
        String result = "";
        try {
            url = new URL(urlToRead);
            conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("GET");
            rd = new BufferedReader(new InputStreamReader(conn.getInputStream()));
            while ((line = rd.readLine()) != null) {
                result += line;
            }
            rd.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return result;
    }

    public static void main(String args[])
    {
        c c = new c();
        System.out.println(c.getHTML(args[0]));
    }
}
