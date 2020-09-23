using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [SerializeField] protected ColorSide colorSide;
    [SerializeField] protected int health;

    public ColorSide ColorSide => colorSide;
    
    public void TakeDamage(int value)
    {
        health -= value;
        if (health <= 0)
            Die();
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }
}
