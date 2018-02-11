using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour {

	public int GateNumber;
	public bool[] Atravessado;

	// Use this for initialization
	void Start () 
	{
        //inicializa as vriáveis booleanas, para determinar se os gates foram atrvessados.
		Atravessado = new bool[4];
		for(int i=0;i<4;i++)
			Atravessado[i] = false;

	}


	void OnTriggerEnter(Collider other) //inicializado quando um dos carros passam por um dos gates.
	{

		switch (other.tag)
		{
		case "P1":
			Atravessado[0] = true;
			break;
		case "P2":
			Atravessado[1] = true;
			break;
		case "P3":
			Atravessado[2] = true;
			break;
		case "P4":
			Atravessado[3] = true;
			break;
		default:
			break;
		}

	}
}
