using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerMinimalCuca : MonoBehaviour
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
	public string turnRButton = "TurnR_P1";
	public string turnLButton = "TurnL_P1";
	public string skillButton = "Skill_P1";

	private float horizontalValue;
	private float verticalValue;
	public bool isMovementAllowed;

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
    


    // Use this for initialization
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

		/////INVERSOR DE CONTROLES
		inverteControle = Traquinagem.inverte; 

		if (inverteControle == false)
		{
			i = 1;
			spark.SetActive(false);
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

	/////SKILL CUCA - URUCUBACA - INVOCA NUVEM MAGICA QUE ATRAI VEICULOS PROXIMOS
	void Skill() {


		HuehueBr();


		if (Input.GetButtonDown(skillButton) && pu == true)
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



