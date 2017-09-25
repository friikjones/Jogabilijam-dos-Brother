using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharSelectInfo : MonoBehaviour {

	//private static int index;
	private Canvas canvas;
	private CharSelectDTOScript DTO;

	// Use this for initialization
	void Start () {

		canvas = GameObject.Find("EmptyThingy").GetComponentInChildren<Canvas>();
		DTO = GameObject.Find("CharSelectDTO").GetComponent<CharSelectDTOScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ButtonPressed(int butIndex){
		//index = butIndex;
		DTO.setIndex(butIndex);
		SceneManager.LoadScene ("Mockup");
		canvas.enabled = false;
	}
}
