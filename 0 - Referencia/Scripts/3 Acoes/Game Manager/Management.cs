using UnityEngine;
using System.Collections;

public class Management : MonoBehaviour {


	public int healthP1;
    public int healthP2;
    public int staminaP1;
    public int staminaP2;

    public int vitorioso;

    public bool FimDeJogo;


	void Start () {

        //Inicio do jogo
        Player2ControllerLimitando P2Script = GameObject.Find("Player 2").GetComponent<Player2ControllerLimitando>();
        staminaP2 = P2Script.stamina = 6;
        healthP2 = P2Script.health = 10;

        Player1ControllerLimitando P1Script = GameObject.Find("Player 1").GetComponent<Player1ControllerLimitando>();
        staminaP1 = P1Script.stamina = 6;
        healthP1 = P1Script.health = 10;

        FimDeJogo = false;
        vitorioso = 0;

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        Player2ControllerLimitando P2Script = GameObject.Find("Player 2").GetComponent<Player2ControllerLimitando>();
        staminaP2 = P2Script.stamina;
        healthP2 = P2Script.health;

        Player1ControllerLimitando P1Script = GameObject.Find("Player 1").GetComponent<Player1ControllerLimitando>();
        staminaP1 = P1Script.stamina;
        healthP1 = P1Script.health;

        //Limitando a Vida e Stamina P1 e P2
        if (P2Script.health > 10)
            P2Script.health = 10;
        if (P2Script.stamina > 6)
            P2Script.stamina = 6;
        if (P2Script.stamina < 0)
            P2Script.stamina = 0;

        if (P1Script.health > 10)
            P1Script.health = 10;
        if (P1Script.stamina > 6)
            P1Script.stamina = 6;
        if (P1Script.stamina < 0)
            P1Script.stamina = 0;



        if (healthP1 > 0 && healthP2 <= 0)
        {
            VitoriaP1();
            FimDeJogo = true;
            Debug.Log("Vitoria P1");
            vitorioso = 1;
        }
        

        if (healthP1 <= 0 && healthP2 > 0)
        {
            VitoriaP2();
            FimDeJogo = true;
            Debug.Log("Vitoria P2");
            vitorioso = 2;
        }
        

        if (healthP1 <= 0 && healthP2 <= 0)
        {
            Draw();
            FimDeJogo = true;
            Debug.Log("Draw");
            vitorioso = 3;
        }
        


    }




    private void VitoriaP1()
    {

    }

    private void VitoriaP2()
    {

    }

    private void Draw()
    {

    }
}




