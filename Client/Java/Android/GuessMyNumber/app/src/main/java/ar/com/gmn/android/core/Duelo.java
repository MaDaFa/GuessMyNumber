package ar.com.gmn.android.core;

import java.util.List;

/**
 * Created by Fcostazini on 09/05/2014.
 */
public class Duelo {
    private final String dueloId;
    private List<Respuesta> respuestasP1;
    private List<Respuesta> respuestasP2;
    private Player p2;
    private Player p1;

    public Duelo(String dueloId) {
        this.dueloId = dueloId;
    }

    public String getDueloId() {
        return dueloId;
    }

    public List<Respuesta> getRespuestasP1() {
        return respuestasP1;
    }

    public void setRespuestasP1(List<Respuesta> respuestasP1) {
        this.respuestasP1 = respuestasP1;
    }

    public List<Respuesta> getRespuestasP2() {
        return respuestasP2;
    }

    public void setRespuestasP2(List<Respuesta> respuestasP2) {
        this.respuestasP2 = respuestasP2;
    }

    public Player getP2() {
        return p2;
    }

    public void setP2(Player p2) {
        this.p2 = p2;
    }

    public Player getP1() {
        return p1;
    }

    public void setP1(Player p1) {
        this.p1 = p1;
    }
}
