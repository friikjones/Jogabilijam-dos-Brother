using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using UnityEngine.SceneManagement;


public class TwoPlayerStdEnd : MonoBehaviour {

	public int player1Money;
	public int player2Money;
	public int player1Income;
	public int player2Income;



	// Use this for initialization
	void Start () {

		GameManagerScript ManagerScript = GameObject.Find ("GameManager").GetComponent<GameManagerScript> ();
		player1Money = ManagerScript.playerMoney1;
		player2Money = ManagerScript.playerMoney2;
		player1Income = ManagerScript.playerIncome1;
		player2Income = ManagerScript.playerIncome2;

		SceneManager.UnloadScene (1);
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}
}
