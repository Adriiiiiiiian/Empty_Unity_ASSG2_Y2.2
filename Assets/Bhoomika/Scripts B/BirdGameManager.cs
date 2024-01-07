/*
 * Author: Bhoomika & Grace  
 * Date: 3/1/2024
 * Description: handles all the firebase things (leaderboard & VR game)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;


public class BirdGameManager : MonoBehaviour
{
    //Database ref
    DatabaseReference mDatabaseref;

    //Refers to other scripts
    private AuthManager auth;
    private AuthManager authCode;
    private BirdFirebaseManager fireBaseMgr;

    //Declares bird
    public GameObject birdPrefab; 

    //Declares spawn area
    public BoxCollider spawnArea; 

    //Declares UI objs
    public TMP_Text scoreText; 
    public TMP_Text timerText; 
    public GameObject nextBtn;
    public GameObject hTP;

    //Declares time
    public float gameDuration = 10f;
    private float timer;

    //Declares score
    private int score = 0;
    private int HighScore;

    //Checks for game playing
    private bool gameRunning = false;

    //Initialize firebases
    private void Start()
    {
        mDatabaseref = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Update()
    {
        //Checks if game is running = true
        if (gameRunning)
        {
            //Changes time
            timer -= Time.deltaTime;

            //If timer hits 0
            if (timer <= 0)
            {
                //Time is 0
                timer = 0;

                //Game over
                gameRunning = false;

                //Updates player stat
                UpdatePlayerStat(this.HighScore);

                //Sets active button to eating scene
                nextBtn.gameObject.SetActive(true);
            }

            //Updates the UI texts
            scoreText.text = "Birds Hit: " + score.ToString();
            timerText.text = "Time Left: " + Mathf.Round(timer).ToString();
        }
    }

    //To be added OnClick() to start button
    public void StartGame()
    {
        //Remove UI page
        hTP.gameObject.SetActive(false);

        //Start score
        score = 0;

        //Display timer
        timer = gameDuration;

        //Set active texts
        scoreText.text = "Birds Hit: " + score.ToString();
        timerText.text = "Time Left: " + Mathf.Round(timer).ToString();

        //Start game
        gameRunning = true;

        //Start spawning birds at intervals
        InvokeRepeating("SpawnBird", 0f, 1.5f); 
    }

    //Spawn bird function
    void SpawnBird()
    {
        //Checks for game running = true
        if (gameRunning)
        {
            //Declare spawning within box collider
            Vector3 randomSpawnPosition = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                                                     Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                                                     Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));

            //Spawn birds
            Instantiate(birdPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }

    //Updates score from BirdController.cs
    public void BirdHit()
    {
        score++;
    }

    //Updates score into BirdFirebaseManager.cs to firebase
    public void UpdatePlayerStat(int HighScore)
    {
        //Get UUID & display name
        string userID = auth.GetUserID();
        string displayName = auth.GetCurrentUserDisplayName();

        //Score 
        int score = HighScore;

        //Declares score & updates firebase
        BirdFirebaseManager managerInstance = new BirdFirebaseManager();
        managerInstance.UpdatePlayerStats(auth.GetCurrentUser().UserId, score, auth.GetCurrentUserDisplayName());

        //Debug.Log($"UserID: {userID}, DisplayName: {displayName}, Score: {score.ToString()}");
    }
}
