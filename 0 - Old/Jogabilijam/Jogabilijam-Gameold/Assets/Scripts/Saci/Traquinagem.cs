using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Traquinagem : MonoBehaviour
{

    Collider col;
    public  GameObject go;
    public static bool inverte;
    public  int raio = 15;
    public  int layerMask = (~(1 << 8) & ~(1 << 9)) & ~(1 << 10); //TODOS MENOS SACI, CENARIO E EFEITOS
    public  static string nome;
    public float skillTime;
    
    

    private void Awake()
    {
        col = go.GetComponent<CapsuleCollider>();

        col.enabled = false;

        skillTime = GameObject.Find("Saci").GetComponent<CarControllerMinimalSaci>().skillTime;
    }



    private void Update()
    {

        if (Input.GetKey(KeyCode.RightControl))
        {
            col.enabled = true;
            inverte = true;
            StartCoroutine(LimpaIverte());


        }

        

    }

    IEnumerator LimpaIverte()
    {
        yield return new WaitForSeconds(skillTime);
        col.enabled = false;
        inverte = false;
    }




    public  string CapturaTroll()
    {

        Vector3 colliderCenter = go.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(colliderCenter, raio, layerMask);

       

        int r = Random.Range(0, hitColliders.Length);

      


        if (hitColliders.Length != 0)
        {
           Debug.Log("O escolhido foi: " + hitColliders[r]);

            nome = hitColliders[r].ToString();

        }
        else
        {
            nome = null;
            

        }

        System.Array.Clear(hitColliders,0,hitColliders.Length);
        //Debug.Log("A lista limpa tem: " + hitColliders.Length);

        return nome;

    }

    
}


