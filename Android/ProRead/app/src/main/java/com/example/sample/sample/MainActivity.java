package com.example.sample.sample;

import android.content.Intent;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;


public class MainActivity extends ActionBarActivity {
    static Button howToButton,startBookButton, startAddBook;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        OnHowClickButtonListener();
        OnStartBookButtonListener();
    }
    public void OnHowClickButtonListener() {
        howToButton = (Button) findViewById(R.id.titleButtonHowTo);
        howToButton.setOnClickListener(new View.OnClickListener(){
            @Override
            //On click function
            public void onClick(View view) {
                //Create the intent to start another activity
                Intent intent = new Intent("com.example.sample.sample.HowToActivity" );
                startActivity(intent);
            }
        });
    }
    public void OnStartBookButtonListener() {
        startBookButton = (Button) findViewById(R.id.titleBookStart);
        startBookButton.setOnClickListener(new View.OnClickListener(){
            @Override
            //On click function
            public void onClick(View view) {
                //Create the intent to start another activity
                Intent intent = new Intent("com.example.sample.sample.ChooseSpeed" );
                startActivity(intent);
            }
        });
    }
    public void OnStartBookAddListener() {
        startAddBook = (Button) findViewById(R.id.titleButtonAddeBook);
        startAddBook.setOnClickListener(new View.OnClickListener(){
            @Override
            //On click function
            public void onClick(View view) {
                //Create the intent to start another activity
                Intent intent = new Intent("com.example.sample.sample.ShowBooks" );
                startActivity(intent);
            }
        });
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }
}
