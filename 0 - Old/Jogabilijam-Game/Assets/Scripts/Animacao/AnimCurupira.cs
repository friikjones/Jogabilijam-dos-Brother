using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCurupira : MonoBehaviour
{

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
        inverte = GameObject.Find("Curupira").GetComponent<CarControllerMinimalCurupira>().inverteControle;

        


        if (Input.GetKey(KeyCode.RightArrow))
        {
            direita = true;
        }
        else
        {
            direita = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
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
