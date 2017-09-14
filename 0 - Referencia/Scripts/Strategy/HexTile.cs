using UnityEngine;
using System.Collections;

public class HexTile : MonoBehaviour {
	
	public int tileId;
	public bool selected;
	
	private GameObject tileManager;
	private GameObject player1;
	private GameObject player2;
	
	// Use this for initialization
	void Start () {
	
	tileManager = GameObject.Find("GameMat");
	Debug.Log ("found " + tileManager.name);
	player1 = GameObject.Find("TanqueBase");
	Debug.Log ("found " + player1.name);
	player2 = GameObject.Find("TanqueEnemy");
	Debug.Log ("found " + player2.name);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown ()
	{
		if (player1.GetComponent<TankBehaviour>().selected || player2.GetComponent<TankBehaviour>().selected)
		selected = true;
	}
	
}
