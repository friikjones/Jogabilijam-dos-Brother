using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerMinimalCurupira : MonoBehaviour
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
	public string accelAxis = "Accel_P2";
	public string turnAxis = "Horizontal_P2";
	public string turnRButton = "TurnR_P1";
	public string turnLButton = "TurnL_P1";
	public string skillButton = "Skill_P2";

	private float horizontalValue;
	private float verticalValue;
	public bool isMovementAllowed;

	/////PARAMETROS PARA HABILIDADES

	public bool pu;
	public int puStack = 0;
	public float skillTime = 2.2f;
	private GameObject script;

	/////PARAMETROS PARA INVERTER OS CONTROLES (SKILL DO SACI)
	public bool inverteControle;
	int i = 1;
	GameObject spark;


	/////PARAMETROS PARA EXECUTAR HABILIDADE TROLADA
	string hue;
	string newHue;
	string compara = "Curupira";
	int resultado;
	private string troll;
	public bool trolado = false;

	public GameObject arvoreTroll;
	public GameObject spriteTroll;
	Rigidbody rbTroll;

    


    // Use this for initialization
    void Start()
    {
		/////CAPTURANDO OBJETOS PARA SKILL, FEEDBACK DA SKILL DO SACI E SKILL TROLADA
		script = GameObject.Find("Arveres");

		spark = GameObject.FindGameObjectWithTag("SparkCurupira");
		spark.SetActive(false);


		spriteTroll.SetActive(true);
		rbTroll = GameObject.Find("Curupira").GetComponent<Rigidbody>();
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
		inverteControle = Traquinagem.inverte;

		/////INVERSOR DE CONTROLES
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


		verticalValue = 0;
		verticalValue = Input.GetAxis(accelAxis);
		Debug.Log ("VerticalValue = " + verticalValue);

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

	/////SKILL CURUPIRA - AS ARVERES... - COM O PODER DA NATUREZA CRIA UMA FLORESTA 
	/////EM POSIÇÕES RANDOMICAS NO CIRCUITO POR slillTime SEGUNDOS
	void Skill()
	{
		HuehueBr();


		if (Input.GetButtonDown(skillButton) && pu == true)
		{

			/////SKILL TROLADA CURUPIRA - ...SOMOS NOSES - OS ESPIRITOS DA FLORESTA SE ENFURECERAM
			/////E O CURUPIRA VIRA UMA ARVORE E FICA IMOVEL POR skillTime SEGUNDOS
			if (trolado == true)
			{
				spriteTroll.SetActive(false);
				arvoreTroll.SetActive(true);
				rbTroll.isKinematic = true;

				trolado = false;

				puStack = 0;
				pu = false;


				StartCoroutine(ResetSkill());

			}
			else
			{
				/////SKILL AS ARVERES...
				SceneFill arveres = (SceneFill)script.GetComponent(typeof(SceneFill));
				arveres.FillScene();

				trolado = false;


				pu = false;
				puStack = 0;



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
		script.GetComponent<SceneFill>().enabled = false;

		spriteTroll.SetActive(true);
		arvoreTroll.SetActive(false);
		rbTroll.isKinematic = false;



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



