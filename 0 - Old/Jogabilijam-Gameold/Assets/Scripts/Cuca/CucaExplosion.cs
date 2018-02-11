using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucaExplosion : MonoBehaviour
{

    public float radius;
    public float power;
    public float upForce;
    public GameObject bomb;
   // GameObject goIgnore;
   



    private void FixedUpdate()
    {


        Detonar();

    }

    void Detonar()
    {
        Vector3 explosionPosition = bomb.transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

        foreach (Collider hit in colliders)
        {

            Rigidbody rb = hit.GetComponent<Rigidbody>();


          
                    if (rb != null)
                    {
                        //goIgnore = GameObject.Find("Cuca");
                        Rigidbody rbIgnore = GameObject.Find("Cuca").GetComponent<Rigidbody>();

                        if (rb != rbIgnore)
                        {
                            rb.AddExplosionForce(-power, explosionPosition, radius, upForce, ForceMode.Impulse);
                        }


                    }
        }
    }


}






