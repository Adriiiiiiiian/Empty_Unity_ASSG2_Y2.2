/*
 * Author: Kesler
 * Date: 1/5/2024
 * Description: Manages starting and ending game, game's user interface and manages player's score. 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using TMPro;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;

public class ChopeGameManager : MonoBehaviour
{   
    public AudioSource DINGDINGDINGG;
    public GameObject tissueButton;
    public GameObject tissuePlatform;
    public GameObject UnityCanvas;
    public GameObject Canvas;
    public GameObject dialogue3;
    public GameObject GameOverUI;
    public GameObject btn4;
    
    int chopeScore = 0;
    int finalScore = 0;
    public float countdown = 60.0f;
    public TextMeshProUGUI chopeScoreText; 
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI timeRemainingText;
    public AuthManager auth;

    //bool isCountdown = false;
    public bool isGamePlaying = false;

    public ChopeFirebase chopeFirebaseManager;

    void Start()
    {
        DINGDINGDINGG = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   
        //Timer Countdown
        if (isGamePlaying == true)
        {   
            timerText.text = "Time Remaining: " + countdown.ToString("F2") + 's';

                if (countdown <= 0)
                {   
                    EndGame();
                }
        }

    }

    public void ActivateGameObjects()
    {
        tissueButton.SetActive(true);
        tissuePlatform.SetActive(true);
        UnityCanvas.SetActive(false);
        //Canvas.SetActive(false);
    }

    public void DeactivateGameObjects()
    {
        tissueButton.SetActive(false);
        tissuePlatform.SetActive(false);
        UnityCanvas.SetActive(true);
        btn4.SetActive(true);
        Canvas.SetActive(true);
        dialogue3.SetActive(false);
        GameOverUI.SetActive(true);
    }
    
    public void IncreaseScore()
    {   
        Debug.Log("Increased score.");
        chopeScore++;
        chopeScoreText.text = "Target Score: " + chopeScore + "/3";

        Debug.Log("Current Score: " + chopeScore + "/3");

        //Check if the maximum score has reached 3
        if (chopeScore == 3)
        {
            EndGame();
            Debug.Log("Chope score is 3. Ending game.");
        }
    }

    private void StartGame() //Start game using raycast
    {
        isGamePlaying = true;

        //Set game objects to active
        ActivateGameObjects();
    }

    public void StartCountdown()
    {
        if (!isGamePlaying)
        {
            isGamePlaying = true;
            StartCoroutine(CountdownTimer());
        }
    }

    private void EndGame()
    {   
        DINGDINGDINGG.Play();
        isGamePlaying = false;

        // Log the chope score and time remaining
        Debug.Log("Time Remaining: " + countdown);
        Debug.Log("Chope Score: " + chopeScore);

        if (countdown <= 1)
        {   
            finalScore = chopeScore * 10;
            Debug.Log("if");
        }
        
        else 
        {
            finalScore = Mathf.RoundToInt(chopeScore * countdown * 10);
            Debug.Log("else");
        }
        Debug.Log("Final Score: " + finalScore);

        // Display final score
        finalScoreText.text = "Total Score: " + finalScore.ToString("F2");

        // Display remaining time
        timeRemainingText.text = "Time Remaining: " + countdown.ToString("F2") + "s";
        
        //countdown = 0;
        //Debug.Log("countdown set to 0");

        //Set game over UI to true and game objects to false
        DeactivateGameObjects();
        UpdatePlayerStat(finalScore);

    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UpdatePlayerStat(int finalScore)
    {

        string userID = auth.GetUserID();
        string displayName = auth.GetCurrentUserDisplayName();
        int score = finalScore;

        chopeFirebaseManager.UpdatePlayerStats(auth.GetCurrentUser().UserId,score, auth.GetCurrentUserDisplayName());

        Debug.Log($"UserID: {userID}, DisplayName: {displayName}, Score: {score.ToString()}");
    }

    private IEnumerator CountdownTimer()
{
    while (countdown > 0)
    {
        countdown -= Time.deltaTime;
        countdown = Mathf.Max(countdown, 0f);
        timerText.text = "Time Remaining: " + countdown.ToString("F2") + 's';

        //Debug.Log("Countdown: " + countdown.ToString("F2") + 's');

        yield return null;
    }

    isGamePlaying = false;

}

}
