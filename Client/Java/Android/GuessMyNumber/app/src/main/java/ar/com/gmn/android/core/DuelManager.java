package ar.com.gmn.android.core;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Vector;

/**
 * Created by Fcostazini on 08/05/2014.
 */
public class DuelManager {


    public static final  String DUEL_ID = "DUEL_ID";
    private static DuelManager instance;
    private Map<String,Duelo> duelos;
    private DuelManager(){
        this.duelos = new HashMap<>();
        inicializar();
    }

    private void inicializar() {
        Duelo d = new Duelo("duelo1");
        List<Respuesta> l1 = new Vector<>();
        l1.add(new Respuesta(new Numero("1234"),1,2));
        l1.add(new Respuesta(new Numero("1234"),1,2));
        l1.add(new Respuesta(new Numero("1234"),1,2));
        List<Respuesta> l2 = new Vector<>();
        l2.add(new Respuesta(new Numero("1234"),1,2));
        l2.add(new Respuesta(new Numero("1234"),1,2));
        d.setRespuestasP1(l1);
        d.setRespuestasP2(l2);
        d.setP1(new Player("Facundo Costa Zini","fcostazini","lala"));
        d.setP2(new Player("Juan","jj","lala"));
        this.duelos.put("duelo1",d);

        d = new Duelo("duelo2");
        l1 = new Vector<>();
        l1.add(new Respuesta(new Numero("1234"),1,2));
        l1.add(new Respuesta(new Numero("6523"),0,2));
        l1.add(new Respuesta(new Numero("0789"),1,1));
        l1.add(new Respuesta(new Numero("0789"),1,1));
        l2 = new Vector<>();
        l2.add(new Respuesta(new Numero("1234"),1,2));
        l2.add(new Respuesta(new Numero("1234"),1,2));
        l2.add(new Respuesta(new Numero("1234"),1,2));
        d.setRespuestasP1(l1);
        d.setRespuestasP2(l2);
        d.setP1(new Player("Facundo Costa Zini","fcostazini","lala"));
        d.setP2(new Player("Mauro Agnoletti","agnoletti","lala"));
        this.duelos.put("duelo2",d);

        d = new Duelo("duelo4");
        l1 = new Vector<>();
        l1.add(new Respuesta(new Numero("1234"),1,2));
        l1.add(new Respuesta(new Numero("6523"),0,2));
        l1.add(new Respuesta(new Numero("0789"),1,1));
        l1.add(new Respuesta(new Numero("0789"),1,1));
        l2 = new Vector<>();
        l2.add(new Respuesta(new Numero("1234"),1,2));
        l2.add(new Respuesta(new Numero("1234"),1,2));
        l2.add(new Respuesta(new Numero("1234"),1,2));
        d.setRespuestasP1(l1);
        d.setRespuestasP2(l2);
        d.setP1(new Player("Facundo Costa Zini","fcostazini","lala"));
        d.setP2(new Player("Mauro Agnoletti","agnoletti","lala"));
        this.duelos.put("duelo3",d);

        d = new Duelo("duelo4");
        l1 = new Vector<>();
        l1.add(new Respuesta(new Numero("1234"),1,2));
        l1.add(new Respuesta(new Numero("6523"),0,2));
        l1.add(new Respuesta(new Numero("0789"),1,1));
        l1.add(new Respuesta(new Numero("0789"),1,1));
        l2 = new Vector<>();
        l2.add(new Respuesta(new Numero("1234"),1,2));
        l2.add(new Respuesta(new Numero("1234"),1,2));
        l2.add(new Respuesta(new Numero("1234"),1,2));
        d.setRespuestasP1(l1);
        d.setRespuestasP2(l2);
        d.setP1(new Player("Facundo Costa Zini","fcostazini","lala"));
        d.setP2(new Player("Mauro Agnoletti","agnoletti","lala"));
        this.duelos.put("duelo4",d);

    }

    public static DuelManager getInstance() {
        if(DuelManager.instance== null){
            DuelManager.instance = new DuelManager();
        }
        return DuelManager.instance;
    }

    public Duelo getDuelo(String dueloId){
        if(this.duelos.containsKey(dueloId)){
            return this.duelos.get(dueloId);
        }else{
            return this.duelos.get("duelo1");
        }
    }
}
