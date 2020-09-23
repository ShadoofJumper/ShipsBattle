using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Combat
{
    [SerializeField] private Turret[] turrets;

    private void Start()
    {
        foreach (var turret in turrets)
        {
            turret.TurretInit(fireRate, bulletOriginal, bulletSpeed, colorSide);
        }
    }
}
