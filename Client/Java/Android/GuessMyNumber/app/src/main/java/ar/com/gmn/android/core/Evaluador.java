package ar.com.gmn.android.core;


public class Evaluador {
	private Numero incognita;

	public Evaluador(Numero incognita) {
		this.incognita = incognita;
	}

	public Evaluador(String valor) {

		this.incognita = new Numero(valor);
	}

	public Respuesta evaluar(String valor) {
		return evaluar(new Numero(valor));
	}

	public Respuesta evaluar(Numero aComparar) {
		Respuesta r = new Respuesta();
		r.setNumero(aComparar);
		int i = 1;
		for (Integer comparado : aComparar.getNumero()) {
			if (incognita.get(i) == comparado) {
				r.addCorrectos();
			} else if (incognita.contiene(comparado)) {
				r.addRegular();
			}
			i++;
		}
		return r;

	}

	public Numero getNumero() {
		return this.incognita;
	}

}
