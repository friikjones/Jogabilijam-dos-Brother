using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour {


    Material mat;
	// Use this for initialization
	void Start () {

        gameObject.layer = 9;
        mat = GetComponent<MeshRenderer>().material;


	}
	public void Interact()
    {
        mat.color = Color.green;
    }
	
}
