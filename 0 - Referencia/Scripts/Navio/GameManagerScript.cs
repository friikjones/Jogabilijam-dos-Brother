using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public GameObject Iceberg;

    private GameObject currentInstance;

    // Use this for initialization
    void Start () {

        IcebergSpawner(300,600,100);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void IcebergSpawner(int amount, int range, int initialSpace)
    {

        float X = 0f;
        float Z = 0f;
        float R = 0f;

        for (int i = 0; i < amount; i++)
        {
            do
            {
                X = Random.Range(-range, range);
            } while (X < -initialSpace && X > initialSpace);

            do
            {
                Z = Random.Range(-range,range);
            } while (Z < -initialSpace && Z > initialSpace);
            
            R = Random.Range(-180, 180);


            currentInstance = Instantiate(Iceberg, new Vector3(X, 0, Z), Quaternion.identity);
            currentInstance.transform.parent = this.transform;
            currentInstance.transform.Rotate(new Vector3(0, R, 0));

        }
    }
}
