/*
 * Author: Bhoomika Manot
 * Date: 21/12/2023
 * Description: Change scene
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Chope()
    {
        SceneManager.LoadScene(3);
    }

    public void Trivia()
    {
        SceneManager.LoadScene(4);
    }

    public void Food()
    {
        SceneManager.LoadScene(5);
    }

    public void Drink()
    {
        SceneManager.LoadScene(6);
    }

    public void Bird()
    {
        SceneManager.LoadScene(7);
    }
    
    public void Eat()
    {
        SceneManager.LoadScene(8);
    }

    public void Tray()
    {
        SceneManager.LoadScene(9);
    }

    public void End()
    {
        SceneManager.LoadScene(10);
    }
}
