using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : Controller
{
    //Dados do Veículo
    public float maxSpeed = 6f;
    public float timeZeroToMax = 2.5f;
    public float timeMaxToZero = 6f;
    public float timeBrakeToZero = 1f;
    public float turnAnglePerSec = 90f;

    //Dados calculados em tempo real
    float accelRatePerSec;
    float decelRatePerSec;
    float brakeRatePerSec;

    //Estado atual do veículo
    float fowardVelocity;
    float currentTurn;
    bool accelChange;
    bool inReverse;

    void Start()
    {
        //configuração inicial do veículo
        accelRatePerSec = maxSpeed / timeZeroToMax;
        decelRatePerSec = -maxSpeed / timeMaxToZero;
        brakeRatePerSec = -maxSpeed / timeBrakeToZero;
        fowardVelocity = 0f;
        currentTurn = 0f;
        inReverse = false;
    }



    public override void ReadInput(InputData data)
    {

        //registrar e executar os controles do veículo

        //botao de curva
        if (data.axes[1] != 0)
        {
            currentTurn = turnAnglePerSec * Time.deltaTime * (data.axes[1] > 0 ? 1 : -1);
        }

        //botao para a ré
        if (data.axes[0] < 0)
        {
            inReverse = true;
            Accelerate(accelRatePerSec / 2);

        }


        //botao de acelerar
        if (data.axes[0] > 0)
        {
            Accelerate(accelRatePerSec);

        }

        //botao de freio

        if (data.buttons[0] == true)
        {
            Brake(brakeRatePerSec);
        }



        newInput = true;
    }



    void LateUpdate()
    {
        //carro não pode virar, se estiver parado ese não estiver andando para frente
        if (fowardVelocity > 0f)
        {
            rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0, currentTurn, 0));

        }

        //se não houver aceleração, desacelere
        if (!accelChange)
        {
            Brake(decelRatePerSec);
        }


        //acelere baseado na velociade atual
        rb.velocity = transform.forward * fowardVelocity;

        //resetar estados para o proximo frame
        accelChange = false;
        currentTurn = 0f;
        inReverse = false;
        newInput = false;
    }


    void Accelerate(float accel)
    {
        float reverseFactor = inReverse ? -1 : 1;
        fowardVelocity += accel * Time.deltaTime * reverseFactor;
        fowardVelocity = Mathf.Clamp(fowardVelocity, -maxSpeed, maxSpeed);
        accelChange = true;

    }

    void Brake(float decel)
    {
        if (fowardVelocity == 0)
        {
            accelChange = true;
            return;
        }

        float reverseFactor = Mathf.Sign(fowardVelocity);
        fowardVelocity = Mathf.Abs(fowardVelocity);
        fowardVelocity += decel * Time.deltaTime;
        fowardVelocity = Mathf.Max(fowardVelocity, 0) * reverseFactor;
        accelChange = true;
    }


}
