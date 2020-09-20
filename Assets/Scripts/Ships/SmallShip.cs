using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallShip : Ship
{
    [SerializeField] private bool isPlayer;

    
    public void Init(bool isPlayer, ShipSide shipType)
    {
        this.isPlayer = isPlayer;
        this.shipType = shipType;
    }
    
}
