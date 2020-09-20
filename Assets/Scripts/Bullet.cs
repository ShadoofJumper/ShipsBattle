using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float delayDestroy = 7.0f;

    private Rigidbody2D bulletRig;
    private ShipSide bulletShipSide;
    
    public Action<GameObject> OnBulletDestroy;
    
    public void Init(float speed, ShipSide bulletShipSide)
    {
        this.speed = speed;
        this.bulletShipSide = bulletShipSide;
    }
    
    private void Start()
    {
        bulletRig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void StartTimerToSelfDestroy()
    {
        StartCoroutine(StartTimerToSelfDestroy(delayDestroy));
    }

    IEnumerator StartTimerToSelfDestroy(float delayDestroy)
    {
        yield return new WaitForSeconds(delayDestroy);
        if (gameObject.activeSelf)
            OnBulletDestroy(gameObject);
    }
    
    private void Move()
    {
        Vector3 newPos = transform.position + transform.right * speed * Time.deltaTime;
        bulletRig.MovePosition(newPos);
    }
}
