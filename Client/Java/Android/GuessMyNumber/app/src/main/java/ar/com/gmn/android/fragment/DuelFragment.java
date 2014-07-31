package ar.com.gmn.android.fragment;

import android.graphics.PorterDuff;
import android.graphics.Typeface;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TableLayout;
import android.widget.TextView;
import android.widget.Toast;
import ar.com.gmn.android.R;
import ar.com.gmn.android.core.Duelo;
import ar.com.gmn.android.core.Evaluador;
import ar.com.gmn.android.core.Numero;
import ar.com.gmn.android.core.Respuesta;


import ar.com.gmn.android.view.component.CustomNumberPicker2;
import ar.com.gmn.android.view.component.TRRespuestaDuelo;

public class DuelFragment extends Fragment {
    public final static  int NUMBERPIKER_STYLE = 1;
	private Evaluador e;
	private CustomNumberPicker2 digit1;
	private CustomNumberPicker2 digit2;
	private CustomNumberPicker2 digit3;
	private CustomNumberPicker2 digit4;
	private ImageView probar;
	private Integer turno = 0;
	private Numero codigo;
	private TableLayout tResultados;
	private View container;
	private Typeface type;
	private TextView me;
	private TextView him;
    private Duelo duelo;

    protected void addRespuestaP1(Respuesta r) {
		TableLayout tResultados = (TableLayout) container
				.findViewById(R.id.results_me);
		TRRespuestaDuelo trRespuesta = new TRRespuestaDuelo(container.getContext(), r,
				R.style.ResultadoDuelo);
		trRespuesta.setTextFont(type);
		tResultados.addView(trRespuesta);

	}

    protected void addRespuestaP2(Respuesta r) {
        TableLayout tResultados = (TableLayout) container
                .findViewById(R.id.results_him);
        TRRespuestaDuelo trRespuesta = new TRRespuestaDuelo(container.getContext(), r,
                R.style.ResultadoDuelo);
        trRespuesta.setTextFont(type);
        tResultados.addView(trRespuesta);

    }

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup c,
			Bundle savedInstanceState) {

		this.container = inflater.inflate(R.layout.fragment_duel, null);

		this.me = (TextView)this.container.findViewById(R.id.meDuel);
		this.him = (TextView)this.container.findViewById(R.id.himDuel);
		
		this.me.setTypeface(type);
		codigo = Numero.getRandom(4);
		e = new Evaluador(codigo);
		this.type = Typeface.createFromAsset(
				container.getContext().getAssets(), "fonts/HandWrite.ttf");
		digit1 = (CustomNumberPicker2) this.container.findViewById(R.id.digit1);
        digit2 = (CustomNumberPicker2) this.container.findViewById(R.id.digit2);
        digit3 = (CustomNumberPicker2) this.container.findViewById(R.id.digit3);
        digit4 = (CustomNumberPicker2) this.container.findViewById(R.id.digit4);

		
		this.me.setTypeface(type);
        this.me.setText(duelo.getP1().getNombre());
        for(Respuesta r : duelo.getRespuestasP1()){
            addRespuestaP1(r);
        }
        this.him.setText(duelo.getP2().getNombre());
        for(Respuesta r : duelo.getRespuestasP2()){
            addRespuestaP2(r);
        }
		this.him.setTypeface(type);

		probar = (ImageView) container.findViewById(R.id.prueba);


		probar.setBackgroundResource(R.drawable.downarrow);
		// LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(40,
        // 120);
		probar.getBackground().setColorFilter(0x80000000,
				PorterDuff.Mode.SRC_ATOP);


		createTablaResultados();

		probar.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {

				Numero n = new Numero();
				n.set(1, digit1.getValue());
				n.set(2, digit2.getValue());
				n.set(3, digit3.getValue());
				n.set(4, digit4.getValue());

				if (n.tieneRepetidos()) {

					CharSequence text = getResources().getString(R.string.repetidos);
					int duration = Toast.LENGTH_SHORT;

					Toast toast = Toast.makeText(v.getContext(), text, duration);
					toast.show();
				} else {
					turno++;
					Respuesta r = e.evaluar(n);
					addRespuestaP1(r);
					if (r.resuelto()) {
						digit1.setCorrecto();
						digit2.setCorrecto();
						digit3.setCorrecto();
						digit4.setCorrecto();

						probar.setEnabled(false);
					} else {
//						if (turno >= 7) {
//							digit1.setIncorrecto(codigo.get(1));
//							digit2.setIncorrecto(codigo.get(2));
//							digit3.setIncorrecto(codigo.get(3));
//							digit4.setIncorrecto(codigo.get(4));
//							probar.setText("X");
//							probar.setEnabled(false);
//						}
					}

				}
			}

		});

		return this.container;
	}

	public void createTablaResultados() {
		tResultados = (TableLayout) container.findViewById(R.id.results);
		((TextView) container.findViewById(R.id.textBienMe)).setTypeface(type);
		((TextView) container.findViewById(R.id.textNumeroMe)).setTypeface(type);
		
		((TextView) container.findViewById(R.id.textRegularMe)).setTypeface(type);
		((TextView) container.findViewById(R.id.textBienHim)).setTypeface(type);
		((TextView) container.findViewById(R.id.textNumeroHim)).setTypeface(type);
		
		((TextView) container.findViewById(R.id.textRegularHim)).setTypeface(type);

	}



    public void setDuelo(Duelo duelo) {
        this.duelo = duelo;
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
			View rootView = inflater.inflate(R.layout.activity_duel_fragment, container,
					false);
			return rootView;
		}
	}

}
