package ar.com.gmn.android.core;

/**
 * Created by Fcostazini on 08/05/2014.
 */
public class Player {
    private String nombre;
    private String id;
    private String foto;

    public Player(String nombre, String id, String foto) {
        this.nombre = nombre;
        this.id = id;
        this.foto = foto;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getFoto() {
        return foto;
    }

    public void setFoto(String foto) {
        this.foto = foto;
    }
}
