using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sprites : Controller
{

    public Sprite spUp, spDown, spLeft, spRight, spHoldRight, spSuperHoldRight, spHoldLeft, spSuperHoldLeft;
    




    public override void ReadInput(InputData data) {

        if (data.axes[1] == -1)
        {
            this.GetComponent<SpriteRenderer>().sprite = spRight;
        }



    }
}

    /* // Use this for initialization
    void Start()
    {


        this.GetComponent<SpriteRenderer>().sprite = spUp;

    }


    

    // Update is called once per frame
    
        
        
        
        
        void Update()
    {

        if (data.axes[1] != 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = spRight;
        }









        
        if (Input.GetKey(KeyCode.A))
        {

            this.GetComponent<SpriteRenderer>().sprite = spLeft;

            driftR = driftR + 1;

            if (driftR > 25)
            {
                this.GetComponent<SpriteRenderer>().sprite = spHoldLeft;


            }
            if (driftR > 35)
            {
                this.GetComponent<SpriteRenderer>().sprite = spSuperHoldLeft;
            }

        }
        else
        {
            driftR = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.GetComponent<SpriteRenderer>().sprite = spRight;

            driftL = driftL + 1;

            if (driftL > 25)
            {
                this.GetComponent<SpriteRenderer>().sprite = spHoldRight;


            }
            if (driftL > 35)
            {
                this.GetComponent<SpriteRenderer>().sprite = spSuperHoldRight;


            }

        }
        else
        {

            driftL = 0;
        }


    }
}*/


        
        
        
        
        
        
        
        
       
       