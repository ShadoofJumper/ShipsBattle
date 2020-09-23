using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] protected Combat parentCombat;
    //spot from where wee shoot
    [SerializeField] protected Transform turretSpot;
    //bullet prefab
    [SerializeField] protected GameObject bulletOriginal;
    //turret war side
    protected ColorSide turretColorSide = ColorSide.red;

    //current target attack
    protected Transform currentTarget;

    //for bullet shoot
    protected float timeToFire;
    protected float fireRate = 1.0f;
    protected float bulletSpeed = 10.0f;
    
    //search ray for check where enemy 
    protected Vector2 searchRay = Vector2.up;
    protected float raySpeed = 1000.0f;
    

    public void TurretInit(float fireRate, GameObject bulletOriginal, float bulletSpeed, ColorSide turretColorSide)
    {
        this.fireRate = parentCombat.FireRate;
        this.bulletOriginal = parentCombat.BulletOriginal;
        this.bulletSpeed = parentCombat.BulletSpeed;
        this.turretColorSide = parentCombat.ColorSide;
    }
    
    protected Transform SearchTarget()
    {
        //rotate ray
        float step = raySpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, step);
        searchRay = rotation * searchRay;
        
        //get all raycast hits
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, searchRay);
        //take first hit that is target
        GameObject firstTarget = GetFirstTargetInHits(hits);
        
        //if hit something and this is destroyable object and it is closer then old target
        if (firstTarget &&  IsNewTargetСloser(firstTarget.transform))
        {
            return firstTarget.transform;
        }

        //Vector3 test = new Vector3(searchRay.x, searchRay.y, 0);
        //Debug.DrawLine(turretSpot.position, turretSpot.position + test * 20, Color.red);
        return null;
    }

    protected GameObject GetFirstTargetInHits(RaycastHit2D[] hits)
    {
        foreach (var hit in hits)
        {
            if (IsObjectTarget(hit.collider.gameObject))
            {
                return hit.collider.gameObject;
            } 
        }

        return null;
    }
    
    protected bool IsObjectTarget(GameObject target)
    {
        Destroyable destroyableTarget = target.GetComponent<Destroyable>();
        //if(destroyableTarget != null)
        //    Debug.Log("Destroyable!: "+target.name +" current color: "+turretColorSide.ToString()+" target: "+destroyableTarget.ColorSide.ToString());
        
        if (destroyableTarget && destroyableTarget.ColorSide != turretColorSide)
        {
            return true;
        }

        return false;
    }

    protected Quaternion GetRotationToTarget(Transform target)
    {
        Vector3 directionToTarget = Vector3.Normalize(target.position - turretSpot.position);
        
        //Debug.DrawLine(turretSpot.position, turretSpot.position + directionToTarget * 20, Color.green);
        //Debug.DrawLine(turretSpot.position, turretSpot.position + turretSpot.right * 20, Color.yellow);

        return Quaternion.FromToRotation(turretSpot.right, directionToTarget) * parentCombat.gameObject.transform.rotation;
    }

    protected bool IsNewTargetСloser(Transform target)
    {
        if (currentTarget == null)
            return true;

        float distToCurrentTarget = Vector3.Distance(turretSpot.position, currentTarget.position);
        float distToNewTarget = Vector3.Distance(turretSpot.position, target.position);

        return distToNewTarget < distToCurrentTarget;
    }

    protected void Shoot(Quaternion rotationToTarget)
    {
        if (Time.time > timeToFire)
        {
            ShootBullets(rotationToTarget);
            timeToFire = Time.time + 1 / fireRate;
        }
    }

    protected void ShootBullets(Quaternion rotation)
    {
        Bullet bullet = parentCombat.BulletPool.Count > 0
            ? GetBullet(rotation, turretSpot.position)
            : CreateBullet(rotation, turretSpot.position);
        bullet.StartTimerToSelfDestroy();
    }

    protected Bullet CreateBullet(Quaternion rotation, Vector3 position)
    {
        GameObject bulletObject = Instantiate(bulletOriginal, position, rotation);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bulletObject.transform.SetParent(MainManager.instance.sceneController.FolderForBullets);
        bullet.Init(bulletSpeed, turretColorSide);
        bullet.OnBulletDestroy = OnBulletDestroy;
        return bullet;
    }

    protected Bullet GetBullet(Quaternion rotation, Vector3 position)
    {
        GameObject bulletObject = parentCombat.BulletPool.Dequeue();
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bulletObject.transform.position = position;
        bulletObject.transform.rotation = rotation;
        bulletObject.SetActive(true);
        return bullet;
    }

    protected void OnBulletDestroy(GameObject bulletObject)
    {
        bulletObject.transform.position = Vector3.zero;
        bulletObject.SetActive(false);
        parentCombat.BulletPool.Enqueue(bulletObject);
    }
    
}

