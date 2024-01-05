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
    private AuthManager auth;
    private AuthManager authCode;
    private BirdFirebaseManager fireBaseMgr;
    public AudioSource sound;

    DatabaseReference mDatabaseref;

    public GameObject birdPrefab; // Reference to the bird prefab
    public BoxCollider spawnArea; // Collider defining the spawn area
    public TMP_Text scoreText; // UI Text for displaying the score
    public TMP_Text timerText; // UI Text for displaying the score
    public float gameDuration = 10f; // Game duration in seconds
    //public TMP_Text test; 

    private int score = 0;
    private int HighScore;
    private float timer;
    private bool gameRunning = false;

    private void Start()
    {
        mDatabaseref = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Update()
    {
        if (gameRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                gameRunning = false;
                // Game over logic
                UpdatePlayerStat(this.HighScore);
                //test.text = "Updated FB";
            }

            scoreText.text = "Birds Hit: " + score.ToString();
            timerText.text = "Time Left: " + Mathf.Round(timer).ToString();
        }
    }

    public void StartGame()
    {
        score = 0;
        timer = gameDuration;
        scoreText.text = "Birds Hit: " + score.ToString();
        timerText.text = "Time Left: " + Mathf.Round(timer).ToString();
        gameRunning = true;

        InvokeRepeating("SpawnBird", 0f, 1.5f); // Start spawning birds at intervals
    }

    void SpawnBird()
    {
        if (gameRunning)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                                                     Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                                                     Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));

            Instantiate(birdPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }

    public void BirdHit()
    {
        score++;
    }

    public void UpdatePlayerStat(int HighScore)
    {

        string userID = auth.GetUserID();
        string displayName = auth.GetCurrentUserDisplayName();
        int score = HighScore;
        BirdFirebaseManager managerInstance = new BirdFirebaseManager();
        managerInstance.UpdatePlayerStats(auth.GetCurrentUser().UserId, score, auth.GetCurrentUserDisplayName());



        Debug.Log($"UserID: {userID}, DisplayName: {displayName}, Score: {score.ToString()}");
    }
}
