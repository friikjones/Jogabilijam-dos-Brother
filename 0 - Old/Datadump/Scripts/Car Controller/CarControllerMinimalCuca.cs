using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;      /////NECESSARIO PARA EXECUTAR COMPARACAO ENTRE STRINGS

public class CarControllerMinimalCuca : MonoBehaviour
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
    public float skillTime = 5f;
    private GameObject script;
    public Transform naoBate;
    


    /////PARAMETROS PARA INVERTER OS CONTROLES (SKILL DO SACI)
    public bool inverteControle;
    int i = 1;
    GameObject spark;

    /////PARAMETROS PARA EXECUTAR HABILIDADE TROLADA
    string hue;
    string newHue;
    string compara = "Cuca";
    int resultado;
    private string troll;
    public bool trolado = false;
    public float smooth = 1f;
    private GameObject explosaoTroll;
    private Vector3 targetAngles;


    void Start()
    {
        /////CAPTURANDO OBJETOS PARA SKILL, FEEDBACK SKILL DO SACI E SKILL TROLADA
        script = GameObject.Find("Explosion");
        script.SetActive(false);

        spark = GameObject.FindGameObjectWithTag("SparkCuca");
        spark.SetActive(false);

        explosaoTroll = GameObject.Find("ExplosionTroll");
        explosaoTroll.SetActive(false);
        
        pu = false;
        trolado = false;

        


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

        /////INVERSOR DE CONTROLES
        inverteControle = Traquinagem.inverte; 
        
        if (inverteControle == false)
        {
            i = 1;
            spark.SetActive(false);
        }


    }


    /////CONTROLE DO CARRO
    void MovementControl()
    {

        /////ACELERACAO E RE
        if (Input.GetKey(KeyCode.Home))
        {
            rb.AddRelativeForce(v3Front * accellScale * boost * i);
        }

        if (Input.GetKey(KeyCode.End))
        {
            rb.AddRelativeForce(v3Back * reverseScale * i);
        }


        /////STEER DIREITA ESQUERDA
        if (Input.GetKey(KeyCode.Delete))
        {
            transform.Rotate(v3Left * Time.deltaTime * rotateScale * i);
        }

        if (Input.GetKey(KeyCode.PageDown))
        {
            transform.Rotate(v3Right * Time.deltaTime * rotateScale * i);
        }

    }


    /////SKILL CUCA - URUCUBACA - INVOCA NUVEM MAGICA QUE ATRAI VEICULOS PROXIMOS
    void Skill() {


        HuehueBr();
        

        if (Input.GetKey(KeyCode.KeypadPlus) && pu == true)
        {
            ///// SKILL TROLADA CUCA - CHUTA QUE É MACUMBA - GIRA A CUCA EM 180º
            if (trolado == true)
            {
                targetAngles = transform.eulerAngles + 180f * Vector3.up;
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime);
                explosaoTroll.SetActive(true);

                trolado = false;

                puStack = 0;
                pu = false;
                StartCoroutine(ResetSkill());

            }
            else
            {
                /////SKILL URUCUBACA
                trolado = false;
                puStack = 0;
                pu = false;

                script.SetActive(true);

                


                StartCoroutine(ResetSkill());
            }


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

    
    /////LOGICA PARA COLISORES "DENTRO"
    void OnTriggerStay(Collider collider)
    {
        /////TRIGGER INVERSAO DE CONTROLES
        if (collider.gameObject.tag == "Inversor" && inverteControle == true)
        {

            i = -1;
            spark.SetActive(true);
        }
    }


    /////TEMPO DE SKILL E RESETS
    IEnumerator ResetSkill()
    {

        yield return new WaitForSeconds(skillTime);
        script.SetActive(false);
        explosaoTroll.SetActive(false);

    }


    /////DEVOLDE SE VAI SER TROLADO OU NAO PELO SACI (ALEATORIAMENTE)
    void HuehueBr()
    {

        GameObject update = GameObject.Find("Saci");

        if (update.GetComponent<Traquinagem>().enabled == true)
        {

            troll = Traquinagem.nome;
            hue = troll;

        }
        else
        {
            troll = null;
        }


        switch (hue)
        {
            case "Curupira (UnityEngine.BoxCollider)":
                {
                    newHue = "Curupira";

                }
                break;
            case "Mula (UnityEngine.BoxCollider)":
                {
                    newHue = "Mulaa";

                }
                break;
            case "Cuca (UnityEngine.BoxCollider)":
                {
                    newHue = "Cuca";

                }
                break;

            default:
                break;
        }


        resultado = string.Compare(compara, newHue, true);


        if (resultado == 0)
        {
            trolado = true;
            hue = null;
            newHue = null;
        }



    }

}