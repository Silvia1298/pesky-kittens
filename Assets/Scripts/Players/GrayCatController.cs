using UnityEngine;
//este gato para el tiempo durante unos segundos
public class GrayCatController : Ability
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