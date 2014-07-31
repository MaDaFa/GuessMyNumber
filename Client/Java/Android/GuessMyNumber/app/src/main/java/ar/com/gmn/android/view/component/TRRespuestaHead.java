package ar.com.gmn.android.view.component;

import android.content.Context;
import android.graphics.Typeface;
import android.widget.TableRow;
import android.widget.TextView;

public class TRRespuestaHead extends TableRow {
	private TextView turno;
	private TextView numero;
	private TextView bien;
	private TextView regular;

	public TRRespuestaHead(Context context, int style, Typeface tf) {
		super(context);

		turno = new TextView(context);
		turno.setText("T");
		numero = new TextView(context);
		numero.setText("Numero");
		bien = new TextView(context);
		bien.setText("B");
		regular = new TextView(context);
		regular.setText("R");
		turno.setTextAppearance(context, style);
		

		bien.setTextAppearance(context, style);
		
		regular.setTextAppearance(context, style);
		
		numero.setTextAppearance(context, style);
		
		
		turno.setTypeface(tf);
		bien.setTypeface(tf);
		regular.setTypeface(tf);
		numero.setTypeface(tf);

		turno.setWidth(70);

		bien.setWidth(40);

		regular.setWidth(40);

		numero.setWidth(200);

		this.addView(turno);
		this.addView(numero);
		this.addView(bien);
		this.addView(regular);

	}



	public void setTextFont(Typeface type) {
		turno.setTypeface(type);
		bien.setTypeface(type);
		regular.setTypeface(type);
		numero.setTypeface(type);
	}

	public void setBien(String i) {
		this.bien.setText(i.toString());
	}

	public void setRegular(String i) {
		this.regular.setText(i.toString());
	}

	public void setNumero(String n) {
		this.regular.setText(n);
	}

	public void setTurno(String i) {
		this.turno.setText(i.toString());
	}

}
