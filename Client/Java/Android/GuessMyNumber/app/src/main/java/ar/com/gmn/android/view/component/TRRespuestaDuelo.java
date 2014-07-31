package ar.com.gmn.android.view.component;

import android.content.Context;
import android.graphics.Typeface;
import android.view.Gravity;
import android.widget.TableRow;
import android.widget.TextView;
import ar.com.gmn.android.core.Numero;
import ar.com.gmn.android.core.Respuesta;

public class TRRespuestaDuelo extends TableRow {
	
	private NumeroView numero;
	private TextView bien;
	private TextView regular;

	public TRRespuestaDuelo(Context context, Respuesta r, int style) {
		super(context);

		
		numero = new NumeroView(context, r.getNumero());
		bien = new TextView(context);
		bien.setText(r.getCorrectos().toString());
		regular = new TextView(context);
		regular.setText(r.getRegulares().toString());

		

		bien.setTextAppearance(context, style);

		regular.setTextAppearance(context, style);
		numero.setTextAppearance(context, style);
		
		bien.setGravity(Gravity.CENTER);
		regular.setGravity(Gravity.CENTER);
		numero.setGravity(Gravity.CENTER);
		
		this.addView(numero);
		this.addView(bien);
		this.addView(regular);

	}

	public void setTextFont(Typeface type) {
		
		bien.setTypeface(type);
		regular.setTypeface(type);
		numero.setTypeface(type);
	}

	public void setBien(Integer i) {
		this.bien.setText(i.toString());
	}

	public void setRegular(Integer i) {
		this.regular.setText(i.toString());
	}

	public void setNumero(Numero n) {
		this.numero = new NumeroView(this.getContext(), n);
	}



}
