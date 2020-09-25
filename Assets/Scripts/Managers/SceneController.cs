using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Transform folderForBullets;
    [SerializeField] private Color shipRedColor;
    [SerializeField] private Color shipGreenColor;

    
    public Transform FolderForBullets => folderForBullets;
    public List<GameObject> shipsOnScene = new List<GameObject>();
    public Color RedColor => shipRedColor;
    public Color GreenColor => shipGreenColor;

}

public enum ColorSide
{
    red,
    green,
    neutral ,
}
