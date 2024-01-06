using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopePlayerStat 
{
    // Start is called before the first frame update

    public string DisplayName;
    public int score;

    public ChopePlayerStat()
    {

    }

    public ChopePlayerStat(string DisplayName, int score)
    {
        this.DisplayName = DisplayName;
        this.score = score;
    }

    public string ChopePlayerStatToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
