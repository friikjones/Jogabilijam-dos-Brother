using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    public Transform camTransform;
	
	// Update is called once per frame
	void Update () {

        transform.rotation = camTransform.rotation;

        Debug.Log(transform.rotation);

	}
}
