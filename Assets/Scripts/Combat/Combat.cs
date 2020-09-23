using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : Destroyable
{
    [SerializeField] protected GameObject bulletOriginal;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float fireRate = 1.0f;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    public float BulletSpeed => bulletSpeed;
    public float FireRate => fireRate;
    public GameObject BulletOriginal => bulletOriginal;

    public Queue<GameObject> BulletPool
    {
        get
        {
            return bulletPool;
        }
        set
        {
            bulletPool = value;
        }
    }
    

}
