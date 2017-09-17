using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprites : MonoBehaviour
{

    public Sprite spUp, spDown, spLeft, spRight;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = spUp;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
            this.GetComponent<SpriteRenderer>().sprite = spUp;

        if (Input.GetKey(KeyCode.S))
            this.GetComponent<SpriteRenderer>().sprite = spDown;

        if (Input.GetKey(KeyCode.D))
            this.GetComponent<SpriteRenderer>().sprite = spRight;

        if (Input.GetKey(KeyCode.A))
            this.GetComponent<SpriteRenderer>().sprite = spLeft;





    }
}
