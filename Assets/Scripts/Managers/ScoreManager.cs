using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int redKillsScore = 0;
    private int greenKillsScore = 0;

    public void IncreaseKills(ColorSide color)
    {
        if (color == ColorSide.red)
            redKillsScore += 1;
        else if(color == ColorSide.green)
            greenKillsScore += 1;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        MainManager.instance.uiController.UpdateKillsText(redKillsScore, greenKillsScore);
    }
}
