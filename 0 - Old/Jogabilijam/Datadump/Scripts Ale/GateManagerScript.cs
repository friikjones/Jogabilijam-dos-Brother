using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class GateManagerScript : MonoBehaviour {

	public const int maxPlayers = 4;
	private const int maxLaps = 3;

	public int Gates;


	public RawImage[] positionImages;
	public RawImage[] lapImages;
	public Canvas canvas;
	public GameObject VictoryPanel;
	public RawImage countdownImage;
	public CarControllerMinimal[] ccm;

	public int[] CurrentGate;
	public int[] Laps;
	private int[] CurrentOrder;
	private int[] totalGatesCrossed;
	private float[] distances;
	List<int> positions;
	public Text VictoryText;

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

		VictoryPanel = GameObject.Find ("VictoryPanel");
		VictoryPanel.SetActive (false);

		for (int i = 0; i < maxPlayers; i++) {
			positions.Add (i);
			CurrentGate[i] = -1;
			Laps[i] = 0;
			CurrentOrder [i] = i;
			totalGatesCrossed [i] = 0;
		}

		StartCoroutine (Countdown ());

			
	}

	// Update is called once per frame
	void Update () 
	{
		CheckForGates (); //confere se alguém atravessou um gate e qual
		CheckForLaps ();  //confere se alguém completou uma volta e contabiliza isso
		distances = CalculateDistanceNextGate(); // calcula a distancia de cada jogador ao seu proximo gate
	}

	void FixedUpdate(){
		CurrentOrder = GetPositions(); // calcula a posição de cada jogador e salva isso no array CurrentOrder
		updateImages();
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
			if (Laps [j] == maxLaps)
				terminaPartida (j);
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

	private void terminaPartida(int vencedor){
		//termina a partida
		for (int i = 0; i < maxPlayers; i++) {
			positionImages [i].enabled = false;
			lapImages [i].enabled = false;
		}
		VictoryPanel.SetActive (true);
	}

	private void updateImages(){
		for (int i = 0; i < maxPlayers; i++) {
			positionImages [i].texture = Resources.Load((CurrentOrder [i]+1).ToString() + "colorido") as Texture;
			lapImages [i].texture = Resources.Load ("Laps\\LAPS" + (Laps [i] + 1).ToString () + "-3") as Texture;
		}
	}

	IEnumerator Countdown(){
		countdownImage.enabled = false;
		yield return new WaitForSeconds (2);
		countdownImage.texture = Resources.Load ("3colorido") as Texture;
		countdownImage.enabled = true;
		yield return new WaitForSeconds (1);
		countdownImage.enabled = false;
		countdownImage.texture = Resources.Load ("2colorido") as Texture;
		countdownImage.enabled = true;
		yield return new WaitForSeconds (1);
		countdownImage.enabled = false;
		countdownImage.texture = Resources.Load ("1colorido") as Texture;
		countdownImage.enabled = true;
		yield return new WaitForSeconds (1);
		countdownImage.enabled = false;
		countdownImage.texture = Resources.Load ("GO") as Texture;
		countdownImage.enabled = true;
		yield return new WaitForSeconds (0.5f);
		countdownImage.enabled = false;
		for (int i = 0; i < maxPlayers; i++) {
			ccm [i].isMovementAllowed = true;
		}
	}

}
