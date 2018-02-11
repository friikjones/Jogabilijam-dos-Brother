using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBallControler : MonoBehaviour {

    private Rigidbody rb;

    public float forceMultiplier;
    public float rotateMultiplier;
    private float instantRotateMultiplier;
    private float instantForceMultiplier;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);

        if(MoveFoward())
        {
            rb.AddRelativeForce(Vector3.forward * instantForceMultiplier);
        }
        if (MoveBackwards())
        {
            rb.AddRelativeForce(Vector3.back * instantForceMultiplier);
        }
        if (RotateLeft())
        {
            rb.AddRelativeTorque(Vector3.down * instantRotateMultiplier);
        }
        if (RotateRight())
        {
            rb.AddRelativeTorque(Vector3.up * instantRotateMultiplier);
        }
        if (Slide())
        {
            rb.drag = 1;
            instantRotateMultiplier = rotateMultiplier * 2;
            instantForceMultiplier = forceMultiplier / 2;
        }
        else
        {
            rb.drag = 2;
            instantRotateMultiplier = rotateMultiplier;
            instantForceMultiplier = forceMultiplier;
        }
    }

    bool MoveFoward ()
    {
        if (Input.GetKey(KeyCode.W))
            return true;
        else
            return false;
    }
    bool MoveBackwards()
    {
        if (Input.GetKey(KeyCode.S))
            return true;
        else
            return false;
    }
    bool RotateLeft()
    {
        if (Input.GetKey(KeyCode.A))
            return true;
        else
            return false;
    }
    bool RotateRight()
    {
        if (Input.GetKey(KeyCode.D))
            return true;
        else
            return false;
    }

    bool Slide()
    {
        if (Input.GetKey(KeyCode.Space))
            return true;
        else
            return false;
    }

}
