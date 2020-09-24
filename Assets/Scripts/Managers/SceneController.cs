using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Transform folderForBullets;
    public Transform FolderForBullets => folderForBullets;
    public List<GameObject> shipsOnScene = new List<GameObject>();
    
    
}

public enum ColorSide
{
    red,
    green,
}
