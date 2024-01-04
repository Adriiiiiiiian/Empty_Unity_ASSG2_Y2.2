/*
 * Author: Grace Foo & Bhoomika 
 * Date: /1/2024
 * Description: handles all the firebase things (leaderboard & VR game)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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
    public GameObject birdPrefab; // Reference to the bird prefab
    public BoxCollider spawnArea; // Collider defining the spawn area
    public TMP_Text scoreText; // UI Text for displaying the score
    public float gameDuration = 10f; // Game duration in seconds

    private int birdsHit = 0;
    private float timer;
    private bool gameRunning = false;

    private void Start()
    {
        StartGame();
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
            }

            scoreText.text = "Birds Hit: " + birdsHit.ToString();
        }
    }

    public void StartGame()
    {
        birdsHit = 0;
        timer = gameDuration;
        scoreText.text = "Birds Hit: " + birdsHit.ToString();
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
        birdsHit++;
    }
}
