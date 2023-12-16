/*
 * Author: Grace Foo
 * Date: 15/12/2023
 * Description: this handles the leaderboard ui
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Security.Cryptography;

public class LeaderboardManager : MonoBehaviour
{
    /// <summary>
    /// who ever is reading this for some reason, im waiting for the firebase codes to finish this because it will cause an error because they aint in here yet thanks
    /// </summary>
    //public SimpleFireBase fbManager;
    public GameObject rowPrefab;
    public GameObject rowPrefab1;
    public GameObject rowPrefab2;
    public Transform tableContent;

    /// <summary>
    /// get and update leaderboard UI
    /// </summary>
 
    // Start is called before the first frame update
    void Start()
    {
        //fbManager.GetLeaderBoard(3);
        //UpdateLeaderBoardUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
