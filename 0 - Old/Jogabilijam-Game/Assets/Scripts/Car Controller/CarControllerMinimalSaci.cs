using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerMinimalSaci : MonoBehaviour
{

    private CharacterController controller;

    private Rigidbody rb;
    private Vector3 v3Front;
    private Vector3 v3Back;
    private Vector3 v3Left;
    private Vector3 v3Right;
    private Vector3 v3Up;
	private Vector3 v3Horizontal;

    public int accellScale;
    public int reverseScale;
    public int rotateScale;
    public int boost = 1;
    private int driftR, driftL;
    public float gravidade=14f;
	public string accelAxis = "Accel_P1";
	public string turnAxis = "Horizontal_P1";
	public string turnRAxis = "TurnR_P1";
	public string turnLAxis = "TurnL_P1";
	public string skillButton = "Skill_P1";

	private float horizontalValue;
	private float verticalValue;
	public bool isMovementAllowed;

	/////PARAMETROS PARA HABILIDADES
	public bool pu;
	public int puStack = 0;
	public float skillTime = 2.2f;
	private GameObject go;
	public GameObject tornado;
	public string troll;


    


    // Use this for initialization
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
        v3Up = new Vector3(0, 1, 0);
		v3Horizontal = new Vector3(0, 1, 0);

		isMovementAllowed = false;
        
    }

    // Update is called once per frame
    void Update()
    {
		if (isMovementAllowed) {
			MovementControl ();
			Skill ();
		}
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravidade * rb.mass);

    }


        void MovementControl()
    {



		verticalValue = Input.GetAxis (accelAxis);

		if(verticalValue>0)
			rb.AddRelativeForce(v3Front * accellScale * boost);
		if (verticalValue<0)
			rb.AddRelativeForce(v3Back * reverseScale);

		horizontalValue = Input.GetAxis (turnAxis);

		if (horizontalValue < -0.5f)
        {

            transform.Rotate(v3Left * Time.deltaTime * rotateScale);
            driftL = driftL + 1;

            if (driftL > 22)
            {
                transform.Rotate(v3Left * Time.deltaTime * (rotateScale - 110));


            }
            if (driftL > 35)
            {
                transform.Rotate(v3Left * Time.deltaTime * (rotateScale - 120));
            }

        }
        else
        {
            driftL = 0;
        }
			

    


		if (horizontalValue > 0.5f)
        {
            transform.Rotate(v3Right * Time.deltaTime * rotateScale);

            driftR = driftR + 1;

            if (driftR > 22)
            {
                transform.Rotate(v3Right * Time.deltaTime * (rotateScale - 110));


            }
            if (driftR > 35)
            {
                transform.Rotate(v3Right * Time.deltaTime * (rotateScale - 120));
            }

        }
        else
        {
            driftR = 0;
        }



    
    }

	/////SKILL SACI - TORNADO DA ZOEIRA - O SACI INVERTE OS CONTROLES DE JOGADORES NA AREA DE ACAO POR skillTime SEGUNDOS E CAUSA UM EFEITO BONUS DA SKILL:
	/////DENTRO DA AREA DO FURACAO, SERA SELECIONADO ALEATORIAMENTE 1 JOGADOR E ELE TERA SUA SKILL TROLADA, ATE O PROXIMO USO...(risadinhas)
	void Skill()
	{
		if (Input.GetButtonDown(skillButton) && pu == true)
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



    ///// trigger do Boost /////
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



