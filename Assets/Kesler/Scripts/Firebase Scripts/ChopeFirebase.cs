/*
 * Author: Grace Foo
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

public class ChopeFirebase : MonoBehaviour
{
    
    DatabaseReference dbPlayerRef;
    DatabaseReference dbLeaderboardRef;
    Firebase.Auth.FirebaseAuth auth1;

    public TMP_InputField newName;
    public Button newBtnName;

    public void Awake()
    {
        InitializeFirebase();
        auth1 = FirebaseAuth.DefaultInstance;
    }

    public void InitializeFirebase()
    {
        dbPlayerRef = FirebaseDatabase.DefaultInstance.GetReference("chopestats");
        dbLeaderboardRef = FirebaseDatabase.DefaultInstance.GetReference("leaderboardchope");
    }

    public async Task<ChopePlayerStat> GetPlayerStat(string uuid)
    {
        Query q = dbPlayerRef.Child(uuid).LimitToFirst(1);
        ChopePlayerStat p = null;
       await dbPlayerRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                return;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;
                if(ds.Child(uuid).Exists)
                {
                    p = JsonUtility.FromJson<ChopePlayerStat>(ds.Child(uuid).GetRawJsonValue());
                }

                //return sps;

            }
        });
        return p;
    }
    /// looks into the database checks if there is anything inside, if there isnt, add base data, if there is, if the highscore is larger than in the database, updates new score
    public void UpdatePlayerStats(string uuid, int score, string displayName)
    {
        
        Query playerQuery = dbPlayerRef.Child(uuid);
        Debug.Log("CHILD");
        Debug.Log(uuid);
        Debug.Log("User ID: " + auth1.CurrentUser.UserId);

        dbPlayerRef.Child(uuid).GetValueAsync().ContinueWithOnMainThread(task =>
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
                Debug.Log(uuid+"before datasnapshot");
                DataSnapshot playerStats = task.Result;
                Debug.Log(uuid + "after datasnapshot");
                if (playerStats.Exists)
                {
                    Debug.Log("exits???");
                    //updates if there is someting
                    ChopePlayerStat sp = JsonUtility.FromJson<ChopePlayerStat>(playerStats.GetRawJsonValue());
                    Debug.Log("scoreeee");
                    if( score > sp.score )
                    {
                        ///if the new score is higher than the score inside the database, it upates it
                        sp.score = score;
                        UpdatePlayerLeaderBoard(uuid, sp.score);
                        dbPlayerRef.Child(uuid).Child("score").SetValueAsync(score);
                       
                        Debug.Log("helloooooo");
                        
                    }
                    dbPlayerRef.Child(uuid).SetPriorityAsync(sp.ChopePlayerStatToJson());
                }
                else
                {
                    Debug.Log("newww");
                    //creates new stats if there is no player inside
                    ChopePlayerStat sp = new ChopePlayerStat(displayName, score);
                    ChopeLeaderboard lb = new ChopeLeaderboard(displayName, score);//(displayName, score);

                    dbPlayerRef.Child(uuid).SetRawJsonValueAsync(sp.ChopePlayerStatToJson());
                    dbLeaderboardRef.Child(uuid).SetRawJsonValueAsync(lb.ChopeLeaderboardToJson());
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
}

