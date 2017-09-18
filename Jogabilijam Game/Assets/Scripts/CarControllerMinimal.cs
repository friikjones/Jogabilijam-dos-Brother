using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerMinimal : MonoBehaviour
{

    private CharacterController controller;

    private Rigidbody rb;
    private Vector3 v3Front;
    private Vector3 v3Back;
    private Vector3 v3Left;
    private Vector3 v3Right;
    private Vector3 v3Up;

    public int accellScale;
    public int reverseScale;
    public int rotateScale;
    public int boost = 1;
    private int driftR, driftL;
    private float gravidade=14f;

    


    // Use this for initialization
    void Start()
    {
        


        rb = GetComponent<Rigidbody>();
        v3Front = new Vector3(0, 0, 1);
        v3Back = new Vector3(0, 0, -1);
        v3Left = new Vector3(0, -1, 0);
        v3Right = new Vector3(0, 1, 0);
        v3Up = new Vector3(0, 1, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        MovementControl();
    }

    void MovementControl()
    {
        //PULO///////
       /* if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddRelativeForce(v3Up * 600);

        }
        else {
            rb.AddRelativeForce(v3Up * -30);
        }
        */

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(v3Front * accellScale * boost);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(v3Back * reverseScale);
        }



        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(v3Left * Time.deltaTime * rotateScale);
            driftL = driftL + 1;

            if (driftL > 22)
            {
                transform.Rotate(v3Left * Time.deltaTime * (rotateScale - 110));


            }
            if (driftL > 35)
            {
                transform.Rotate(v3Left * Time.deltaTime * (rotateScale - 120));
            }

        }
        else
        {
            driftL = 0;
        }


    


        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(v3Right * Time.deltaTime * rotateScale);

            driftR = driftR + 1;

            if (driftR > 22)
            {
                transform.Rotate(v3Right * Time.deltaTime * (rotateScale - 110));


            }
            if (driftR > 35)
            {
                transform.Rotate(v3Right * Time.deltaTime * (rotateScale - 120));
            }

        }
        else
        {
            driftR = 0;
        }


















    
    }



    ///// trigger do Boost /////
    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.name == "Boost Trigger")
        {

            boost = 2;

            StartCoroutine(StopBoost());

        }

       


    }

    IEnumerator StopBoost() //tempo de boost
    {
        yield return new WaitForSeconds(1.3f);
        boost = 1;

    }



  







}



