using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Transform attackController;
    [SerializeField] private float hitRadius;
    [SerializeField] private int hitDmg;

    void Update()
    {
        if(Input.GetButtonDown("Attack"))
        {
            Hit();
        }
    }

    private void Hit()
    {
        Collider2D[]objects = Physics2D.OverlapCircleAll(attackController.position, hitRadius);
        
        foreach(Collider2D collider in objects)
        {
            if(collider.CompareTag("Enemy"))
            {
                collider.transform.GetComponent<EnemyController>().TakeDmg(hitDmg);
            }
        }
    }
}