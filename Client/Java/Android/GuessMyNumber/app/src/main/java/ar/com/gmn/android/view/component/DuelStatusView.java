package ar.com.gmn.android.view.component;

import android.content.Context;
import android.content.Intent;
import android.graphics.Typeface;
import android.os.Bundle;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import org.w3c.dom.Text;

import ar.com.gmn.android.DuelFragmentActivity;
import ar.com.gmn.android.R;
import ar.com.gmn.android.core.DuelManager;
import ar.com.gmn.android.core.EstadoDuelo;
import ar.com.gmn.android.core.HistoricoDuelo;
import ar.com.gmn.android.core.Player;

/**
 * Created by Fcostazini on 08/05/2014.
 */
public class DuelStatusView extends LinearLayout implements View.OnClickListener {
    private EstadoDuelo estadoDuelo;
    private Player player;
    private String idDuelo;

    public String getIdDuelo() {
        return idDuelo;
    }

    public void setIdDuelo(String idDuelo) {
        this.idDuelo = idDuelo;
    }

    public DuelStatusView(Context context) {
        super(context);
        initView(context);
    }
    public DuelStatusView(Context context,AttributeSet attrs) {
        super(context,attrs);
        initView(context);
    }


    private void initView(Context context) {
        final LayoutInflater inflater = (LayoutInflater)
                context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        if(inflater != null){
            inflater.inflate(R.layout.duel_item, this);
        }
        Typeface typeFace = Typeface.createFromAsset(context.getAssets(),
                "fonts/EraserDust.ttf");
        ((TextView)this.findViewById(R.id.nombre)).setTypeface(typeFace);
        this.setOnClickListener(this);

    }

    public EstadoDuelo getEstadoDuelo() {
        return estadoDuelo;
    }


    private String getDescripcionEstado(EstadoDuelo estadoDuelo) {
        switch (estadoDuelo){
            case PENDIENTE:
                return this.getResources().getString(R.string.duelo_pendiente);
            case ACTIVO:
                return this.getResources().getString(R.string.duelo_activo);
            case CANCELADO:
                return this.getResources().getString(R.string.duelo_cancelado);
            case ESPERANDO:
                return this.getResources().getString(R.string.duelo_esperando);
            case GANADO:
                return this.getResources().getString(R.string.duelo_ganado);
            case PERDIDO:
                return this.getResources().getString(R.string.duelo_perdido);

        }
        return null;
    }

    public Player getPlayer() {
        return player;
    }

    public void setPlayer(Player player) {
        this.player = player;
        ((TextView)this.findViewById(R.id.nombre)).setText(player.getNombre());
        //((ImageView)this.findViewById(R.id.foto)).setImageBitmap(player.getFoto());
        ((ImageView)this.findViewById(R.id.foto)).setImageResource(R.drawable.ic_launcher);
    }



    @Override
    public void onClick(View v) {
        DuelStatusView d = (DuelStatusView)v;
        Intent intent = new Intent(this.getContext(), DuelFragmentActivity.class);
        intent.putExtra(DuelManager.DUEL_ID, d.getIdDuelo());
        this.getContext().startActivity(intent);
    }
}
