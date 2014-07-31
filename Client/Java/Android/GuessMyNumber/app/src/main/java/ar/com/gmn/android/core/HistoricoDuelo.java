package ar.com.gmn.android.core;

import java.util.List;

/**
 * Created by Fcostazini on 08/05/2014.
 */
public class HistoricoDuelo {
    private List<Respuesta> respuestasP1;
    private List<Respuesta> respuestasP2;
    private String idDuelo;

    public HistoricoDuelo(List<Respuesta> respuestasP1, List<Respuesta> respuestasP2, String idDuelo) {
        this.respuestasP1 = respuestasP1;
        this.respuestasP2 = respuestasP2;
        this.idDuelo = idDuelo;
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

    public String getIdDuelo() {
        return idDuelo;
    }

    public void setIdDuelo(String idDuelo) {
        this.idDuelo = idDuelo;
    }

    public void addRespuestaP1(Respuesta r){
        respuestasP1.add(r);
    }
    public void addRespuestaP2(Respuesta r){
        respuestasP2.add(r);
    }
}
