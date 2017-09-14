using UnityEngine;
using System.Collections;

public class TankBehaviour : MonoBehaviour {

	public bool selected;
	private int tileTarget;
	
	public int faction;
	
	public Material unselectedMaterial;
	public Material selectedMaterial;
	private Renderer rend;
	
	private int i;
	private Vector3 initialPosition;
	
	private GameObject tileManager;
	
	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		tileManager = GameObject.Find("GameMat");
		if (faction == 1)
		initialPosition = new Vector3(-1, 0.6f, -1);
		if (faction == 2)
		initialPosition = new Vector3(1, 0.6f, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
		SelectCheck();
		if (selected)
		{
			FindTarget();
			if(tileTarget != 1000)
			{
				MovetoTarget();
			}
		}
	}
	
	void OnMouseDown()
	{
		i++;
		if (i%2 == 0)
		{
			//rend.material = unselectedMaterial;
			selected = false;
		}else{
			//rend.material = selectedMaterial;
			selected = true;
		}
		//Debug.Log("Clique");
	}

	void SelectCheck()
	{
		if (selected)
		{
			rend.material = selectedMaterial;
			
		}else{
			rend.material = unselectedMaterial;
		}
	}
	
	void FindTarget()
	{
		tileTarget = tileManager.GetComponent<TileManagerScript>().TileCheck();
		//Debug.Log("target " + tileTarget);
		tileManager.GetComponent<TileManagerScript>().TileClean();
			
	}
	
	
	void MovetoTarget()
	{
		transform.position = tileManager.GetComponent<TileManagerScript>().TilePosition(tileTarget);
		transform.position = transform.position + initialPosition;
		selected = false;
		Debug.Log("alvo "+tileTarget);
		
	}
}

