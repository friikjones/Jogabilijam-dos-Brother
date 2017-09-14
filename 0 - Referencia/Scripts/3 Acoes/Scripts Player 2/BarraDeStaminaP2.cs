using UnityEngine;
using System.Collections;

public class BarraDeStaminaP2 : MonoBehaviour {

    private int stamina;
    //private int health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Player2ControllerLimitando otherScript = GameObject.Find("Player 2").GetComponent<Player2ControllerLimitando>();
        stamina = otherScript.stamina;
        //health = otherScript.health;
        

        for (int i = 0; i <= 5; i++)
        {
            this.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
        }
        if (stamina < 0) stamina = 0;
        for (int i = 5; i > (stamina-1); i--)
        {
            this.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }
        


    }
}
