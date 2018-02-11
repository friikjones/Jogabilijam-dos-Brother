using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectDTOScript : MonoBehaviour {

	private static int index;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setIndex(int i){
		index = i;
	}

	public int getIndex(){
		return index;
	}
}
