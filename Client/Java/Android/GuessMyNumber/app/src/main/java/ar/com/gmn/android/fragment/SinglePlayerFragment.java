package ar.com.gmn.android.fragment;

import android.graphics.Typeface;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TableLayout;
import android.widget.TextView;
import android.widget.Toast;
import ar.com.gmn.android.R;
import ar.com.gmn.android.core.Evaluador;
import ar.com.gmn.android.core.Numero;
import ar.com.gmn.android.core.Respuesta;


import ar.com.gmn.android.view.component.CustomNumberPicker2;
import ar.com.gmn.android.view.component.TRRespuesta;

public class SinglePlayerFragment extends Fragment {
    public final static  int NUMBERPIKER_STYLE = 2;
	private Evaluador e;
	private CustomNumberPicker2 digit1;
	private CustomNumberPicker2 digit2;
	private CustomNumberPicker2 digit3;
	private CustomNumberPicker2 digit4;
	private ImageView probar;
	private Integer turno = 0;
	private Numero codigo;
	private TableLayout tResultados;
	private Typeface type;
	private View container;

	protected void addRespuesta(Respuesta r) {
		TableLayout tResultados = (TableLayout) this.container.findViewById(R.id.results);
		TRRespuesta trRespuesta = new TRRespuesta(this.container.getContext(), r,
				R.style.ResultadoSinglePLayer);
		trRespuesta.setTextFont(type);
		trRespuesta.setTurno(turno);
		tResultados.addView(trRespuesta);

	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup c,
			Bundle savedInstanceState) {
		this.container = inflater.inflate(R.layout.single_player_fragment, null);
		// Remove title bar

		codigo = Numero.getRandom(4);
		e = new Evaluador(codigo);
		this.type = Typeface.createFromAsset(this.container.getContext()
				.getAssets(), "fonts/EraserDust.ttf");
		digit1 = (CustomNumberPicker2)container.findViewById(R.id.digit1);
        digit2 = (CustomNumberPicker2)container.findViewById(R.id.digit2);
        digit3 = (CustomNumberPicker2)container.findViewById(R.id.digit3);
        digit4 = (CustomNumberPicker2)container.findViewById(R.id.digit4);




		probar = (ImageView)container.findViewById(R.id.prueba);




		createTablaResultados();

		probar.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {

				Numero n = new Numero();
				n.set(1, digit1.getValue());
				n.set(2, digit2.getValue());
				n.set(3, digit3.getValue());
				n.set(4, digit4.getValue());

				if (n.tieneRepetidos()) {

					CharSequence text = container.getResources().getString(R.string.repetidos);
					int duration = Toast.LENGTH_SHORT;

					Toast toast = Toast.makeText(v.getContext(), text,
							duration);
					toast.show();
				} else {
					turno++;
					Respuesta r = e.evaluar(n);
					addRespuesta(r);
					if (r.resuelto()) {
						digit1.setCorrecto();
						digit2.setCorrecto();
						digit3.setCorrecto();
						digit4.setCorrecto();

						probar.setEnabled(false);
					} else {
						if (turno >= 7) {
							digit1.setIncorrecto(codigo.get(1));
							digit2.setIncorrecto(codigo.get(2));
							digit3.setIncorrecto(codigo.get(3));
							digit4.setIncorrecto(codigo.get(4));

							probar.setEnabled(false);
						}
					}

				}
			}

		});
		return this.container;
	}

	public void createTablaResultados() {
		Typeface type = Typeface.createFromAsset(this.container.getContext().getAssets(),
				"fonts/EraserDust.ttf");
		tResultados = (TableLayout) this.container.findViewById(R.id.results);
		((TextView) this.container.findViewById(R.id.textBien)).setTypeface(type);
		((TextView) this.container.findViewById(R.id.textNumero)).setTypeface(type);
		((TextView) this.container.findViewById(R.id.textTurno)).setTypeface(type);
		((TextView) this.container.findViewById(R.id.textRegular)).setTypeface(type);
	}

	
}
