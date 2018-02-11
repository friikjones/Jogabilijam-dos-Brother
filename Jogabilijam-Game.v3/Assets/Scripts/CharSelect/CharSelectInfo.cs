using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharSelectInfo : MonoBehaviour
{

	//private static int index;
	public RawImage[] mask;
	private Canvas canvas;
	private CharSelectDTOScript DTO;
    //variáveis de posicionamento dos personagens
	private Vector3 positSaci;
	private Vector3 positCurupira;
	private Vector3 positMula;
	private Vector3 positCuca;
    //variáveis de imagem, carrega o desenho dos personagems
	public RawImage imageSaci;
	public RawImage imageCurupira;
	public RawImage imageMula;
	public RawImage imageCuca;
	public RawImage numLaps;
    //variável de indexação e tempo de seleção.
	private int[] currentCharIndex;
	private float[] TempoP;

	private const float delayInputs = 0.4f;

	void Start ()
    {
		
		canvas = GameObject.Find("EmptyThingy").GetComponentInChildren<Canvas>();
		DTO = GameObject.Find("CharSelectDTO").GetComponent<CharSelectDTOScript>();
		currentCharIndex = new int[4];
		TempoP = new float[4];

		positSaci 	  =	imageSaci.transform.position + new Vector3(0,20,0);
		positCurupira = imageCurupira.transform.position + new Vector3(0,20,0);
		positMula 	  =	imageMula.transform.position + new Vector3(0,20,0);
		positCuca 	  =	imageCuca.transform.position + new Vector3(0,20,0);

		mask [0].transform.position = positSaci;
		mask [1].transform.position = positCurupira;
		mask [2].transform.position = positMula;
		mask [3].transform.position = positCuca;

		TempoP [0] = 0;
		TempoP [1] = 0;
		TempoP [2] = 0;
		TempoP [3] = 0;

	}
	
	// Update is called once per frame
	void Update ()
    {

		CheckKeyboardControll ();
		this.
		moveMask (0);
		
	}

	public void moveMask(int i)//move a máscara de seleção de personagem para o personagem correspondente
    {
		Vector3 aux = new Vector3 ();
		switch (currentCharIndex[i])
        {
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
		mask[i].transform.position = aux;

	}

	public void escolheChar()//seleção dos carros e carrega a cena da corrida.
    {
		Debug.Log ("char escolhido = " + currentCharIndex);
		DTO.setIndex(currentCharIndex[0]);
		SceneManager.LoadScene ("CenaCarro");//carrega a cena da corrida.
		canvas.enabled = false;//desabilita a tela de seleção.
	}

	public void CheckKeyboardControll()//lê se algum jogador deu algum input para selecionar personagem.
    {
		float now = Time.realtimeSinceStartup;
		for(int i =0;i<4;i++)//faz a leitura de entrada de cada jogador.
        {
			if (now > TempoP [i])
            {
				if (Input.GetAxis ("DireitaP" + (i + 1).ToString ())> 0.99f)
                {
					currentCharIndex [i] += 1;
					TempoP [i] += delayInputs;
				}
				if (Input.GetAxis ("EsquerdaP" + (i + 1).ToString ())> 0.99f)
                {
					currentCharIndex [i] -= 1;
					TempoP [i] += delayInputs;
				}
				if (Input.GetAxis ("CimaP" + (i + 1).ToString ()) > 0.99f)
                {
					currentCharIndex [i] -= 2;
					TempoP [i] += delayInputs;
				}
				if (Input.GetAxis ("BaixoP" + (i + 1).ToString ())> 0.99f)
                {
					currentCharIndex [i] += 2;
					TempoP [i] += delayInputs;
				}
				while (currentCharIndex [i] < 0 || currentCharIndex [i] > 3)
                {
					if (currentCharIndex [i] < 0)
						currentCharIndex [i] += 4;
					if (currentCharIndex [i] > 3)
						currentCharIndex [i] -= 4;
				}
			}

		if (Input.GetButtonDown ("Confirma"))
			escolheChar ();
	}
    }

    //public void butSaciPressed()
    //{
    // currentCharIndex[0] = 0;
    //     escolheChar();
    //}

    // public void butCurupiraPressed()
    // {
    // currentCharIndex[0] = 1;
    //    escolheChar();
    //}

    //public void butMulaPressed()
    //{
    //	currentCharIndex[0] = 2;
    //    escolheChar();
    //}

    // public void butCucaPressed()
    // {
    // currentCharIndex[0] = 3;
    //    escolheChar();
    //}//métodos que, possivelmente, não são usados no programa.


}
