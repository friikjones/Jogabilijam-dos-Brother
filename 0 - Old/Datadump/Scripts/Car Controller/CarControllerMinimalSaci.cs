using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerMinimalSaci : MonoBehaviour
{

    /////VETORES PARA O CARRO
    private Rigidbody rb;
    private Vector3 v3Front;
    private Vector3 v3Back;
    private Vector3 v3Left;
    private Vector3 v3Right;

    /////PARAMETROS INDIVIDUAIS
    public int accellScale;
    public int reverseScale;
    public int rotateScale;
    public float boost = 1;


    /////PARAMETROS PARA HABILIDADES
    public bool pu;
    public int puStack = 0;
    public float skillTime = 2.2f;
    private GameObject go;
    public GameObject tornado;
    public string troll;


    void Start()
    {
        /////CAPTURANDO OBJETOS PARA SKILL E EFEITO DA SKILL
        pu = false;

        go = GameObject.Find("Saci");
        go.GetComponent<Traquinagem>().enabled = false;

        tornado = GameObject.FindGameObjectWithTag("Tornado");
        tornado.SetActive(false);


        rb = GetComponent<Rigidbody>();
        v3Front = new Vector3(0, 0, 1);
        v3Back = new Vector3(0, 0, -1);
        v3Left = new Vector3(0, -1, 0);
        v3Right = new Vector3(0, 1, 0);
    }


    void Update()
    {
        MovementControl();
        Skill();

    }


    /////CONTROLA O CARRO
    void MovementControl()
    {
        
        /////ACELERACAO E RE
        if (Input.GetKey(KeyCode.I))
        {
            rb.AddRelativeForce(v3Front * accellScale * boost);
        }

        if (Input.GetKey(KeyCode.K))
        {
            rb.AddRelativeForce(v3Back * reverseScale);
        }


        /////STEER DIREITA ESQUERDA
        if (Input.GetKey(KeyCode.J))
        {
            transform.Rotate(v3Left * Time.deltaTime * rotateScale);
        }

        if (Input.GetKey(KeyCode.L))
        {
            transform.Rotate(v3Right * Time.deltaTime * rotateScale);
        }

    }


    /////SKILL SACI - TORNADO DA ZOEIRA - O SACI INVERTE OS CONTROLES DE JOGADORES NA AREA DE ACAO POR skillTime SEGUNDOS E CAUSA UM EFEITO BONUS DA SKILL:
    /////DENTRO DA AREA DO FURACAO, SERA SELECIONADO ALEATORIAMENTE 1 JOGADOR E ELE TERA SUA SKILL TROLADA, ATE O PROXIMO USO...(risadinhas)
    void Skill()
    {
        if (Input.GetKey(KeyCode.RightControl) && pu == true)
        {
            pu = false;
            puStack = 0;
            go.GetComponent<Traquinagem>().enabled = true;

            Traquinagem traq = gameObject.GetComponent<Traquinagem>();
            troll = traq.CapturaTroll();
            
            tornado.SetActive(true);


            StartCoroutine(ResetSkill());

        }
    }


    /////LOGICA PARA COLISORES ENTER
    void OnTriggerEnter(Collider collider)
    {

        /////TRIGGER POWER UP PAD
        if (collider.gameObject.tag == "PowerUp")
        {
            pu = true;
            puStack++;
        }
    }



    /////TEMPO DE SKILL E RESETS
    IEnumerator ResetSkill()
    {

        yield return new WaitForSeconds(skillTime);
        go.GetComponent<Traquinagem>().enabled = false;
        tornado.SetActive(false);



    }

}