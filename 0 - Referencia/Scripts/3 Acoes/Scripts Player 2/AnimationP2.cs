using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationP2 : MonoBehaviour {

    //Player1
    public GameObject Player2;
    public Player2ControllerLimitando Script;

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
        Player2 = GameObject.FindGameObjectWithTag("Player2");
        Script = Player2.GetComponent<Player2ControllerLimitando>();

        acao = Script.acao;
        pronto = Script.labelPronto;
        resolvido = Script.labelResolvido;

        Anim.SetInteger("Acao", acao);
        Anim.SetBool("Pronto", pronto);
        Anim.SetBool("Resolvido", resolvido);

    }


    //------------------------------------------SUB-ROTINAS----------------------------------------------
    
}