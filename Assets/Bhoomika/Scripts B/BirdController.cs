/*
 * Author: Bhoomika 
 * Date: 3/1/2024
 * Description: Leaderboard & VR game (bird)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    //private GameObject birdScriptHolder;
    private BirdGameManager gameManager; // Reference to the GameManager
    public GameObject bird;

    
    public void DestroyBird()
    {
        gameManager = GameObject.FindGameObjectWithTag("birdmanager").GetComponent<BirdGameManager>();

            gameManager.BirdHit(); // Notify GameManager of bird hit

        bird.SetActive(false);
        // Alternatively, if you want to destroy the bird
        // Destroy(bird);
    }
}

