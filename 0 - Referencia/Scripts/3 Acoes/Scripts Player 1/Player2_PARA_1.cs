using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player2_PARA_1 : MonoBehaviour {


	//Sprites
	public Sprite Idle;
	public Sprite Attck;
	public Sprite Cntr;
	public Sprite Mtt;
	public Sprite Evd;
	public Sprite Hit;


    //Animator Label;
    public bool labelPronto;
    public bool labelResolvido;
  
    //Dados Jogador
    public int acao;      //1 para ataque, 2 para defesa, 3 para esquiva, estado inicial = 0;
    public int pronto;    //autoexplicativo
    public int stamina;
    public int health;

    //Dados Oponente
    public int acaoOponente;
    public int prontoOponente;
    public int healthOponente;

    //Variáveis de Controle
    private bool indicaAtacou;
    private bool indicaRevidou;
    private bool indicaDesviou;
    private bool indicaMeditou;
    private bool indicaAcertou;
    private bool indicaApanhou;

    //Variáveis de comunicação
    private List<int> lista = new List<int>();
    private List<int> listaOponente = new List<int>();

    //Estado
    private string estado = "e1";
    

    // Use this for initialization
    void Start()
    {
        //Anim = GetComponent<Animator>();
        lista.Clear();
        listaOponente.Clear();
        lista.Add(0);
        lista.Add(0);
        lista.Add(0);
        listaOponente.Add(0);
        listaOponente.Add(0);
        listaOponente.Add(0);
    }

    void FixedUpdate()
    {
        switch (estado)
        {
            case "e1":  //Decidindo jogada
                ZeraControle();
                InputJogador();
                if (pronto == 1 && prontoOponente == 1)
                {
                    estado = "e2";
                    labelPronto = true;
                }
                break;
            case "e2":  //Resolvendo jogada
                ResolveCombate();
                if(VerificaVitoria())
                {
                    estado = "e3";
                }
                estado = "e1";
                break;
            case "e3":  //Fim de jogo
                break;
        }


    }    // Update is called once per frame

    void Update()
    {
        
        EscreveLista();
        
    }


    //------------------------------------------SUB-ROTINAS----------------------------------------------

    //Jogadas
    private void Ataque()
    {
        acao = 1;
    }
    private void Counter()
    {
        acao = 2;
    }
    private void Esquiva()
    {
        acao = 3;
    }
    private void Meditate()
    {
        acao = 4;
    }

    //Input
    private void InputJogador()
    {
		if (Input.GetKey(KeyCode.D))
        {
            Ataque();
        }

		if (Input.GetKey(KeyCode.S))
        {
            Counter();
        }

		if (Input.GetKey(KeyCode.A))
        {
            Esquiva();
        }

		if (Input.GetKey(KeyCode.W))
        {
            Meditate();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            EscutaLista();
            if (acao != 0)
            {
                pronto = 1;
            }
        }    

    }

   
    //Resolução
    private void ResolveCombate()
    {
        //--------------------------------------------<Mecanica Central desta Caralha>--------------------------------------------------------
        switch (acao)
        {
            case 1: //Ataque P1------------------------------------------------------------------
                stamina = stamina - 2;
                indicaAtacou = true;
                switch (acaoOponente)
                {
                    case 1: //Ataque P2
                        health = health - 3;
                        indicaApanhou = true;

				this.gameObject.GetComponent<SpriteRenderer> ().sprite = Attck;

                        break;
                    case 2: //Counter P2
                        health = health - 2;
                        indicaApanhou = true;

				this.gameObject.GetComponent<SpriteRenderer> ().sprite = Hit;

                        break;
                    case 3: //Esquiva P2
                        //*voz do faustão* ERRRRROU

				this.gameObject.GetComponent<SpriteRenderer> ().sprite = Attck;

                        break;
                    case 4: //Meditate P2
                        indicaAcertou = true;

				this.gameObject.GetComponent<SpriteRenderer> ().sprite = Attck;

                        break;
                }     
                break;
            case 2: //Counter P1------------------------------------------------------------------
                stamina = stamina - 3;
                indicaRevidou = true;

			this.gameObject.GetComponent<SpriteRenderer> ().sprite = Cntr;

                switch (acaoOponente)
                {
                    case 1: //Ataque P2
                        indicaAcertou = true;
                        break;
                    case 2: //Counter P2
                        //Nope
                        break;
                    case 3: //Esquiva P2
                        //Nope
                        break;
                    case 4: //Meditate P2
                        //Nope
                        break;
                }
                break;
            case 3: //Esquiva P1------------------------------------------------------------------
                stamina = stamina - 1;
                indicaDesviou = true; 

			this.gameObject.GetComponent<SpriteRenderer> ().sprite = Evd;

                switch (acaoOponente) //Vou deixar aqui apenas pra didática
                {
                    case 1: //Ataque P2
                        //Nope
                        break;
                    case 2: //Counter P2
                        //Nope
                        break;
                    case 3: //Esquiva P2
                        //Nope
                        break;
                    case 4: //Meditate P2
                        //Nope
                        break;
                }
                break;
            case 4: //Meditate P1------------------------------------------------------------------
                switch (acaoOponente)
                {
                    case 1: //Ataque P2
                        health = health - 3;
                        indicaApanhou = true;

				this.gameObject.GetComponent<SpriteRenderer> ().sprite = Hit;

                        break;
			case 2: //Counter P2
				stamina = stamina + 3;
				indicaMeditou = true;

				this.gameObject.GetComponent<SpriteRenderer> ().sprite = Mtt;

                        break;
                    case 3: //Esquiva P2
                        stamina = stamina + 3;
                        indicaMeditou = true;

				this.gameObject.GetComponent<SpriteRenderer> ().sprite = Mtt;

                        break;
                    case 4: //Meditate P2
                        stamina = stamina + 3;
                        indicaMeditou = true;

				this.gameObject.GetComponent<SpriteRenderer> ().sprite = Mtt;

                        break;
                }
                break;
        }   //----------------------------------------------</Mecanica Central>---------------------------------------------------------------

		labelResolvido = true;
		pronto = 0;

		//Tempo (); //Tentar criar uma funcao que espere um tempo antes de zerar.
		//A ideia eh dar tempo pros jogadores visualizarem o que aconteceu
		//Entao ter um contador de ROUNDS. Round 1 OK, Prepare for Round 2!

		//acao = 0;
        
        //Debug.Log("Resolvido");
                
    }

    //Fim de jogo?
    private bool VerificaVitoria()
    {
        //Ver se perdeu
        /*if (health <= 0)
        {
            return true;
        }*/
        //Ver se ganhou - FALTA
        return false;
    }
		
    //Recebe valores
    void EscutaLista()
    {
        Player2ControllerGenerico otherScript = GameObject.Find("Player 2").GetComponent<Player2ControllerGenerico>();
        listaOponente = otherScript.RetornaStatus();

        acaoOponente = listaOponente[0];
        prontoOponente = listaOponente[1];
        healthOponente = listaOponente[2];
    }

    //Escreve lista para envio
    void EscreveLista()
    {
        lista[0] = acao;
        lista[1] = pronto;
        lista[2] = health;
    }

    //Envia lista
    public List<int> RetornaStatus()
    {
        return lista;
    }

    //Zera Variáveis de Controle
    private void ZeraControle ()
	{
		indicaAtacou = false;
		indicaRevidou = false;
		indicaDesviou = false;
		indicaMeditou = false;
		indicaAcertou = false;
		indicaApanhou = false;

		//labelResolvido = false;
		//labelPronto = false;
        
	}
}

	//Contador de Rounds

