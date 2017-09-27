using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharSelectInfo : MonoBehaviour {

	//private static int index;
	public RawImage mask;
	private Canvas canvas;
	private CharSelectDTOScript DTO;

	private Vector3 positSaci;
	private Vector3 positCurupira;
	private Vector3 positMula;
	private Vector3 positCuca;

	private int currentCharIndex;

	// Use this for initialization
	void Start () {
		
		Vector3 xTransform = new Vector3 (240, 0, 0);
		Vector3 yTransform = new Vector3 (0, 230, 0);
		canvas = GameObject.Find("EmptyThingy").GetComponentInChildren<Canvas>();
		DTO = GameObject.Find("CharSelectDTO").GetComponent<CharSelectDTOScript>();
		positSaci = mask.transform.position;
		positCurupira = mask.transform.position + xTransform;
		positMula = mask.transform.position - yTransform;
		positCuca = mask.transform.position + xTransform - yTransform;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Direita"))
			currentCharIndex += 1;
		if (Input.GetButtonDown ("Esquerda"))
			currentCharIndex -= 1;
		if (Input.GetButtonDown ("Cima"))
			currentCharIndex -= 2;
		if (Input.GetButtonDown ("Baixo"))
			currentCharIndex += 2;
		while (currentCharIndex < 0 || currentCharIndex > 3) {
			if (currentCharIndex < 0)
				currentCharIndex += 4;
			if (currentCharIndex > 3)
				currentCharIndex -= 4;
		}

		moveMask ();

		if (Input.GetButtonDown ("Confirma"))
			escolheChar ();
		
	}

	public void moveMask(){
		Vector3 aux = new Vector3 ();
		switch (currentCharIndex) {
		case 0:
			aux = positSaci;
			break;
		case 1:
			aux = positCurupira;
			break;
		case 2:
			aux = positMula;
			break;
		case 3:
			aux = positCuca;
			break;
		}
		mask.transform.position = aux;

	}

	public void escolheChar(){
		DTO.setIndex(currentCharIndex);
		SceneManager.LoadScene ("Mockup");
		canvas.enabled = false;
	}
}
