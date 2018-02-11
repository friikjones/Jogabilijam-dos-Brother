using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucaExplosion : MonoBehaviour
{

    public float radius;//raio de ação da explosão
    public float power;//força da explosão
    public float upForce;
    public GameObject bomb;//a bomba que a cuca solta

    private void FixedUpdate()
    {
        Detonar();
    }

    void Detonar()//detona a bomba da cuca, na forma do objeto "bomb" utilizando a técnica de collider
    {
        Vector3 explosionPosition = bomb.transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius); //Returns an array with all colliders touching or inside the sphere.

        foreach (Collider hit in colliders)//código abaixo aplica  efeito da explosão em quem estiver dentro do raio de ação.
        {

            Rigidbody rb = hit.GetComponent<Rigidbody>();
            
                    if (rb != null)
                    {
                        Rigidbody rbIgnore = GameObject.Find("Cuca").GetComponent<Rigidbody>();//poupa a Cuca de receber a explosão.

                        if (rb != rbIgnore)
                        {
                            rb.AddExplosionForce(-power, explosionPosition, radius, upForce, ForceMode.Impulse);//aplica o efeito da explosão.
                        }


                    }
        }
    }


}






