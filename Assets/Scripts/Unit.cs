using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public float Health;
    public float MaxHealth;
    public float AttackDamage;
    public float movementSpeed;

   

    private void Awake() {
        Health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
