package com.anatoli.parseexample;

import android.app.Activity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;

import com.parse.Parse;
import com.parse.ParseCloud;
import com.parse.ParseException;
import com.parse.ParseInstallation;
import com.parse.ParseQuery;

import java.util.HashMap;
import java.util.Map;

public class ParseExample extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_parse_example);

        Parse.enableLocalDatastore(this);
        Parse.initialize(this, "wUAgTsRuLdin0EvsBhPniG40O24i2nEGVFl8R5OI", "tDNoIFE1vzcXkBrxqltx392kqOqAhmUD9q0CZUlY");
        ParseInstallation installInfo = ParseInstallation.getCurrentInstallation();
        installInfo.addUnique("username", "b3cfc74e-2004-47f5-acd7-a9b6f8811076");
        installInfo.addUnique("channels","Eigg");
        installInfo.addUnique("channels","Vn");
        installInfo.saveInBackground();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_parse_example, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int userCount=0;
        try {

            ParseQuery installationQuery = ParseInstallation.getQuery();
            userCount = installationQuery.count();
        } catch (Exception e) {
            e.printStackTrace();
        }
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }
}
