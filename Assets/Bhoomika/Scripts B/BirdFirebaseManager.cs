/*
 * Author: Bhoomika & Grace  
 * Date: 2/11/2023
 * Description: handles all the firebase things
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

public class BirdFirebaseManager : MonoBehaviour
{
    //Database ref
    DatabaseReference dbStatRef;
    DatabaseReference dbLeaderboardRef;
    Firebase.Auth.FirebaseAuth auth1;

    //Initialize firebases
    public void Awake()
    {
        InitializeFirebase();
        auth1 = FirebaseAuth.DefaultInstance;
    }

    //Initialize firebases
    public void InitializeFirebase()
    {
        //dbPlayerBase = FirebaseDatabase.DefaultInstance.GetReference("players");
        dbStatRef = FirebaseDatabase.DefaultInstance.GetReference("birdstats");
        dbLeaderboardRef = FirebaseDatabase.DefaultInstance.GetReference("leaderboardbird");
    }

    //Get player details
    public async Task<BirdStatPlayer> GetPlayerStat(string uuid)
    {
        Query q = dbStatRef.Child(uuid).LimitToFirst(1);
        BirdStatPlayer p = null;
        await dbStatRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                return;
            }
            else if (task.IsCompleted)
            {
                //Recieve data
                DataSnapshot ds = task.Result;

                //Returns player
                if (ds.Child(uuid).Exists)
                {
                    p = JsonUtility.FromJson<BirdStatPlayer>(ds.Child(uuid).GetRawJsonValue());
                }

                //return sps;

            }
        });
        return p;
    }

    // Checks database for information, and if there is no data, adds data inside
    // If have, updates if highscore is more that current score
    public void UpdatePlayerStats(string uuid, int score, string displayname)
    {
        //Refers to db child
        Query playerQuery = dbStatRef.Child(uuid);
        Debug.Log("CHILD");
        Debug.Log(uuid);
        Debug.Log("User ID: " + auth1.CurrentUser.UserId);

        dbStatRef.Child(uuid).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            Debug.Log("pass");
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("error in saving playerStatLeaderboarddata");
                return;
            }
            else if (task.Exception != null)
            {
                Debug.LogError("Error: " + task.Exception.ToString());
                return;
            }
            else if (task.IsCompleted)
            {
                //Recieves data
                //test.text = "Updated FB";
                DataSnapshot playerStats = task.Result;

                //Check for player stats
                if (playerStats.Exists)
                {
                    //test.text = "Updated FB";

                    //Refers to player stat
                    BirdStatPlayer sp = JsonUtility.FromJson<BirdStatPlayer>(playerStats.GetRawJsonValue());

                    //Compares current score to highest score from players past data
                    if (score > sp.score)
                    {
                        //If the new score is higher than the score inside the database, it updates score
                        sp.score = score;
                        UpdatePlayerLeaderBoard(uuid, sp.score);
                        dbStatRef.Child(uuid).Child("score").SetValueAsync(score);
                    }

                    //Updates to database
                    dbStatRef.Child(uuid).SetPriorityAsync(sp.BirdStatPlayerToJson());
                }
                else
                {
                    //test.text = "Updated FB";

                    //Creates new stats if there is no data is inside
                    BirdStatPlayer sp = new BirdStatPlayer(displayname, score);
                    BirdLeaderboardStat lb = new BirdLeaderboardStat(displayname, score);

                    //Updates database
                    dbStatRef.Child(uuid).SetRawJsonValueAsync(sp.BirdStatPlayerToJson());
                    dbLeaderboardRef.Child(uuid).SetRawJsonValueAsync(lb.BirdLeaderboardStatToJson());
                }
            }
        });
    }

    //Updates info to leaderboard database
    public void UpdatePlayerLeaderBoard(string uuid, int score)
    {
        dbLeaderboardRef.Child(uuid).Child("score").SetValueAsync(score);
    }

    /// <summary>
    /// this makes a list, if there is something inside, it loops through, sorting it by score, adding it to a list, from small to big and reversing it
    /// </summary>
    /// <param name="limit"></param>
    /// <returns></returns>
    /*public async Task<List<BirdLeaderboardStat>> GetLeaderBoard(int limit = 3)
    {
        Query q = dbLeaderboardRef.OrderByChild("score").LimitToLast(limit);
        List<BirdLeaderboardStat> leaderBoardList = new List<BirdLeaderboardStat>();

        await q.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.Log("Sorry, issue getting leaderboard");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;
                if (ds.Exists)// && ds.HasChildren)
                {
                    int rankCounter = 1;
                    foreach (DataSnapshot d in ds.Children)
                    {
                        BirdLeaderboardStat lb = JsonUtility.FromJson<BirdLeaderboardStat>(d.GetRawJsonValue());
                        leaderBoardList.Add(lb);
                        Debug.Log("Leaderboard");
                    }

                    leaderBoardList.Reverse();
                    foreach (BirdLeaderboardStat lb in leaderBoardList)
                    {
                        Debug.LogFormat("Leaderboard: rank {0} PlayerName {1} Score {2}", rankCounter, lb.displayname, lb.score);
                        rankCounter++;
                    }
                }
            }
        });

        return leaderBoardList;
    }*/

    // Changes display name in database
    public void NewPlayerName(string uuid, string displayname)
    {
        dbLeaderboardRef.Child(uuid).Child("displayname").SetValueAsync(displayname);
        dbStatRef.Child(uuid).Child("displayname").SetValueAsync(displayname);
        //dbPlayerBase.Child(uuid).Child("displayName").SetValueAsync(displayName);
    }

    //Removes score (changes score to 0)
    public void DeletePlayerStats(string uuid, int score)
    {
        Debug.Log(uuid);
        dbLeaderboardRef.Child(uuid).Child("score").SetValueAsync(score);
        dbStatRef.Child(uuid).Child("score").SetValueAsync(score);
        //dbPlayerBase.Child(uuid).Child("HighScore").SetValueAsync(score);
        //dbPlayerRef.Child(uuid).Child("score").RemoveValueAsync();
        //dbLeaderboardRef.Child(uuid).Child("score").RemoveValueAsync();
    }
}
