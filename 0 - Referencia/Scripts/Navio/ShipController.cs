using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 front;
    private Vector3 back;
    private Vector3 left;
    private Vector3 right;

    public bool movementStation;

    public float translateScale;
    public float rotateScale;


    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        front = new Vector3(0, 0, 1);
        back = new Vector3(0, 0, -1);
        left = new Vector3(0, -1, 0);
        right = new Vector3(0, 1, 0);

        movementStation = false;

    }

    // Update is called once per frame
    void Update() {

        if (movementStation)
            MovementControl();

        if (Input.GetKeyUp(KeyCode.Space))
            movementStation = !movementStation;
    }

    void MovementControl() {

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(front * translateScale);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(back * translateScale);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeTorque(left * rotateScale);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeTorque(right * rotateScale);
        }

    }

}
