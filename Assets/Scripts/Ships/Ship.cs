using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destroyable))]
public class Ship : MonoBehaviour
{
    [SerializeField] private SpriteRenderer shipSprite;
    private Destroyable shipDestroyable;
    private ColorSide shipColorSide;
    
    private void Start()
    {
        shipDestroyable = GetComponent<Destroyable>();
        shipColorSide = shipDestroyable.ColorSide;
        MainManager.instance.sceneController.shipsOnScene.Add(gameObject);
        UpdateColorForShip();
    }

    private void UpdateColorForShip()
    {
        Color red = MainManager.instance.sceneController.RedColor;
        Color green = MainManager.instance.sceneController.GreenColor;
        shipSprite.color = shipColorSide == ColorSide.red ? red : green;
    }

    private void OnDestroy()
    {
        MainManager.instance.sceneController.shipsOnScene.Remove(gameObject);
    }
}
