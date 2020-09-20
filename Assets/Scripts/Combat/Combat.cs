using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private GameObject bulletOriginal;
    [SerializeField] private float fireRate;
    
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

    protected GameObject CreateBullet(Vector3 direction, Vector3 position)
    {
        return Instantiate(bulletOriginal, position, Quaternion.identity);
    }

}
