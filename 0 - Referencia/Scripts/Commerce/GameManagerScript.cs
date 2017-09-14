using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

	//Variáveis de UI
	public Button nextTurn;
	public Text textPlayer1;
	public Text textPlayer2;
	public Text playerMoneyUI;
	public Text playerIncomeUI;

	//Variáveis de PopUp
	public Text nextMoneyUI;
	public Text nextIncomeUI;

    //Variáveis do GameManager
	public int gameState = 0;
	private bool nextTurnPressed = false;
	public GameObject Route;
	public int vencedor;

	//Variáveis dos Jogadores
	public int playerMoney1;
	public int playerIncome1;
	public int playerMoney2;
	public int playerIncome2;
	public int nextMoney;
	public int nextIncome;


	//Variáveis das Trilhas
	public int routeId;
	public bool routeBuying;
	public int routeCost;
	public int routeIncome;
	
	
	//Variáveis de Balanço
	
	
	void Start () {

        //Variáveis de UI
		textPlayer1.enabled = false;
		textPlayer2.enabled = false;
		nextMoneyUI.enabled = false;
		nextIncomeUI.enabled = false;

		//Variáveis do GameManager
		gameState = 0;

		Canvas Board = GameObject.Find("Board").GetComponent<Canvas>();
		

	}
	
	// Update is called once per frame
	void Update () {

		if (routeBuying == false) 
		{
			nextMoneyUI.enabled = false;
			nextIncomeUI.enabled = false;
		}

		switch (gameState) 
		{

			case 0:  //Inicio
			{
				//textPlayer1.enabled = false;
				//textPlayer2.enabled = false;
				//ShowStartMenu();
				playerMoney1 = 10;
				playerIncome1 = 2;
				playerMoney2 = 8;
				playerIncome2 = 2;

				if (true) 
				{
					
					gameState = 1;
				}

			}break;

			case 1: //Vez do Jogador 1
			{
				
				textPlayer1.enabled = true;
				textPlayer2.enabled = false;
				playerMoneyUI.text = playerMoney1.ToString();
				playerIncomeUI.text = "+"+playerIncome1.ToString();

				ButtonListen();
				if (nextTurnPressed) 
				{
					gameState = 2;
					nextTurnPressed = false;
					ButtonClean();
					UpdatePlayers();
					VerificaFim ();
				}

			}break;

			case 2: //Vez do Jogador 2
			{
				
				textPlayer1.enabled = false;
				textPlayer2.enabled = true;
				playerMoneyUI.text = playerMoney2.ToString();
				playerIncomeUI.text = "+"+playerIncome2.ToString();

				ButtonListen();
				if (nextTurnPressed) 
				{
					gameState = 1;
					nextTurnPressed = false;
					ButtonClean();
					UpdatePlayers();
					VerificaFim ();
				}

			}break;

			case 3: //Fim de jogo
			{
				//Debug.Log (lastPlayer);
				textPlayer1.enabled = false;
				textPlayer2.enabled = false;
				if(vencedor==1)
				{
					SceneManager.LoadScene (4, LoadSceneMode.Additive);
				}
				else
				{
					SceneManager.LoadScene (5, LoadSceneMode.Additive);
				}
			}break;

		}
			
    }


	// Rotinas de UI
    public void NextTurnPress()
    {
        //Debug.Log("Ate aki");
		nextTurnPressed = true;

        

    }

	// Rotinas do PopUp
	public void Comprar()
	{
		Canvas Board = GameObject.Find("Board").GetComponent<Canvas>();
		//Board.transform.GetChild (routeId).GetComponent<RouteScript>().comprado = true;

		if (gameState == 1) 
		{
			if ((playerMoney1 - nextMoney) >= 0) 
			{
				playerMoney1 = playerMoney1 - nextMoney;
				playerIncome1 = playerIncome1 + nextIncome;
				Board.transform.GetChild (routeId).GetComponent<RouteScript>().comprado = 1;
			}
		}

		if (gameState == 2) 
		{
			if ((playerMoney2 - nextMoney) >= 0) 
			{
				playerMoney2 = playerMoney2 - nextMoney;
				playerIncome2 = playerIncome2 + nextIncome;
				Board.transform.GetChild (routeId).GetComponent<RouteScript>().comprado = 2;
			}
		}

		nextMoneyUI.enabled = false;
		nextIncomeUI.enabled = false;
		Board.transform.GetChild (routeId).GetComponent<RouteScript>().comprando = false;
	}


	//Rotinas Trilhas
	public void ButtonListen()
	{
		Canvas Board = GameObject.Find("Board").GetComponent<Canvas>();

		int children = Board.transform.childCount;
		for (int i = 0; i < children; i++) 
		{
			//Debug.Log ("ate aki");
			if (Board.transform.GetChild (i).GetComponent<RouteScript> ().comprando == true) {
				//Debug.Log (i);
				routeBuying = true;
				routeId = Board.transform.GetChild (i).GetComponent<RouteScript> ().Id;
				routeCost = Board.transform.GetChild (i).GetComponent<RouteScript> ().Cost;
				routeIncome = Board.transform.GetChild (i).GetComponent<RouteScript> ().Income;

				//UI

				nextMoneyUI.enabled = true;
				nextIncomeUI.enabled = true;
				nextMoney = routeCost;
				nextIncome = routeIncome;

				if (gameState == 1) {
					nextMoneyUI.text = (playerMoney1 - nextMoney).ToString ();
					nextIncomeUI.text = "+" + (playerIncome1 + nextIncome).ToString ();
				}

				if (gameState == 2) {
					nextMoneyUI.text = (playerMoney2 - nextMoney).ToString ();
					nextIncomeUI.text = "+" + (playerIncome2 + nextIncome).ToString ();
				}


			} else {
				routeBuying = false;
			}
		}

	}

	public void ButtonClean()
	{
		Canvas Board = GameObject.Find ("Board").GetComponent<Canvas> ();

		int children = Board.transform.childCount;
		for (int i = 0; i < children; i++) 
		{
			Board.transform.GetChild (i).GetComponent<RouteScript> ().comprando = false;

		}
	}

	public void UpdatePlayers()
	{
		if (gameState == 1) 
		{
			playerMoney1 = playerMoney1 + playerIncome1;
		}
		if (gameState == 2) 
		{
			playerMoney2 = playerMoney2 + playerIncome2;
		}
	}

	public void VerificaFim()
	{
		bool teste = false;
		bool testeFinal = false;

		Canvas Board = GameObject.Find ("Board").GetComponent<Canvas> ();

		int children = Board.transform.childCount;
		for (int i = 0; i < children; i++) 
		{
			if ((Board.transform.GetChild (i).GetComponent<RouteScript> ().VerificaVizinhos () == true) && (Board.transform.GetChild (i).GetComponent<RouteScript> ().comprado == 0)) 
			{
				teste = true;
				//Debug.Log ("True " + i.ToString ());
			}
				

		}
		if (teste == false)
			testeFinal = true;


		if (testeFinal == true) {
			//Debug.Log ("Fim");
			vencedor = RetornaVencedor();
			gameState = 3;
		
		}
						
	}

	public void ForcaFim()
	{
		gameState = 3;
	}
	
	public int RetornaVencedor()
	{
		
		int contadorPlayer1 = 0;
		int contadorPlayer2 = 0;
		
		Canvas Board = GameObject.Find ("Board").GetComponent<Canvas> ();

		int children = Board.transform.childCount;
		for (int i = 0; i < children; i++) 
		{
			if (Board.transform.GetChild (i).GetComponent<RouteScript> ().comprado == 1) 
			{
				contadorPlayer1++;
			}
			if (Board.transform.GetChild (i).GetComponent<RouteScript> ().comprado == 2) 
			{
				contadorPlayer2++;
			}
			
		}
		
		if (contadorPlayer1 > contadorPlayer2)
		{
			
			return 1;
			
		}else{
			
			if (contadorPlayer1 < contadorPlayer2)
			{
				return 2;
				
			}else{
				
				return 0;
			}
			
		}
		
		
	}
	
}
