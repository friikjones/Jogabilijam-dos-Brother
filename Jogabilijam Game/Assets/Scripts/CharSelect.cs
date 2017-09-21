using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelect : MonoBehaviour {

	private GameObject[] charList;
	private int index;

	// Use this for initialization
	void Start () {

		index = PlayerPrefs.GetInt ("CharSelected");

		//Popula o array com os bonecos
		charList = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
			charList [i] = transform.GetChild (i).gameObject;

		//Desliga o render de cada uma
		foreach (GameObject go in charList) {
			go.SetActive (false);
			System.Console.WriteLine(go.name);
		}

		//Liga só o certo
		if (charList [index]) {
			charList [index].SetActive(true);
		}

	}

	public void ToggleLeft(){

		charList [index].SetActive (false);

		index--;
		if (index < 0)
			index = charList.Length - 1;

		charList [index].SetActive (true);
	}

	public void ToggleRight(){

		charList [index].SetActive (false);

		index++;
		if (index == charList.Length)
			index = 0;

		charList [index].SetActive (true);
	}

	public void ConfirmButton(){
		PlayerPrefs.SetInt ("CharSelected", index);
		SceneManager.LoadScene("Mockup");
	}




	
	// Update is called once per frame
	void Update () {
		
	}
}
