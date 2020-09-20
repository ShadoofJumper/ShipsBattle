using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] protected ShipSide shipType;
    public ShipSide ShipType => shipType;
}
