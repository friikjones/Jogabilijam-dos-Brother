using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBall : MonoBehaviour {

    public GameObject other;
    public Vector3 cameraOffset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = other.transform.position + cameraOffset;
	}
}
