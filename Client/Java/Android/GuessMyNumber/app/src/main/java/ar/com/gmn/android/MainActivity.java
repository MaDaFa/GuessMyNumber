package ar.com.gmn.android;

import android.accounts.AccountManager;
import android.content.Intent;
import android.location.Location;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Toast;

import com.google.android.gms.common.AccountPicker;

public class MainActivity extends ActionBarActivity {

    //Intents Codes
    private static final int ACCOUNT_PICK_REQUEST_CODE = 1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Intent intent = AccountPicker.newChooseAccountIntent(null, null, new String[]{"com.google"}, false, null, null, null, null);
        startActivityForResult(intent, ACCOUNT_PICK_REQUEST_CODE);
    }

    public void onActivityResult(int request, int result, Intent i) {
        switch(request){
            case ACCOUNT_PICK_REQUEST_CODE:
                if (result == RESULT_OK){
                    String accountName = i.getStringExtra(AccountManager.KEY_ACCOUNT_NAME);
                    Toast.makeText(getApplicationContext(), accountName, Toast.LENGTH_SHORT).show();
                    Intent intent = new Intent(this, MenuActivity.class);
                    this.startActivity(intent);

                }
                break;
        }
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }



}
