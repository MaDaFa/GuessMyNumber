package ar.com.gmn.android.core;


public class Respuesta {
	private Integer correctos = 0;
	private Integer regulares = 0;
	private Numero numero;

	public Respuesta(Integer correctos, Integer regulares) {
		this.correctos = correctos;
		this.regulares = regulares;
	}
    public Respuesta(Numero n,Integer correctos, Integer regulares) {
        this.correctos = correctos;
        this.regulares = regulares;
        this.numero = n;
    }
	public Respuesta(){
		
	}
	public Integer getCorrectos() {
		return correctos;
	}

	public void addCorrectos() {
		this.correctos++;
	}

	public Integer getRegulares() {
		return regulares;
	}

	public void addRegular() {
		this.regulares++;
	}

	public boolean resuelto() {
		return this.correctos == 4;
	}

	public Numero getNumero() {
		return numero;
	}

	public void setNumero(Numero numero) {
		this.numero = numero;
	}

	@Override
	public boolean equals(Object obj) {
		Respuesta r = (Respuesta) obj;
		return this.numero.equals(r.getNumero());
	}

	@Override
	public String toString() {
		return "["+correctos+"B, " + regulares + "R, numero=" + numero + "]";
	}
	public Integer totalEncontrados() {
		// TODO Auto-generated method stub
		return this.correctos + this.regulares;
	}

	
}
