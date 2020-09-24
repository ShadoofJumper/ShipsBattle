using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private void Start()
    {
        MainManager.instance.sceneController.shipsOnScene.Add(gameObject);
    }

    private void OnDestroy()
    {
        MainManager.instance.sceneController.shipsOnScene.Remove(gameObject);
    }
}
