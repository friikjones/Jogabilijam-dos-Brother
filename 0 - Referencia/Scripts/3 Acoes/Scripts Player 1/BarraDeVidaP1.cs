using UnityEngine;
using System.Collections;

public class BarraDeVidaP1 : MonoBehaviour {

    //private int stamina;
    private int health;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Player1ControllerLimitando otherScript = GameObject.Find("Player 1").GetComponent<Player1ControllerLimitando>();
        //stamina = otherScript.stamina;
        health = otherScript.health;


        /*for (int i = 0; i <= 9; i++)
        {
            this.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
        }*/
        if (health < 0) health = 0;
        for (int i = 9; i > (health-1); i--)
        {
            this.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }
        


    }
}
