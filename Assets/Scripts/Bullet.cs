using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float delayDestroy = 7.0f;
    [SerializeField] private int damage = 1;
    
    private Rigidbody2D bulletRig;
    private ColorSide bulletColorSide;
    
    public Action<GameObject> OnBulletDestroy;
    
    public void Init(float speed, ColorSide bulletColorSide)
    {
        this.speed = speed;
        this.bulletColorSide = bulletColorSide;
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroyable destroyableObject = other.GetComponent<Destroyable>();

        if ( destroyableObject != null )
        {
            //get damage object can do
            if (destroyableObject.ColorSide != bulletColorSide)
                destroyableObject.TakeDamage(damage);
            else
                return;
        }
        OnBulletDestroy(gameObject);
    }
    
 

}
