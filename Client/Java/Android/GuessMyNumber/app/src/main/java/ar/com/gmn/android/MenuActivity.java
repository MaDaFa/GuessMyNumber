package ar.com.gmn.android;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.app.ActionBarActivity;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.LinearLayout;

import ar.com.gmn.android.R;
import ar.com.gmn.android.core.IMenuOptions;
import ar.com.gmn.android.view.component.MenuOption;
import ar.com.gmn.android.view.component.MenuOptions;

public class MenuActivity extends ActionBarActivity {
	private IMenuOptions options;
	private LinearLayout optionsContent;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_menu);
		overridePendingTransition(R.anim.abc_slide_in_bottom, R.anim.abc_slide_out_bottom);
		this.options = new MenuOptions(this, R.style.ButtonOptionMenu);
		if (savedInstanceState == null) {
			getSupportFragmentManager().beginTransaction()
					.add(R.id.container, new PlaceholderFragment()).commit();
		}
		optionsContent = (LinearLayout)findViewById(R.id.options_content);
		Button b = null;
		for (MenuOption menuOption : options.getOptions()) {
			optionsContent.addView(menuOption);
			
		}
		
		
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {

		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.menu, menu);
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

	/**
	 * A placeholder fragment containing a simple view.
	 */
	public static class PlaceholderFragment extends Fragment {

		public PlaceholderFragment() {
		}

		@Override
		public View onCreateView(LayoutInflater inflater, ViewGroup container,
				Bundle savedInstanceState) {
			View rootView = inflater.inflate(R.layout.fragment_menu, container,
					false);
			return rootView;
		}
	}

	

}
