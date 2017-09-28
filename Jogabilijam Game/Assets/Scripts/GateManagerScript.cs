using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class GateManagerScript : MonoBehaviour {

	private const int maxPlayers = 4;

	public int Gates;

	public Text positionsText;

	public int[] CurrentGate;
	public int[] Laps;
	private int[] CurrentOrder;
	private int[] totalGatesCrossed;
	private float[] distances;
	List<int> positions;


	private Transform Child;
	private GateScript ChildGateScript;

	// Use this for initialization
	void Start () 
	{
		Gates = transform.childCount;
		DefineGateNumbers ();
		CurrentGate = new int[maxPlayers];
		Laps = new int[maxPlayers];
		CurrentOrder = new int[maxPlayers];
		totalGatesCrossed = new int[maxPlayers];
		distances = new float[maxPlayers];
		positions = new List<int>();
		for (int i = 0; i < maxPlayers; i++) {
			positions.Add (i);
			CurrentGate[i] = -1;
			Laps[i] = 0;
			CurrentOrder [i] = i;
			totalGatesCrossed [i] = 0;
		}

		positionsText.text = "Posicoes = ";
	}

	// Update is called once per frame
	void Update () 
	{
		CheckForGates (); //confere se alguém atravessou um gate e qual
		CheckForLaps ();  //confere se alguém completou uma volta e contabiliza isso
		updatePositionsText(); //atualiza o texto com as posições na tela
		distances = CalculateDistanceNextGate(); // calcula a distancia de cada jogador ao seu proximo gate
	}

	void FixedUpdate(){
		CurrentOrder = GetPositions(); // calcula a posição de cada jogador e salva isso no array CurrentOrder
	}

	void DefineGateNumbers() // da a cada gate o seu numero correto
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
		for (int j = 0; j < maxPlayers; j++) {
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
		for (int j = 0; j < maxPlayers; j++) {
			if (CurrentGate[j] == Gates) {
				Laps[j]++;
				CurrentGate[j] = 0;
			}
		}
	}

	float[] CalculateDistanceNextGate(){
		GameObject[] players = new GameObject[maxPlayers];
		float[] distances = new float[maxPlayers];
		for (int i = 0; i < maxPlayers; i++) {
			players [i] = GameObject.FindGameObjectWithTag ("P" + (i+1).ToString ());
		}
		for (int i = 0; i < maxPlayers; i++) {
			Vector3 playerPosition = players[i].transform.position;
			Vector3 gatePosition = this.gameObject.transform.GetChild (NextGate(CurrentGate [i])).transform.position;
			distances [i] = Vector3.Distance (playerPosition, gatePosition);
		}
		return distances;
	}

	int NextGate(int currentGate){ // verifica qual o gate seguinte dado o gate atual
		if (currentGate + 1 == Gates) //caso seja o ultimo, o proximo é o primeiro
			return 0;
		else
			return currentGate + 1; // em todos os outros casos, é o seguinte
	}

	private void updatePositionsText(){
		string aux = "Posicoes: ";
		for (int i = 0; i < maxPlayers; i++) {
			aux += i + " = " + CurrentOrder [i].ToString () + "; ";
		}
		positionsText.text = aux;
	}

	private int[] GetPositions(){
		List<float> totalDistanceTravelled = new List<float>(); //cria lista de distancia total que cada jogador andou
		for(int i=0;i<maxPlayers;i++){
			totalDistanceTravelled.Add(totalGatesCrossed[i]*200+100-distances[i]);  // soma a distancia desde o ultimo gate (aproximada)
		}																			// à distancia acumulada com os outros gates
		float currentMax = 0;
		for (int i = 0; i < maxPlayers; i++) { //ordena os jogadores baseado nessa distancia total
			currentMax = totalDistanceTravelled.Max ();
			positions [totalDistanceTravelled.IndexOf (currentMax)] = i;
			totalDistanceTravelled [totalDistanceTravelled.IndexOf (currentMax)] = -1;
		}
		return positions.ToArray ();
	}


}
