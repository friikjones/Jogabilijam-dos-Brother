using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RouteScript : MonoBehaviour {

	//Variáveis do XML
	public const string path = "items";

	//Variáveis do item
	public int Id;
	public float X;
	public float Y;
	public float Alpha;
	public int Cost;
	public int Income;
	public int N1;
	public int N2;
	public int N3;
	public int N4;
	public int N5;
	public int Own;

	//Variáveis dos Childs
	private string texto;
	private int index;
	private GameObject Parent;

	//Variáveis de UI
	public bool comprando;
	public int comprado;
	
	public Sprite Normal;
	public Sprite AzulClaro;
	public Sprite AzulEscuro;
	public Sprite VermelhoClaro;
	public Sprite VermelhoEscuro;
	
	
	//Variáveis de Balanço
	public int multiplierCost;

	// Use this for initialization
	void Start () {
		
		multiplierCost=2;


		Parent = GameObject.FindGameObjectWithTag ("Board");
		this.transform.parent = Parent.transform;

		ItemContainer ic = ItemContainer.Load(path);
		index = transform.GetSiblingIndex();
		foreach (Item item in ic.items)
		{
			if(item.Id==index)
			{
				Id = item.Id;
				X=item.X;
				Y=item.Y;
				Alpha=item.Alpha;
				Cost=item.Cost*multiplierCost;
				Income=item.Income;
				N1 = item.N1;
				N2 = item.N2;
				N3 = item.N3;
				N4 = item.N4;
				N5 = item.N5;
				Own = item.Own;

			}
		}


		if (Id == 31) 
			comprado = 1;
		if (Id == 32)
			comprado = 2;

		transform.localScale = new Vector3 (1,1,1);
		transform.localPosition = new Vector3 (X,Y,0);
		transform.localRotation = Quaternion.AngleAxis (Alpha, new Vector3 (0, 0, 1));
		this.gameObject.transform.GetChild(0).localRotation = Quaternion.AngleAxis (Alpha, new Vector3 (0, 0, -1));
		this.gameObject.transform.GetChild(1).localRotation = Quaternion.AngleAxis (Alpha, new Vector3 (0, 0, -1));
		this.transform.Find("Custo").GetComponent<Text>().text = Cost.ToString();
		this.transform.Find("Income").GetComponent<Text>().text = "+"+Income.ToString();

		//this.GetComponentInChildren<Text>().text = "+"+Income.ToString();
	}
	
	// Update is called once per frame
	void Update () {

		GameManagerScript ManagerScript = GameObject.Find ("GameManager").GetComponent<GameManagerScript> ();

		//Controle de Cor - Comprando
		if (comprando == true && ManagerScript.gameState == 1)
			//GetComponent<Image> ().color = new Color32(0x81, 0x97, 0xFF, 0xFF) ;
			GetComponent<Image>().sprite = AzulClaro;
		if (comprando == true && ManagerScript.gameState==2)
			//GetComponent<Image> ().color = new Color32(0xFF, 0x81, 0x83, 0xFF) ;
			GetComponent<Image>().sprite = VermelhoClaro;
		if (comprando == false)
			//GetComponent<Image> ().color = Color.white;
			GetComponent<Image>().sprite = Normal;

		//Controle de Cor - Comprado
		if (comprado == 1)
			//GetComponent<Image> ().color = new Color32(0x32, 0x55, 0xFF, 0xFF) ;
			GetComponent<Image>().sprite = AzulEscuro;
		if (comprado == 2)
			//GetComponent<Image> ().color = new Color32(0xFF, 0x38, 0x3C, 0xFF) ;
			GetComponent<Image>().sprite = VermelhoEscuro;


	}

	public void Comprando() {


		//if (comprado != 0) 
		//{
			Canvas Board = GameObject.Find ("Board").GetComponent<Canvas> ();

			int children = Board.transform.childCount;
			for (int i = 0; i < children; i++) {
				Board.transform.GetChild (i).GetComponent<RouteScript> ().comprando = false;
			}
		if (VerificaVizinhos ())
			comprando = true;
		else
			comprando = false;
		//}
	}

	public bool VerificaVizinhos(){

		//bool vizinhosOk;
		Debug.Log("Id "+Id.ToString()+" ");
		Canvas Board = GameObject.Find ("Board").GetComponent<Canvas> ();
		GameManagerScript ManagerScript = GameObject.Find ("GameManager").GetComponent<GameManagerScript> ();

		if (Board.transform.GetChild (N1).GetComponent<RouteScript> ().comprado == ManagerScript.gameState ||
			Board.transform.GetChild (N2).GetComponent<RouteScript> ().comprado == ManagerScript.gameState ||
			Board.transform.GetChild (N3).GetComponent<RouteScript> ().comprado == ManagerScript.gameState ||
			Board.transform.GetChild (N4).GetComponent<RouteScript> ().comprado == ManagerScript.gameState ||
			Board.transform.GetChild (N5).GetComponent<RouteScript> ().comprado == ManagerScript.gameState) 
		return true;


		Debug.Log ("Falso");
		return false;
			
	}


}
