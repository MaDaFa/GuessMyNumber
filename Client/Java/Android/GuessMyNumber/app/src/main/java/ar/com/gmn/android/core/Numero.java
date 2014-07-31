package ar.com.gmn.android.core;

import java.util.Collections;
import java.util.List;
import java.util.Random;
import java.util.Vector;

public class Numero {

	private List<Integer> numero = new Vector<Integer>();

	public static Numero getRandom(Integer digits) {
		Numero n = new Numero();
		Random r = new Random();
		Integer d = r.nextInt(9);

		for (int i = 1; i <= digits; i++) {

			while (n.contiene(d)) {
				d = r.nextInt(9);
			}
			n.set(i, d);
		}
		return n;
	}

	public Numero(String numero) {
		this.numero = toArray(numero);
	}

	public void rotarPosiciones(Integer a, Integer b) {
		Integer valorA = get(a);
		Integer valorB = get(b);
		set(a, valorB);
		set(b, valorA);
	}

	public Integer getPosicionDigito(Integer valor) {
		if (numero.contains(valor)) {
			return this.numero.indexOf(valor);
		}
		return 0;
	}

	public Integer getSiguientePosicion(Integer a) {
		if (a >= this.numero.size()) {
			return 1;
		} else {
			return a + 1;
		}
	}

	public Numero clone() {
		Numero n = new Numero();
		Collections.copy(n.numero, this.numero);
		return n;
	}

	public Numero() {
		this.numero = new Vector<Integer>();
		this.numero.add(-1);
		this.numero.add(-1);
		this.numero.add(-1);
		this.numero.add(-1);

	}

	public boolean isInitial() {
		return this.numero.get(0) < 0 && this.numero.get(1) < 0
				&& this.numero.get(2) < 0 && this.numero.get(3) < 0;

	}

	public boolean isCompleto() {
		return this.numero.get(0) >= 0 && this.numero.get(1) >= 0
				&& this.numero.get(2) >= 0 && this.numero.get(3) >= 0;
	}

	public void set(Integer posicion, Integer valor) {
		this.numero.set(posicion - 1, valor);
	}

	private List<Integer> toArray(String valor) {

		List<Integer> a = new Vector<Integer>();
		for (int i = 0; i < valor.length(); i++) {
			a.add(Integer.parseInt(String.valueOf(valor.charAt(i))));
		}
		return a;
	}

	public Integer get(Integer i) {
		return this.numero.get(i - 1);
	}

	@Override
	public boolean equals(Object obj) {

		Numero aComparar = (Numero) obj;
		for (int j = 1; j <= numero.size(); j++) {
			if (!aComparar.get(j).equals(this.get(j))) {
				return false;
			}
		}
		return true;

	}

	@Override
	public String toString() {

		return numero.toString();
	}

	public List<Integer> getNumero() {

		return this.numero;
	}

	public boolean contiene(Integer comparado) {

		return this.numero.contains(comparado);
	}

	public boolean tieneRepetidos() {
		for (int i = 1; i <=this.numero.size(); i++) {
			for (int j = 1; j <=this.numero.size(); j++) {
				if(i!=j && this.get(i)==this.get(j)){
					return true;
				}
			}
		}
		return false;
	}

	public List<Integer> getPosicionesIniciales() {
		List<Integer> posiciones = new Vector<Integer>();
		for (Integer integer : numero) {
			if (integer >= 0) {
				posiciones.add(integer);
			}
		}
		return posiciones;
	}

}
