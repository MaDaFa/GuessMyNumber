package ar.com.gmn.android.core;

/**
 * Created by Fcostazini on 08/05/2014.
 */
public enum EstadoDuelo {
PENDIENTE(1),CANCELADO(2),GANADO(3),PERDIDO(4),ACTIVO(5),ESPERANDO(6);
    private int code;
    public int getCode(){
        return code;
    }

    private EstadoDuelo(int code){
        this.code = code;
    }
}
