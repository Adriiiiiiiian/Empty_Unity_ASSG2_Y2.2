/*
 * Author: Bhoomika 
 * Date: 2/1/2024
 * Description: Handles audio sound
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    //Declare variables
    [SerializeField] Slider volumeSlider;
    public AudioSource sound;

    //Declare auth
    public FirebaseAuth auth;

    void Start()
    {
        //Check if slider has been touched before
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            //Keep volume on slider 
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            //Start at highest volume
            Load();
        }

        //Initialize firebase
        auth = FirebaseAuth.DefaultInstance;
    }

    //Change volume according to slider value from 0 to 1
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;

        //Save volume via function
        Save();
    }

    //Using Get Set to Get currrent volume
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    //Using Get Set to Set currrent volume
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    //Change to game scene OnClick() on Play btn
    public void Intro()
    {
        SceneManager.LoadScene(2);
    }

    //Sign out function
    public void SignOutUser()
    {
        //Checks if user is not null
        if (auth.CurrentUser != null)
        {
            //Sign out from Auth
            auth.SignOut();

            //Returns to Login Scene
            SceneManager.LoadScene(0);
        }
    }
}
