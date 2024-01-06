using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopeLeaderboard 
{
    // Start is called before the first frame update

    public string DisplayName;
    public int score;

    public ChopeLeaderboard()
    {

    }

    public ChopeLeaderboard(string DisplayName, int score)
    {
        this.DisplayName = DisplayName;
        this.score = score;
    }

    public string ChopeLeaderboardToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
