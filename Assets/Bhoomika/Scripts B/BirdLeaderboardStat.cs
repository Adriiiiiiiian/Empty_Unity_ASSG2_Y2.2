using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdLeaderboardStat 
{

    public string displayname;
    public int score;

    public BirdLeaderboardStat()
    {
        //
    }

    public BirdLeaderboardStat(string DisplayName, int score)
    {
        this.displayname = displayname;
        this.score = score;
    }

    public string BirdLeaderboardStatToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
