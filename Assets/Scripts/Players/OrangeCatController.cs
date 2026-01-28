using UnityEngine;
//este gato tirara bolas de pelo al oponente
public class OrangeCatController : Ability
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
