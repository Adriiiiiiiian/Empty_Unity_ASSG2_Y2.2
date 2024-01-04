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
    public BirdGameManager gameManager; // Reference to the GameManager
    public GameObject bird;

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bird")) // Check for collision with projectile
        {
            gameManager.BirdHit(); // Notify GameManager of bird hit
            Destroy(gameObject); // Destroy the bird
        }
    }*/

    public void DestroyBird()
    {
        gameManager.BirdHit(); // Notify GameManager of bird hit
        bird.gameObject.SetActive(false);
        //Destroy(gameObject); // Destroy the bird
    }
}
