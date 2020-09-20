using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Combat
{
    [SerializeField] private Transform shootSpotFront;
    [SerializeField] private Transform shootSpotBack;
    [SerializeField] private float bulletSpeed;

    private Queue<GameObject> bulletPool;
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //shoot front
            ShootBullets(true);
        }

        if (Input.GetMouseButton(1))
        {
            //shoot back
            ShootBullets(false);
        }
        
        Debug.DrawLine(shootSpotFront.position, shootSpotFront.position + shootSpotFront.forward * 20, Color.green);
        Debug.DrawLine(shootSpotBack.position, shootSpotBack.position + shootSpotBack.forward * 20, Color.red);


    }

    private void ShootBullets(bool isFront)
    {
        Vector3 direction = isFront ? shootSpotFront.forward : shootSpotBack.forward;
        Vector3 position = isFront ? shootSpotFront.position : shootSpotBack.position;
        
        GameObject bulletObject = CreateBullet(direction, position);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.Init(bulletSpeed);
    }


}
