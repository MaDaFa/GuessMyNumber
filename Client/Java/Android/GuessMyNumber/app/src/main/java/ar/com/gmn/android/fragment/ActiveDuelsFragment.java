package ar.com.gmn.android.fragment;

import android.graphics.PorterDuff;
import android.graphics.Typeface;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TableLayout;
import android.widget.TextView;
import android.widget.Toast;

import ar.com.gmn.android.R;
import ar.com.gmn.android.core.DuelManager;
import ar.com.gmn.android.core.EstadoDuelo;
import ar.com.gmn.android.core.Evaluador;
import ar.com.gmn.android.core.Numero;
import ar.com.gmn.android.core.Player;
import ar.com.gmn.android.core.Respuesta;
import ar.com.gmn.android.view.component.CustomNumberPicker2;
import ar.com.gmn.android.view.component.DuelStatusView;
import ar.com.gmn.android.view.component.TRRespuestaDuelo;

public class ActiveDuelsFragment extends Fragment {
    public final static  int NUMBERPIKER_STYLE = 1;

private View container;
private DuelManager duelManager;    
	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup c,
			Bundle savedInstanceState) {

		this.container = inflater.inflate(R.layout.active_duels_fragment, null);
        LinearLayout activeDuels = (LinearLayout)this.container.findViewById(R.id.active_duels_container);
        DuelStatusView d = new DuelStatusView(this.container.getContext());
        d.setPlayer(new Player("Facundo Costa Zini","fcostazini", "unafoto"));
        d.setIdDuelo("duelo1");

        activeDuels.addView(d);
        d = new DuelStatusView(this.container.getContext());
        d.setPlayer(new Player("Dario Camarro","dcamarro", "unafoto"));
        d.setIdDuelo("duelo2");

        activeDuels.addView(d);
        d = new DuelStatusView(this.container.getContext());
        d.setPlayer(new Player("Mauro Agnoletti","magnoletti", "unafoto"));
        d.setIdDuelo("duelo2");

        activeDuels.addView(d);
        d = new DuelStatusView(this.container.getContext());
        d.setPlayer(new Player("Mauro Agnoletti2","magnoletti", "unafoto"));
        d.setIdDuelo("duelo3");

        activeDuels.addView(d);
        d = new DuelStatusView(this.container.getContext());
        d.setPlayer(new Player("Mauro Agnoletti3","magnoletti", "unafoto"));
        d.setIdDuelo("duelo4");


        LinearLayout pendingDuels = (LinearLayout)this.container.findViewById(R.id.pending_duels_container);
        d = new DuelStatusView(this.container.getContext());
        d.setPlayer(new Player("Facundo Costa Zini","fcostazini", "unafoto"));
        d.setIdDuelo("duelo5");

        pendingDuels.addView(d);
        d = new DuelStatusView(this.container.getContext());
        d.setPlayer(new Player("Dario Camarro","dcamarro", "unafoto"));
        d.setIdDuelo("duelo6");

        pendingDuels.addView(d);
        d = new DuelStatusView(this.container.getContext());
        d.setPlayer(new Player("Mauro Agnoletti","magnoletti", "unafoto"));
        d.setIdDuelo("duelo7");

        pendingDuels.addView(d);
        
		return this.container;
	}




}
