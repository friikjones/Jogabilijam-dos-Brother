using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActonType
{
    Interact,
    Attack
}


[RequireComponent(typeof(BoxCollider))]
public class PlayerHitbox : MonoBehaviour
{

    //movimento do colisor
    float offset = 1f;
    BoxCollider col;

    //duração do colisor
    float duration;

    //float secundario de dano
    float secondary;

    //traquear tipo de ação
    ActonType action;



    private void Awake()
    {
        WalkingController.OnFancingChange += RefreshFacing;
        WalkingController.OnInteract += StartCollisionCheck;
        col = GetComponent<BoxCollider>();
        col.enabled = false;
    }

    private void Update()
    {
        if (col.enabled)
        {
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                col.enabled = false;
            }
        }
    }

    void StartCollisionCheck(float dur, float sec, ActonType act)
    {
        action = act;
        gameObject.layer = (act == ActonType.Interact) ? 8 : 10;
        col.enabled = true;
        duration = dur;
        secondary = sec;
    }




    void RefreshFacing(FacingDirection fd)
    {
        switch (fd)
        {
            case FacingDirection.North:
                col.center = Vector3.forward * offset;
                break;
            case FacingDirection.East:
                col.center = Vector3.right * offset;
                break;
            case FacingDirection.West:
                col.center = Vector3.left * offset;
                break;
            default:
                col.center = Vector3.back * offset;
                break;

        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (action == ActonType.Interact)
        {
            col.GetComponent<InteractObject>().Interact();
        }
        else
        {
            col.GetComponent<AttackbleObject>().TakeDamage(secondary);
        }

        
    }



}
