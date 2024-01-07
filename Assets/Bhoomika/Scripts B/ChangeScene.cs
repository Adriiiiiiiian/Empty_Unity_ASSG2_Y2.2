/*
 * Author: Bhoomika Manot
 * Date: 21/12/2023
 * Description: Change scenes
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //Master script to change scenes in the game
    public void menu()
    {
        //Return back to main menu
        //Used in EndingScene (10)
        SceneManager.LoadScene(1);
    }

    public void Chope()
    {
        //Used to play chope table game
        //Used by IntroScene1 (2)
        SceneManager.LoadScene(3);
    }

    public void Trivia()
    {
        //Used to play trivia quiz
        //Used by ChopeGame (3)
        SceneManager.LoadScene(4);
    }

    public void Food()
    {
        //Used to order cai fan
        //Used by Trivia quiz scene (4)
        SceneManager.LoadScene(5);
    }

    public void Drink()
    {
        //Used to order drink
        //Used by AdrianScene (5)
        SceneManager.LoadScene(6);
    }

    public void Bird()
    {
        //Used to shoo bird
        //Used by Drink scene (6)
        SceneManager.LoadScene(7);
    }
    
    public void Eat()
    {
        //Used to display eating scene
        //Used by BirdGame (7)
        SceneManager.LoadScene(8);
    }

    public void Tray()
    {
        //Used to return tray
        //Used by Eat scene (8)
        SceneManager.LoadScene(9);
    }

    public void End()
    {
        //Ending scene
        //Used by TrayScene (9)
        SceneManager.LoadScene(10);
    }
}
