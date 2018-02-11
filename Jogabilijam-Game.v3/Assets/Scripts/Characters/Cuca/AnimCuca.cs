using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCuca : MonoBehaviour
{
    //programa lê as entradas do usuário para modificar o sprite da Cuca, de acordo com o botão apertado.
    Animator anim;
    public bool direita = false;
    public bool esquerda = false;
    public bool inverte = false;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        inverte = GameObject.Find("Cuca").GetComponent<CarControllerMinimalCuca>().inverteControle;
        
        if (Input.GetKey(KeyCode.PageDown))
        {
            direita = true;
        }
        else
        {
            direita = false;
        }

        if (Input.GetKey(KeyCode.Delete))
        {
            esquerda = true;
        }
        else
        {
            esquerda = false;
        }
        anim.SetBool("Direita", direita);
        anim.SetBool("Esquerda", esquerda);
        anim.SetBool("Inverte", inverte);






    }
}