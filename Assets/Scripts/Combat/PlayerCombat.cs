using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Combat
{
    [SerializeField] private PlayerTurret backTurret;
    [SerializeField] private PlayerTurret frontTurrent;

    private void Start()
    {
        backTurret.TurretInit(fireRate, bulletOriginal, bulletSpeed, colorSide);
        frontTurrent.TurretInit(fireRate, bulletOriginal, bulletSpeed, colorSide);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            frontTurrent.ShootPlayerTurret();

        if (Input.GetMouseButton(1))
            backTurret.ShootPlayerTurret();
    }

    public override void TakeDamage(int value)
    {
        health -= value;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        MainManager.instance.uiController.UpdatePlayerHealth(health);
    }



}
