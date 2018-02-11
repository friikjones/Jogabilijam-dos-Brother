using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFill : MonoBehaviour
{

    public GameObject Prefab;
    public int count = 2;
    public float skillTime = 1f;
    List<GameObject> prefabList = new List<GameObject>();

    
    public void FillScene()
    {
        int count_ = count;
        while (count_ > 0)
        //popular a cena com obstaculos
        {

            float x = Random.value * 200f - 100f;
            float z = Random.value * 200f - 100f;
            if (x > -5f && x < 5f && z > -5f && z < 5f)
                continue;

            Quaternion rot = Quaternion.Euler(0f, Random.value * 360f, 0f);
            GameObject novoPrefab = Instantiate(Prefab, new Vector3(x, 1, z), rot, transform);

            count_--;

            prefabList.Add(novoPrefab);
        }
        StartCoroutine(ResetArvere());
    }



    IEnumerator ResetArvere()
    {
        yield return new WaitForSeconds(skillTime);

        DestruirPrefabs();
        
    }

    public void DestruirPrefabs()
    {
        int conta = prefabList.Count;

        for (int i = 0; i < conta; i++)
        {
            Destroy(prefabList[i]);
        }

        prefabList.RemoveRange(0, conta);
    }


}
