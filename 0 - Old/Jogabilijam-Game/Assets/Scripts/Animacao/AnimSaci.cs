using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSaci : MonoBehaviour
{

    Animator anim;
    public bool direita = false;
    public bool esquerda = false;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            direita = true;
        }
        else
        {
            direita = false;
        }

        if (Input.GetKey(KeyCode.J))
        {
            esquerda = true;
        }
        else
        {
            esquerda = false;
        }
        anim.SetBool("Direita", direita);
        anim.SetBool("Esquerda", esquerda);






    }
}
