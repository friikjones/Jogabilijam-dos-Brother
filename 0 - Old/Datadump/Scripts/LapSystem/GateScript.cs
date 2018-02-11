using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour {

	public int GateNumber;
	public bool Atravessado;

	// Use this for initialization
	void Start () 
	{
		
		Atravessado = false;

	}

	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			Atravessado = true;
		}

	}
}
