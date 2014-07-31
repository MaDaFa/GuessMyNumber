package ar.com.gmn.android;

import android.os.Bundle;
import android.support.v7.app.ActionBarActivity;

import ar.com.gmn.android.R;

public class LoginActivity extends ActionBarActivity {
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		// Remove title bar

		setContentView(R.layout.login);
	}
}
