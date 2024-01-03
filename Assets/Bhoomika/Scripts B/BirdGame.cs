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
    public GameObject objectToSpawn; // The object you want to spawn
    public BoxCollider spawnArea; // Collider defining the spawn area
    public TMP_Text countdownText; // Text UI element for countdown

    private bool canSpawn = true;
    private float timer = 10f;

    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(SpawnObjectRandomly());
            canSpawn = false;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            countdownText.text = "Time Left: " + Mathf.Round(timer);
        }
        else
        {
            countdownText.text = "Time's up!";
        }
    }

    IEnumerator SpawnObjectRandomly()
    {
        while (timer > 0)
        {
            float randomX = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            float randomY = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            float randomZ = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f)); // Random time delay between spawns
        }
    }
}
