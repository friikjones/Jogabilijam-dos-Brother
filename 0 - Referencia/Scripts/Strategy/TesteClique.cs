using UnityEngine;
using System.Collections;

public class TesteClique : MonoBehaviour {

	public Material CuboAzul;
	public Material CuboVermelho;
	
	public Renderer TesteRend;
	
	private int i;
	
	// Use this for initialization
	void Start () 
	{
		TesteRend = GetComponent<Renderer>();
		TesteRend.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown()
	{
		i++;
		if (i%2 == 0)
		{
			TesteRend.material = CuboAzul;
		}else{
			TesteRend.material = CuboVermelho;
		}
		Debug.Log("Clique");
	}
}
