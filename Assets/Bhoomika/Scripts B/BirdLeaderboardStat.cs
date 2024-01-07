/*
 * Author: Bhoomika & Grace  
 * Date: 2/1/2024
 * Description: handles all the firebase things
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdLeaderboardStat 
{
    //Declare variables
    public string displayname;
    public int score;

    public BirdLeaderboardStat()
    {
        //
    }

    //Declaration
    public BirdLeaderboardStat(string DisplayName, int score)
    {
        this.displayname = displayname;
        this.score = score;
    }

    //To Json
    public string BirdLeaderboardStatToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
