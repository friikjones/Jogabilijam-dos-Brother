using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuInicialScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}

	public void Load2PlayerStd()
	{
		//Application.LoadLevel ("Jogo Base");
		SceneManager.LoadScene (3);
	}
	
	public void LoadMainMenu()
	{
		//Application.LoadLevel ("Jogo Base");
		SceneManager.LoadScene (0);
	}
	
	public void LoadInfoMenu1()
	{
		SceneManager.LoadScene (1);
	}
	
	public void LoadInfoMenu2()
	{
		SceneManager.LoadScene (2);
	}
}
