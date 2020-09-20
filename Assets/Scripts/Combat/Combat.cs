using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] protected GameObject bulletOriginal;
    
    private int health;
    private int damage;
    
    public void TakeDamage(int value)
    {
        health -= value;
        if (health <= 0)
            Die();
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }



}
