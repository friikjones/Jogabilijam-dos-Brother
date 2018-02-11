using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PwUpPad : MonoBehaviour {



 
    Material mat;
    private GameObject pad;
    
    
	// Use this for initialization
	void Start () {
        mat = GetComponent<MeshRenderer>().material;
        mat.color = Color.yellow;

        
                
    }

  
    
    void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.tag == "Player")
        {

            


            mat.color = Color.green;
            pad = GameObject.FindGameObjectWithTag("PowerUp");
            pad.gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(CD());
               
        }
        

    }

    IEnumerator CD() //tempo de cd do pwPad
    {
        yield return new WaitForSeconds(3f);
        pad.gameObject.GetComponent<BoxCollider>().enabled = true;
        mat.color = Color.yellow;
    }




}
