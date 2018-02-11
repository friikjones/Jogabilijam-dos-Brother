﻿using System.Collections;
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
	private Vector3 v3Horizontal;

    public int accellScale;
    public int reverseScale;
    public int rotateScale;
    public int boost = 1;
    private int driftR, driftL;
    public float gravidade=14f;
	public string accelAxis = "Accel_P1";
	public string turnAxis = "Horizontal_P1";
	public string turnRButton = "TurnR_P1";
	public string turnLButton = "TurnL_P1";

	private float horizontalValue;
	private float verticalValue;
	public bool isMovementAllowed;

    


    // Use this for initialization
    void Start()
    {
        


        rb = GetComponent<Rigidbody>();
        v3Front = new Vector3(0, 0, 1);
        v3Back = new Vector3(0, 0, -1);
        v3Left = new Vector3(0, -1, 0);
        v3Right = new Vector3(0, 1, 0);
        v3Up = new Vector3(0, 1, 0);
		v3Horizontal = new Vector3(0, 1, 0);

		isMovementAllowed = false;
        
    }

    // Update is called once per frame
    void Update()
    {
		if(isMovementAllowed)
        	MovementControl();
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravidade * rb.mass);

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

//        if (Input.GetKey(KeyCode.W))
//        {
//            rb.AddRelativeForce(v3Front * accellScale * boost);
//        }
//
//        if (Input.GetKey(KeyCode.S))
//        {
//            rb.AddRelativeForce(v3Back * reverseScale);
//        }

		verticalValue = Input.GetAxis (accelAxis);

		if(verticalValue>0)
			rb.AddRelativeForce(v3Front * accellScale * boost);
		if (verticalValue<0)
			rb.AddRelativeForce(v3Back * reverseScale);

		if (Input.GetButton(turnLButton))
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
			

    


		if (Input.GetButton(turnRButton))
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



