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
    [SerializeField] Slider volumeSlider;
    public FirebaseAuth auth;
    public AudioSource sound;

    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
        auth = FirebaseAuth.DefaultInstance;
    }

    public void Intro()
    {
        SceneManager.LoadScene(2);
    }

    public void SignOutUser()
    {
        if (auth.CurrentUser != null)
        {
            auth.SignOut();
            SceneManager.LoadScene(0);
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
