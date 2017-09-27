using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GateManagerScript : MonoBehaviour {

	public int Gates;

	public Text positionsText;

	public int[] CurrentGate;
	public int[] Laps;
	private int[] CurrentOrder;
	private int[] totalGatesCrossed;
	private float[] distances;


	private Transform Child;
	private GateScript ChildGateScript;

	// Use this for initialization
	void Start () 
	{
		Gates = transform.childCount;
		DefineGateNumbers ();
		CurrentGate = new int[4];
		Laps = new int[4];
		CurrentOrder = new int[4];
		totalGatesCrossed = new int[4];
		distances = new float[4];
		for (int i = 0; i < 4; i++) {
			CurrentGate[i] = 0;
			Laps[i] = 0;
			CurrentOrder [i] = i;
			totalGatesCrossed [i] = 0;
		}

		positionsText.text = "Posicoes = ";
	}

	// Update is called once per frame
	void Update () 
	{
		CheckForGates ();
		CheckForLaps ();
		//Debug.Log ("totalGatesCrossed[0] = " + totalGatesCrossed [0]);
		updatePositionsText();
	}

	void LateUpdate(){
		distances = CalculateDistanceNextGate();
		CurrentOrder = GetPositions();
	}

	void DefineGateNumbers()
	{

		for (int i =0; i<Gates;i++)
		{
			//Child = Transform.GetChild (i);
			ChildGateScript = this.gameObject.transform.GetChild (i).GetComponent<GateScript> ();

			ChildGateScript.GateNumber = i;
		}

	}

	void CheckForGates()
	{
		for (int j = 0; j < 4; j++) {
			//Para cada jogador
			for (int i = 0; i < Gates; i++) {
				//Child = Transform.GetChild (i);
				ChildGateScript = this.gameObject.transform.GetChild (i).GetComponent<GateScript> ();
				if (ChildGateScript.Atravessado[j] == true) {
					if (ChildGateScript.GateNumber == NextGate(CurrentGate[j])) {
						totalGatesCrossed [j]++;
						CurrentGate[j]++;
						ChildGateScript.Atravessado[j] = false;
					} else {
						ChildGateScript.Atravessado[j] = false;
					}
				}
			}
		}
			
	}

	void CheckForLaps()
	{
		for (int j = 0; j < 4; j++) {
			if (CurrentGate[j] == Gates) {
				Laps[j]++;
				CurrentGate[j] = 0;
			}
		}
	}

	int[] CheckPositions(){
		int[] positions = new int[4];
		int first = 0;
		List<int> auxList = new List<int> ();
		for (int i = 0; i < 4; i++) {
				auxList.Add (i);
		}
		while (auxList.Count != 1) {
			//first = auxList.ElementAt (0);
			first = 0;
			for (int i = 1; i < auxList.Count; i++) {
				Debug.Log ("first, i =" + first + " " + i);
				Debug.Log ("auxList.Count = " + auxList.Count);
				if (isInFront (auxList[i], auxList[first]))
					first = i;
			}
			auxList.RemoveAt(first);
			positions[first]=3-auxList.Count;
		}
		positions [auxList[0]] = 3;


		return positions;

	}

	int [] GetPositions()
	{
		List<int> positions = new List<int>{ 0, 1, 2, 3 };
		positions.Sort( (a, b) => { return isInFront(a, b) ? 1 : -1; } );
		Debug.Log ("sorted positions: 0 = " + positions [0] + "; 1 = " + positions [1] + "; 2 = " + positions [2] + "; 3 = " + positions [3]);
		return positions.ToArray();
	}

	float[] CalculateDistanceNextGate(){
		GameObject[] players = new GameObject[4];
		float[] distances = new float[4];
		for (int i = 0; i < 4; i++) {
			players [i] = GameObject.FindGameObjectWithTag ("P" + (i+1).ToString ());
		}
		for (int i = 0; i < 4; i++) {//maximo esta 1 por enquanto porque não coloquei com varios jogadores
			Vector3 playerPosition = players[i].transform.position;
			Vector3 gatePosition = this.gameObject.transform.GetChild (NextGate(CurrentGate [i])).transform.position;
			distances [i] = Vector3.Distance (playerPosition, gatePosition);
		}
//		string aux = "";
//		for (int i = 0; i < 4; i++)
//			aux += distances [i].ToString () + " "; 
//		Debug.Log ("distances = " + aux);
		return distances;
	}

	int NextGate(int currentGate){
		if (currentGate + 1 == Gates)
			return 0;
		else
			return currentGate + 1;
	}

	bool isInFront(int first, int second){//recebe indice dos jogadores a verificar
		//quem tem mais gates
//		Debug.Log("first, second = " + first + " " + second);
//		Debug.Log ("totalGatesCrossed for each = " + totalGatesCrossed [first] + " " + totalGatesCrossed [second]);
		if (first == second)
			return false;
		if (totalGatesCrossed [first] != totalGatesCrossed [second]) {
			Debug.Log ("num of crossed gates is different");
			Debug.Log ("gatesCrossed for each = " + totalGatesCrossed [first] + " " + totalGatesCrossed [second]);
			if (totalGatesCrossed [first] > totalGatesCrossed [second]) {
				Debug.Log (first + " crossed more gates than " + second);
				return true;
			} else {
				Debug.Log (second + " crossed more gates than " + first);
				return false;
			}
		} else { //se os gates estao iguais, checa distancia
			if (distances [first] >= distances [second]) {
				Debug.Log (second + " is in front of " + first);
				return true;
			}
			else {
				Debug.Log (first + " is in front of " + second);
				return false;
			}
		}
	}

	private void updatePositionsText(){
		string aux = "Posicoes: ";
		for (int i = 0; i < 4; i++) {
			aux += i + " = " + CurrentOrder [i].ToString () + "; ";
		}
		positionsText.text = aux;
	}



}
