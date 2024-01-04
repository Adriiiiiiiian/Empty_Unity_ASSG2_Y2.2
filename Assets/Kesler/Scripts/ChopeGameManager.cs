using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChopeGameManager : MonoBehaviour
{   
    public int chopeScore = 0;
    public float countdown = 60.0f;
    public TextMeshProUGUI chopeScoreText; 
    public TextMeshProUGUI timerText; 

    // Update is called once per frame
    void Update()
    {   
        //Timer Countdown
        countdown -= Time.deltaTime;
        timerText.text = "Time: " + countdown.ToString("F2");

        if (countdown <= 0)
        {
            //Set game over UI to true
        }

    }

    public void IncreaseScore()
    {
        chopeScore++;
        chopeScoreText.text = "Score: " + chopeScore;
    }
}
