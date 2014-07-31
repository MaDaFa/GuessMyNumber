package ar.com.gmn.android.view.component;

import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.widget.ImageView;
import android.widget.TextView;

public class ImageViewOnTouchListener implements OnTouchListener{
	public Integer current;

	public ImageViewOnTouchListener() {
	}

	 @Override
     public boolean onTouch(View v, MotionEvent event) {

         switch (event.getAction()) {
             case MotionEvent.ACTION_DOWN: {
                 TextView view = (TextView) v;
                 //overlay is black with transparency of 0x77 (119)
//                 view.getDrawable().setColorFilter(0x77000000,PorterDuff.Mode.SRC_ATOP);
                 view.invalidate();
                 break;
             }
             case MotionEvent.ACTION_UP:{
            	 v.performClick();
             }
             case MotionEvent.ACTION_CANCEL: {
                 ImageView view = (ImageView) v;
                 //clear the overlay
                 view.getDrawable().clearColorFilter();
                 view.invalidate();
                 break;
             }
         }

         return true;
     }
 
}