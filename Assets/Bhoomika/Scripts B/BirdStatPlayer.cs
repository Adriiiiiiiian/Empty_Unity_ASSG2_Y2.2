/*
 * Author: Bhoomika & Grace  
 * Date: 2/1/2024
 * Description: handles all the firebase things
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdStatPlayer 
{
    public string displayname;
    public int score;

    public BirdStatPlayer()
    {
        //
    }

    public BirdStatPlayer(string DisplayName, int score)
    {
        this.displayname = displayname;
        this.score = score;
    }

    public string BirdStatPlayerToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
