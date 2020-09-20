using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Combat
{
    [SerializeField] private Transform shootSpotFront;
    [SerializeField] private Transform shootSpotBack;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate = 1.0f;
    private float timeToFireForward;
    private float timeToFireBackward;
    
    private Queue<GameObject> bulletPool = new Queue<GameObject>();
    private ShipSide playerShipSide;

    private void Start()
    {
        playerShipSide = gameObject.GetComponent<Ship>().ShipType;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            Shoot(true);

        if (Input.GetMouseButton(1))
            Shoot(false);
        
        Debug.DrawLine(shootSpotFront.position, shootSpotFront.position + shootSpotFront.right * 20, Color.green);
        Debug.DrawLine(shootSpotBack.position, shootSpotBack.position + shootSpotBack.right * 20, Color.red);
    }

    private void Shoot(bool isFrontSpot)
    {
        ref float timeToFire = ref timeToFireForward;
        if (!isFrontSpot)
            timeToFire = ref timeToFireBackward;
        
        if (Time.time > timeToFire)
        {
            ShootBullets(isFrontSpot);
            timeToFire = Time.time + 1 / fireRate;
        }
    }
    
    private void ShootBullets(bool isFrontSpot)
    {
        Quaternion rotation = isFrontSpot ? shootSpotFront.rotation : shootSpotBack.rotation;
        Vector3 position = isFrontSpot ? shootSpotFront.position : shootSpotBack.position;

        Bullet bullet = bulletPool.Count > 0 ? GetBullet(rotation, position) : CreateBullet(rotation, position);
        bullet.StartTimerToSelfDestroy();
    }

    private Bullet CreateBullet(Quaternion rotation, Vector3 position)
    {
        GameObject bulletObject = Instantiate(bulletOriginal, position, rotation);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bulletObject.transform.SetParent(MainManager.instance.sceneController.FolderForBullets);
        bullet.Init(bulletSpeed, playerShipSide);
        bullet.OnBulletDestroy = OnBulletDestroy;
        return bullet;
    }

    private Bullet GetBullet(Quaternion rotation, Vector3 position)
    {
        GameObject bulletObject = bulletPool.Dequeue();
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bulletObject.transform.position = position;
        bulletObject.transform.rotation = rotation;
        bulletObject.SetActive(true);
        return bullet;
    }

    private void OnBulletDestroy(GameObject bulletObject)
    {
        bulletObject.transform.position = Vector3.zero;
        bulletObject.SetActive(false);
        bulletPool.Enqueue(bulletObject);
    }


}
