using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text playerHealthText;
    [SerializeField] private Text redKillsText;
    [SerializeField] private Text greenKillsText;


    public void UpdatePlayerHealth(int value)
    {
        playerHealthText.text = "Health: " + value;
    }

    public void UpdateKillsText(int redValue, int greenValue)
    {
        Debug.Log($"UpdateKillsText: {redValue}, {greenValue}");
        redKillsText.text = "Red kills: " + redValue;
        greenKillsText.text = "Green kills: " + greenValue;
    }
    
}
