using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour {

	public int GateNumber;
	public bool[] Atravessado;

	// Use this for initialization
	void Start () 
	{
		Atravessado = new bool[4];
		for(int i=0;i<4;i++)
			Atravessado[i] = false;

	}

	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter(Collider other)
	{
//		if (other.tag == "P1") 
//		{
//			Atravessado[0] = true;
//		}
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
