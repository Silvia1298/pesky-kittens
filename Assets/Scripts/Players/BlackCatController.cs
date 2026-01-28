using UnityEngine;
//este gato tiene doble salto
public class BlackCatController : Ability
{
   private bool used;

    public override void Activate()
    {
       
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        used = false;
    }
}