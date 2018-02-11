using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManagerScript : MonoBehaviour {

	public int Gates;

	public int CurrentGate;
	public int Laps;

	private Transform Child;
	private GateScript ChildGateScript;

	// Use this for initialization
	void Start () 
	{
		Gates = transform.childCount;
		DefineGateNumbers ();

		CurrentGate = 0;
		Laps = 0;
	}

	// Update is called once per frame
	void Update () 
	{
		CheckForGates ();
		CheckForLaps ();
	}

	void DefineGateNumbers()
	{

		for (int i =0; i<Gates;i++)
		{
			//Child = Transform.GetChild (i);
			ChildGateScript = this.gameObject.transform.GetChild (i).GetComponent<GateScript> ();

			ChildGateScript.GateNumber = i + 1;
		}

	}

	void CheckForGates()
	{

		for (int i =0; i<Gates;i++)
		{
			//Child = Transform.GetChild (i);
			ChildGateScript = this.gameObject.transform.GetChild (i).GetComponent<GateScript> ();
			if (ChildGateScript.Atravessado == true) 
			{
				if (ChildGateScript.GateNumber == CurrentGate + 1) 
				{
					CurrentGate++;
					ChildGateScript.Atravessado = false;
				}else{
					ChildGateScript.Atravessado = false;
				}
			}
		}
			
	}

	void CheckForLaps()
	{
		if (CurrentGate == Gates) 
		{
			Laps++;
			CurrentGate = 0;
		}
	}



}
