using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 front;
    private Vector3 back;
    private Vector3 left;
    private Vector3 right;

    private GameObject ship;
    private ShipController shipController;

    public bool movementStation;

    public float translateScale;


    // Use this for initialization
    void Start()
    {

        front = new Vector3(0, 0, -1);
        back = new Vector3(0, 0, 1);
        left = new Vector3(-1, 0, 0);
        right = new Vector3(1, 0, 0);
        

        shipController = this.GetComponentInParent<ShipController>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementStation = this.GetComponentInParent<ShipController>().movementStation;

        if (!movementStation)
            MovementControl();

    }

    void MovementControl()
    {

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(front * translateScale * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(back * translateScale * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(left * translateScale * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(right * translateScale * Time.deltaTime);
        }

    }
}
