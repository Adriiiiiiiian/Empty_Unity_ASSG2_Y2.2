/*
 * Author: Bhoomika Manot
 * Date: 21/12/2023
 * Description: Authentication for Unity consisting of Sign up, Login and Forget password.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using TMPro;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class AuthManager : MonoBehaviour
{
    //Firebase ref
    public FirebaseAuth auth;

    //User inputs
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;
    public TMP_InputField signupEmailInput;
    public TMP_InputField signupPasswordInput;
    public TMP_InputField fpEmailInput;

    //Buttons
    public GameObject signupBtn;
    public GameObject loginBtn;
    public GameObject forgotPasswordBtn;

    private void Awake()
    {
        InitializeFirebase();
        Debug.Log("Firebase Initialize working");
    }

    void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SignUpNewUser()
    {
        string signupemail = signupEmailInput.text.Trim();
        string signuppassword = signupPasswordInput.text.Trim();
        Debug.Log("SignUpUser func working");

        auth.CreateUserWithEmailAndPasswordAsync(signupemail, signuppassword).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("ERROR: " + task.Exception);
            }
            if (task.IsCompleted)
            {
                FirebaseUser newPlayer = task.Result.User;
                Debug.LogFormat("Welcome back", newPlayer.UserId, newPlayer.Email);
            }
        });
    }

    public void LogInNewUser()
    {
        string loginemail = loginEmailInput.text.Trim();
        string loginpassword = loginPasswordInput.text.Trim();
        Debug.Log("LogInUser func working");

        auth.SignInWithEmailAndPasswordAsync(loginemail, loginpassword).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                FirebaseUser currentPlayer = task.Result.User;
                Debug.LogFormat("Welcome back", currentPlayer.UserId, currentPlayer.Email);
            }
        });
    }

    public void SignOutUser()
    {
        if (auth.CurrentUser != null)
        {
            auth.SignOut();
        }
    }

    public void ForgetPassword()
    {
        string fpemail = loginEmailInput.text.Trim();

        auth.SendPasswordResetEmailAsync(fpemail).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Password reset sent");
            }

        });
    }
}
