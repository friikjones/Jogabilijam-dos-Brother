using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerMinimal : MonoBehaviour {

	private Rigidbody rb;
	private Vector3 v3Front;
	private Vector3 v3Back;
	private Vector3 v3Left;
	private Vector3 v3Right;

	public int accellScale;
	public int reverseScale;
	public int rotateScale;
    public bool flagLEFT;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		v3Front = new Vector3(0, 0, 1);
		v3Back = new Vector3(0, 0, -1);
		v3Left = new Vector3(0, -1, 0);
		v3Right = new Vector3(0, 1, 0);

	}
	
	// Update is called once per frame
	void Update () 
	{
		MovementControl();
	}

	void MovementControl() 
	{
		if (Input.GetKey(KeyCode.W))
		{
			rb.AddRelativeForce(v3Front * accellScale);
		}
		if (Input.GetKey(KeyCode.S))
		{
			rb.AddRelativeForce(v3Back * reverseScale);
		}
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(v3Left * Time.deltaTime * rotateScale);
            flagLEFT = true;
        }
        else {
            flagLEFT = false;
}
		if (Input.GetKey(KeyCode.D))
		{
       	        transform.Rotate (v3Right * Time.deltaTime * rotateScale);
               
		}

	}
}
