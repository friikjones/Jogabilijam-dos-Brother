using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class GateManagerScript : MonoBehaviour {

	public const int maxPlayers = 4;//Quantidade de jogadores
	private const int maxLaps = 3;//Quantidade de voltas

    public int Gates;


	public RawImage[] positionImages;
	public RawImage[] lapImages;
	public Canvas canvas;//Elements on a canvas are rendered AFTER scene rendering
    public GameObject VictoryPanel;
	public RawImage countdownImage;
	public CarControllerMinimalSaci ccmSaci;
	public CarControllerMinimalCuca ccmCuca;
	public CarControllerMinimalCurupira ccmCurupira;
	public CarControllerMinimalMULA ccmMula;


	public int[] CurrentGate;
	public int[] Laps;
	private int[] CurrentOrder;
	private int[] totalGatesCrossed;
	private float[] distances;
	List<int> positions;

	private Transform Child;
	private GateScript ChildGateScript; //variável do tipo "GateScript" que correlacionará cada gate existente no jogo.

	// Use this for initialization
	void Start () 
	{
        //instancia os gates e os numera da forma correta.
		Gates = transform.childCount;
		DefineGateNumbers ();
        //instancia as variáveis com base no número máximo de jogadores;
		CurrentGate = new int[maxPlayers];
		Laps = new int[maxPlayers];
		CurrentOrder = new int[maxPlayers];
		totalGatesCrossed = new int[maxPlayers];
		distances = new float[maxPlayers];
		positions = new List<int>();
        //instancia o "painel de vitória, que só aparecerá no final da corrida.
		VictoryPanel = GameObject.Find ("VictoryPanel");
		VictoryPanel.SetActive (false);

		for (int i = 0; i < maxPlayers; i++)
        {
			positions.Add (i);
			CurrentGate[i] = -1;
			Laps[i] = 0;
			CurrentOrder [i] = i;
			totalGatesCrossed [i] = 0;
		}

		StartCoroutine (Countdown ()); // inicia a contagem regressiva para a corrida

			
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
			ChildGateScript = this.gameObject.transform.GetChild (i).GetComponent<GateScript> ();
            ChildGateScript.GateNumber = i;
		}

	}

	void CheckForGates()//analisa os gates passados por cada jogador
	{
		for (int j = 0; j < maxPlayers; j++)
        {
			//Para cada jogador, faz uma comparação se aquele jogador passou por x gates e faz a contagem dos gates de cada jogador
			for (int i = 0; i < Gates; i++)
            {
				ChildGateScript = this.gameObject.transform.GetChild (i).GetComponent<GateScript> ();
				if (ChildGateScript.Atravessado[j] == true)
                {
					if (ChildGateScript.GateNumber == NextGate(CurrentGate[j]))
                    {
						totalGatesCrossed [j]++;
						CurrentGate[j]++;
						ChildGateScript.Atravessado[j] = false;
					}

                    else
                    {
						ChildGateScript.Atravessado[j] = false;
					}
				}
			}
		}
			
	}

	void CheckForLaps()
	{
		for (int j = 0; j < maxPlayers; j++) //para cada jogador
        {
			if (CurrentGate[j] == Gates)// se o número de gates cruzados é igual ao número total de gates
            {
				Laps[j]++;//contabiliza uma volta e zera o contador de gates.
				CurrentGate[j] = 0;
			}
			if (Laps [j] == maxLaps)//se o número de voltas foi atingido, termina a corrida.
				terminaPartida (j);
		}
	}

	float[] CalculateDistanceNextGate()
    {
		GameObject[] players = new GameObject[maxPlayers];
		float[] distances = new float[maxPlayers];

		for (int i = 0; i < maxPlayers; i++)
        {
			players [i] = GameObject.FindGameObjectWithTag ("P" + (i+1).ToString ());//a variável player recebe os dados do game object P1, P2, etc.
		}
		for (int i = 0; i < maxPlayers; i++)//calcula a distância para o próximo gate de cada player
        {
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
		

	private int[] GetPositions()//calcula a distância percorrida por cada carro e, no segundo "for" ordena a posição de cada um
    {
		List<float> totalDistanceTravelled = new List<float>(); //cria lista de distancia total que cada jogador andou
        float currentMax = 0;
        for (int i=0;i<maxPlayers;i++)
        {
			totalDistanceTravelled.Add(totalGatesCrossed[i]*200+100-distances[i]);  // soma a distancia desde o ultimo gate (aproximada)
		}																			// à distancia acumulada com os outros gates
		
		for (int i = 0; i < maxPlayers; i++)//ordena os jogadores baseado nessa distancia total
        { 
			currentMax = totalDistanceTravelled.Max ();
			positions [totalDistanceTravelled.IndexOf (currentMax)] = i;
			totalDistanceTravelled [totalDistanceTravelled.IndexOf (currentMax)] = -1;
		}
		return positions.ToArray ();
	}

	private void terminaPartida(int vencedor)
    {
		//termina a partida
		for (int i = 0; i < maxPlayers; i++)
        {
			positionImages [i].enabled = false;//some a imagem de posição de cada jogador
			lapImages [i].enabled = false;//some a imagem de voltas de cada jogador
		}
		VictoryPanel.SetActive (true);//ativa o painel de vencedor da partida
	}

	private void updateImages()//faz a atualização das imagens correspondentes a posição de cada jogador
    {
		for (int i = 0; i < maxPlayers; i++)
        {
			positionImages [i].texture = Resources.Load((CurrentOrder [i]+1).ToString() + "colorido") as Texture;
			lapImages [i].texture = Resources.Load ("Laps\\LAPS" + (Laps [i] + 1).ToString () + "-3") as Texture;
		}
	}

	IEnumerator Countdown() //faz a contagem regressiva para o início da corrida
    {
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

		ccmSaci.isMovementAllowed = true;
		ccmCuca.isMovementAllowed = true;
		ccmCurupira.isMovementAllowed = true;
		ccmMula.isMovementAllowed = true;
	}

}
