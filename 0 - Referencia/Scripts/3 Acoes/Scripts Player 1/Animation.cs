using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Animation : MonoBehaviour {

    //Player1
    public GameObject Player1;
	public Player1ControllerLimitando Script;

    //Animação
    Animator Anim;

    //Variáveis
    public int acao;
    public bool pronto;
    public bool resolvido;
    

    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        
        
    }    // Update is called once per frame

    void Update()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player1");
		Script = Player1.GetComponent<Player1ControllerLimitando>();

        acao = Script.acao;
        pronto = Script.labelPronto;
        resolvido = Script.labelResolvido;
        

        Anim.SetInteger("Acao", acao);
        Anim.SetBool("Pronto", pronto);
        Anim.SetBool("Resolvido", resolvido);

    }


    //------------------------------------------SUB-ROTINAS----------------------------------------------
    
}