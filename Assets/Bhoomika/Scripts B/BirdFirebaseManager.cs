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
    DatabaseReference dbStatRef;
    DatabaseReference dbLeaderboardRef;
    //DatabaseReference dbPlayerBase;
    Firebase.Auth.FirebaseAuth auth1;

    //public TMP_InputField newName;
    //public Button newBtnName;
    //public TMP_Text test;

    public void Awake()
    {
        InitializeFirebase();
        auth1 = FirebaseAuth.DefaultInstance;
    }

    public void InitializeFirebase()
    {
        //dbPlayerBase = FirebaseDatabase.DefaultInstance.GetReference("players");
        dbStatRef = FirebaseDatabase.DefaultInstance.GetReference("birdstats");
        dbLeaderboardRef = FirebaseDatabase.DefaultInstance.GetReference("leaderboardbird");
    }

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
                DataSnapshot ds = task.Result;
                if (ds.Child(uuid).Exists)
                {
                    p = JsonUtility.FromJson<BirdStatPlayer>(ds.Child(uuid).GetRawJsonValue());
                }

                //return sps;

            }
        });
        return p;
    }
    /// looks into the database checks if there is anything inside, if there isnt, add base data, if there is, if the highscore is larger than in the database, updates new score
    public void UpdatePlayerStats(string uuid, int score, string displayname)
    {

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
                //test.text = "Updated FB";
                Debug.Log(uuid + "before datasnapshot");
                DataSnapshot playerStats = task.Result;
                Debug.Log(uuid + "after datasnapshot");
                if (playerStats.Exists)
                {
                    //test.text = "Updated FB";
                    Debug.Log("exits???");
                    //updates if there is someting
                    BirdStatPlayer sp = JsonUtility.FromJson<BirdStatPlayer>(playerStats.GetRawJsonValue());
                    Debug.Log("scoreeee");
                    if (score > sp.score)
                    {
                        ///if the new score is higher than the score inside the database, it upates it
                        sp.score = score;
                        UpdatePlayerLeaderBoard(uuid, sp.score);
                        dbStatRef.Child(uuid).Child("score").SetValueAsync(score);
                        //dbPlayerBase.Child(uuid).Child("HighScore").SetValueAsync(score);
                        Debug.Log("helloooooo");

                    }
                    dbStatRef.Child(uuid).SetPriorityAsync(sp.BirdStatPlayerToJson());
                }
                else
                {
                    //test.text = "Updated FB";
                    Debug.Log("newww");
                    //creates new stats if there is no player inside
                    BirdStatPlayer sp = new BirdStatPlayer(displayname, score);
                    BirdLeaderboardStat lb = new BirdLeaderboardStat(displayname, score);//(displayName, score);

                    dbStatRef.Child(uuid).SetRawJsonValueAsync(sp.BirdStatPlayerToJson());
                    dbLeaderboardRef.Child(uuid).SetRawJsonValueAsync(lb.BirdLeaderboardStatToJson());
                }
            }
        });
    }
    /// <summary>
    ///updates new info into the leaderboard database
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="score"></param>
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
    /// <summary>
    /// changes the display name in the database
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="displayName"></param>
    public void NewPlayerName(string uuid, string displayname)
    {
        dbLeaderboardRef.Child(uuid).Child("displayname").SetValueAsync(displayname);
        dbStatRef.Child(uuid).Child("displayname").SetValueAsync(displayname);
        //dbPlayerBase.Child(uuid).Child("displayName").SetValueAsync(displayName);
    }

    /// <summary>
    /// removes the highscore in the database to 0
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="score"></param>
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
