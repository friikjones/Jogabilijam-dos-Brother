using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapSystem : MonoBehaviour
{

    public int n_checks;
    public int prox_check;
    public int n_volta;
    public int volta_atual;

    void Start()
    {


        n_checks = GameObject.Find("Checkpoints").transform.childCount; //contagem do numero de check points
        prox_check = 1;
        n_volta = 3;
        volta_atual = 1;
    }

    void Update()
    {
        if (prox_check > n_checks)   //verifica se passou pelos checks em ordem
        {
            volta_atual++;
            prox_check = 1;

        }

        if (volta_atual == n_volta)
        {

            Debug.Log("Finish");

        }


    }

    private void OnTriggerEnter(Collider check_col)
    {
        if (check_col.name == prox_check.ToString())   //se passar pelo check, incrementa
        {

            prox_check++;
        }
    }
}