/*
 * Author: Bhoomika Manot
 * Date: 24/12/2023
 * Description: Bird game consisting of UI, Bird game via grab, and scores registered to database
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BirdGame : MonoBehaviour
{
    public GameObject birdSpawn; // Bird prefab to be spawned
    public BoxCollider spawnArea; // Collider defining spawn area
    //public Text scoreText; // UI Text for displaying score
    public Text timerText; // UI Text for displaying countdown

    private int destroyedObjects = 0;
    private float timer = 10f;
    private bool gameRunning = false;

    void Start()
    {
        InvokeRepeating("SpawnObject", 0f, 1.5f); // Spawns objects at intervals
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
            }

            timerText.text = "Time Left: " + Mathf.Round(timer).ToString();
        }
    }

    void SpawnObject()
    {
        if (!gameRunning)
        {
            return;
        }

        Vector3 randomSpawnPosition = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                                                 Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                                                 Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));

        Instantiate(birdSpawn, randomSpawnPosition, Quaternion.identity);
    }

    public void ObjectDestroyed()
    {
        destroyedObjects++;
        //scoreText.text = "Destroyed: " + destroyedObjects.ToString();
    }

    public void StartGame()
    {
        gameRunning = true;
    }
}
