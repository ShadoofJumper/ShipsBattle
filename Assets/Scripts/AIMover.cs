using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AIMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float angularSpeed = 90.0f;
    [SerializeField] private float searchRange = 5.0f;
    [SerializeField] private float searchAngle = 45.0f;
    private ColorSide parentShipColorSide;

    private Transform target;
    private float angleNeedApply = 0f;
    private void Start()
    {
        parentShipColorSide = gameObject.GetComponent<Combat>().ColorSide;
    }

    private void FixedUpdate()
    {
        SearchForTarget();
        RotateShip();
        MoveShip();
    }

    private void SearchForTarget()
    {
        //reset target
        target = GetClosestTarget();

        if (target)
        {
            float angleToTarget = GetAngleToTarget(target.transform);
            //check is enemy in angle of attack, else save as 0
            angleNeedApply = Math.Abs(angleToTarget) < searchAngle/2 ? angleToTarget : 0;
        }

    }

    private Transform GetClosestTarget()
    {
        Transform targetTemp = null;
        //get all target in range
        List<GameObject> allShips = MainManager.instance.sceneController.shipsOnScene;
        //find closest target in range
        float distanceToTarget = 0.0f;
        foreach (var shipObject in allShips)
        {
            if (IsTargetIsEnemy(shipObject))
            {
                float distance = DistanceToTarget(shipObject.transform);
                if (distance < searchRange)
                {
                    //if already have target, check is this target closer
                    if (targetTemp)
                    {
                        if (distance < distanceToTarget)
                        {
                            targetTemp = shipObject.transform;
                            distanceToTarget = distance;
                        }
                    }
                    else
                    {
                        targetTemp = shipObject.transform;
                        distanceToTarget = distance; 
                    }
                }
            }
        }

        return targetTemp;
    }

    private float DistanceToTarget(Transform target)
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    private float GetAngleToTarget(Transform target)
    {
        Vector3 directionToTarget = target.position - transform.position;
        return Vector3.SignedAngle(directionToTarget, transform.right, transform.forward);
    }

    private bool IsTargetIsEnemy(GameObject shipObject)
    {
        return shipObject.GetComponent<Combat>().ColorSide != parentShipColorSide;
    }
    
    
    private void RotateShip()
    {
        float directionToRotate = 1;
        //if have angle to apply
        if (Math.Abs(angleNeedApply) > 0.0f)
            directionToRotate = angleNeedApply/angleNeedApply * -1;
        
        //Debug.Log($"angleNeedApply: {angleNeedApply}, directionToRotate{directionToRotate}");
        Vector3 changeStep = Vector3.forward * angularSpeed * Time.deltaTime * directionToRotate;
        transform.eulerAngles += changeStep;
    }

    private void MoveShip()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;
    }

   //private void OnDrawGizmos()
   //{
   //    Gizmos.color = Color.yellow;
   //    Gizmos.DrawWireSphere(transform.position, searchRange);
   //}
}
