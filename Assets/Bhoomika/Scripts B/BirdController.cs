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

    //Reference to the game manager
    private BirdGameManager gameManager;

    //Reference to the bird
    public GameObject bird;

    //To "shoo" bird
    public void DestroyBird()
    {
        //Find bird
        gameManager = GameObject.FindGameObjectWithTag("birdmanager").GetComponent<BirdGameManager>();

        //Notfiy game manager of bird hit, to ++ score
        gameManager.BirdHit(); 

        //Set inacitve to "successfully" shoo bird
        bird.SetActive(false);
    }
}

