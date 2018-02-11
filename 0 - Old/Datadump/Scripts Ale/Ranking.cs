using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{

    public GameObject car;
    public GameObject check1, check2, check3;
    public float dist1, dist2, dist3;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



        dist1 = Vector3.Distance(car.transform.position, check1.transform.position);
        dist2 = Vector3.Distance(car.transform.position, check2.transform.position);
        dist3 = Vector3.Distance(car.transform.position, check3.transform.position);
    }
}
