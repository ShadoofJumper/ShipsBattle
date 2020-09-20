using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    private Rigidbody2D bulletRig;

    public void Init(float speed)
    {
        this.speed = speed;
    }
    
    private void Start()
    {
        bulletRig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        bulletRig.MovePosition(bulletRig.position + Vector2.up * speed * Time.deltaTime);
    }
}
