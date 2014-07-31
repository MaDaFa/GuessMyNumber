package ar.com.gmn.android.view.component;

import java.util.List;
import java.util.Vector;

import android.content.Context;
import android.graphics.Typeface;
import android.widget.LinearLayout;
import android.widget.TextView;
import ar.com.gmn.android.core.Numero;

public class NumeroView extends LinearLayout {
private Numero numero;
private Typeface type;
private TextView digit1;
private TextView digit2;
private TextView digit3;
private TextView digit4;
private List<TextView> textos;


	public NumeroView(Context context, Numero n) {
		super(context);
		
		this.setOrientation(LinearLayout.HORIZONTAL);
		this.textos = new Vector<TextView>();
		type = Typeface.createFromAsset(context.getAssets(),"fonts/EraserDust.ttf");
		digit1 = new TextView(context);
		digit1.setTypeface(type);
		digit1.setText(n.get(1).toString());
		
		
		
		digit2 = new TextView(context);
		digit2.setTypeface(type);
		digit2.setText(n.get(2).toString());
		
		digit3 = new TextView(context);
		digit3.setTypeface(type);
		digit3.setText(n.get(3).toString());
		
		digit4 = new TextView(context);
		digit4.setTypeface(type);
		digit4.setText(n.get(4).toString());
		
		this.addText(digit1);
		
		this.addText(digit2);
		this.addText(digit3);
		this.addText(digit4);
		
		
		
	}


	private void addText(TextView text) {
		this.addView(text);
		this.textos.add(text);
		
	}


	public void setTypeface(Typeface type2) {
		for (TextView text : textos) {
			text.setTypeface(type2);
		}
		
		
	}
	
	public void setTextSize(int size){
		for (TextView text : textos) {
			text.setTextSize(size);
		}
		
	}


	public void setTextColor(int color) {
		for (TextView text : textos) {
			text.setTextColor(color);
		}
		
		
	}


	public void setTextAppearance(Context context, int style) {
		for (TextView text : textos) {
			text.setTextAppearance(context, style);
		}
		
	}

}
