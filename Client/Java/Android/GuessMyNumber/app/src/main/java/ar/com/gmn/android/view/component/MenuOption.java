package ar.com.gmn.android.view.component;



import android.content.Context;
import android.content.Intent;
import android.graphics.Typeface;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;




public class MenuOption extends Button implements OnClickListener{
	private Class target;
	public MenuOption(Context context,String description, Class target, int style) {
		super(context);
		this.target = target;
		this.setText(description);
		this.setOnClickListener(this);
		this.setTextAppearance(context, style);
		Typeface type = Typeface.createFromAsset(context.getAssets(),"fonts/EraserDust.ttf");
		this.setTypeface(type);
		
//		
		
	}



	@Override
	public void onClick(View v) {
		Intent intent = new Intent(this.getContext(), target);
	    this.getContext().startActivity(intent);
		
	}

	
	
}
