using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float angularSpeed;
    
    private void FixedUpdate()
    {
        RotateShip();
        MoveShip();
    }

    private void RotateShip()
    {
        Vector3 changeStep = Vector3.forward * angularSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles += changeStep;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles -= changeStep;
        }

    }

    private void MoveShip()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;
    }
    
}