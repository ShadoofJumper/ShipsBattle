using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurret : Turret
{
    private void Update()
    {
        Transform target = SearchTarget();

        if (target)
            currentTarget = target;
    }
    
    public void ShootPlayerTurret()
    {
        Quaternion rotationToTarget = currentTarget ? GetRotationToTarget(currentTarget) : turretSpot.rotation;
        Shoot(rotationToTarget);
    }

}
