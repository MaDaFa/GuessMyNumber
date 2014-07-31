package ar.com.gmn.android;

import java.util.ArrayList;
import java.util.List;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentPagerAdapter;
import android.support.v4.view.ViewPager;
import android.support.v7.app.ActionBarActivity;
import android.view.Menu;
import android.view.MenuItem;

import ar.com.gmn.android.R;
import ar.com.gmn.android.fragment.SinglePlayerFragment;
import ar.com.gmn.android.fragment.StatusFragmentSP;

public class SinglePlayerFragmentActivity extends ActionBarActivity {

	  /**
	   * Identifier for the first fragment.
	   */
	  public static final int FRAGMENT_ONE = 0;
	 
	  /**
	   * Identifier for the second fragment.
	   */
	  public static final int FRAGMENT_TWO = 1;
	 
	  /**
	   * Number of total fragments.
	   */
	  public static final int FRAGMENTS = 2;
	 
	  /**
	   * The adapter definition of the fragments.
	   */
	  private FragmentPagerAdapter _fragmentPagerAdapter;
	 
	  /**
	   * The ViewPager that hosts the section contents.
	   */
	  private ViewPager _viewPager;
	 
	  /**
	   * List of fragments.
	   */
	  private List<Fragment> _fragments = new ArrayList<Fragment>();
	  
	 
	  @Override
	  protected void onCreate(final Bundle savedInstanceState) {
	    super.onCreate(savedInstanceState);
	 
	    setContentView(R.layout.singleplayer_fragment_main);
	    overridePendingTransition(R.anim.abc_fade_in, R.anim.abc_fade_out);
	    // Create fragments.
	    _fragments.add(FRAGMENT_ONE, new SinglePlayerFragment());
	    _fragments.add(FRAGMENT_TWO, new StatusFragmentSP());
	 
	    // Setup the fragments, defining the number of fragments, the screens and titles.
	    _fragmentPagerAdapter = new FragmentPagerAdapter(getSupportFragmentManager()){
	      @Override
	      public int getCount() {
	        return FRAGMENTS;
	      }
	      @Override
	      public Fragment getItem(final int position) {
	        return _fragments.get(position);
	      }
//	      @Override
//	      public CharSequence getPageTitle(final int position) {
//	        switch (position) {
//	          case FRAGMENT_ONE:
//	            return "Title One";
//	          case FRAGMENT_TWO:
//	            return "Title Two";
//	          default:
//	            return null;
//	        }
//	      }
	    };
	    _viewPager = (ViewPager) findViewById(R.id.pager);
	    _viewPager.setAdapter(_fragmentPagerAdapter);
	  }
		@Override
		public void onBackPressed() {
			super.onBackPressed();
			overridePendingTransition(R.anim.abc_slide_in_top,
					R.anim.abc_slide_out_top);
		}
		@Override
		public boolean onCreateOptionsMenu(Menu menu) {

			super.onCreateOptionsMenu(menu);
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
			if (id == R.id.new_game) {
				Intent intent = getIntent();
				finish();
				startActivity(intent);

			}
			return super.onOptionsItemSelected(item);
		}

}
