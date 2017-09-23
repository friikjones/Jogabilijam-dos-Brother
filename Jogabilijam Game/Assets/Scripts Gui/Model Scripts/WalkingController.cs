using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FacingDirection
{
    North,
    East,
    South,
    West
}

public class WalkingController : Controller
{
    //informações de movimento
    Vector3 walkVelocity;
    Vector3 prevWalkVelocity;
    float adjVertVelocity;
    float jumpPressTime;
    FacingDirection facing = FacingDirection.South;


    //configurações
    public float walkSpeed = 3f;
    public float jumpSpeed = 8f;
    public float intractDuration = .1f;
    public float attackDamage = 5f;


    //eventos & delegações (mudança de sprites)
    public delegate void FacingChangeHandler(FacingDirection fd);
    public static event FacingChangeHandler OnFancingChange;
    public delegate void HitboxEventHandler(float dur, float sec,ActonType act);
    public static event HitboxEventHandler OnInteract;

    private void Start()
    {
        if (OnFancingChange != null)
        {
            OnFancingChange(facing);
        }
    }

   


    public override void ReadInput(InputData data)
    {
        prevWalkVelocity = walkVelocity;
        ResetMovimentToZero();

        //configurando movimento vertical
        if (data.axes[0] != 0f)
        {
            walkVelocity += Vector3.forward * data.axes[0] * walkSpeed;
        }

        //configurando movimento horizontal
        if (data.axes[1] != 0f)
        {
            walkVelocity += Vector3.right * data.axes[1] * walkSpeed;
        }

        //configurando movimento de pulo
        if (data.buttons[0] == true)
        {
            if (jumpPressTime == 0f)
            {
                if (Grounded())
                {
                    adjVertVelocity = jumpSpeed;
                }
            }
            jumpPressTime += Time.deltaTime;
        }
        else
        {
            jumpPressTime = 0f;
        }

        //check se botão de interação foi apertado
        if (data.buttons[1] == true)
        {
            if (OnInteract != null)
            {
                OnInteract(intractDuration, 0,ActonType.Interact);
            }
        }

        //check se botão de ataque foi apertado
        if (data.buttons[2] == true)
        {
            if (OnInteract != null)
            {
                OnInteract(intractDuration, attackDamage,ActonType.Attack);
            }
        }




        newInput = true;

    }
    

    //metodo para detectar contato com chão
    bool Grounded()
    {
        //raycast apontando para o chão
        return Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.1f);

    }






    private void LateUpdate()
    {
        if (!newInput)
        {
            prevWalkVelocity = walkVelocity;
            ResetMovimentToZero();
            jumpPressTime = 0f;

        }
        if (prevWalkVelocity != walkVelocity)
        {
            //verificando se mundou a direção que se está olhando
            CheckForFacingChange();
        }

        rb.velocity = new Vector3(walkVelocity.x, rb.velocity.y + adjVertVelocity, walkVelocity.z);

        newInput = false;

    }


    void CheckForFacingChange()
    {
        if (walkVelocity == Vector3.zero)
        {
            return;
        }

        if (walkVelocity.x == 0 || walkVelocity.z == 0)
        {
            //muda a direção baseado no walkVelocity
            ChangeFacing(walkVelocity);
        }
        else
        {
            if (prevWalkVelocity.x == 0)
            {
                ChangeFacing(new Vector3(walkVelocity.x, 0, 0));
            }
            else if (prevWalkVelocity.z == 0)
            {
                ChangeFacing(new Vector3(0, 0, walkVelocity.z));
            }
            else
            {
                Debug.LogWarning("Valor walkVelocity inexperado");
                ChangeFacing(walkVelocity);
            }
        }

    }



    //Metodo assume apenas valores de X e Z não vai ser zero no parametro dir. irá setar o valor z como padrão
    void ChangeFacing(Vector3 dir)
    {
        if (dir.z != 0)
        {
            facing = (dir.z > 0) ? FacingDirection.North : FacingDirection.South;
        }
        else if (dir.x != 0)
        {
            facing = (dir.x > 0) ? FacingDirection.East : FacingDirection.West;
        }

        //chamar o evento de direção 
        if (OnFancingChange != null)
        {
            OnFancingChange(facing);
        }

    }







    void ResetMovimentToZero()
    {


        walkVelocity = Vector3.zero;
        adjVertVelocity = 0f;


    }

        

}



