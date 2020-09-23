using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : Turret
{
    
    private void Update()
    {
        Transform target = SearchTarget();

        if (target)
            currentTarget = target;
                
        if (currentTarget)
        {
            Quaternion rotationToTarget = GetRotationToTarget(currentTarget);
            Shoot(rotationToTarget);
        }
    }

}
