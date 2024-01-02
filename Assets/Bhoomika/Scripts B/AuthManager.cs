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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    //Firebase ref
    public FirebaseAuth auth;
    DatabaseReference mDatabaseref;

    //User inputs
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;
    public TMP_InputField signupEmailInput;
    public TMP_InputField signupPasswordInput;
    public TMP_InputField fpEmailInput;
    public TMP_InputField displayNameUpdateInput;

    //Buttons
    public GameObject signupBtn;
    public GameObject loginBtn;
    public GameObject updateBtn;
    public GameObject playGameBtn;
    public GameObject forgotPasswordBtn;

    //Text
    public TMP_Text displayNameText;

    private void Awake()
    {
        InitializeFirebase();
        mDatabaseref = FirebaseDatabase.DefaultInstance.RootReference;
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
                UpdateDisplayName(newPlayer.UserId);
                ShowDisplayName();
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
                ShowDisplayName();
                Debug.LogFormat("Welcome back", currentPlayer.UserId, currentPlayer.Email);
            }
        });
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

    public void ShowDisplayName()
    {
        if (IsUserLoggedIn())
        {
            displayNameText.text = GetUserProfile();
            displayNameUpdateInput.text = auth.CurrentUser.DisplayName;
        }
    }

    public void UpdateDisplayName(string newDisplayName)
    {
        string updatename = displayNameUpdateInput.text.Trim();
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;

        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = newDisplayName
            };

            user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {

                }
                else if (task.IsCompleted)
                {
                    //
                }
            });
        }
    }

    public string GetUserProfile()
    {
        return "Display Name: " + auth.CurrentUser.DisplayName;
    }

    public bool IsUserLoggedIn()
    {
        return auth.CurrentUser != null;
    }

    public void SignOutUser()
    {
        if (auth.CurrentUser != null)
        {
            auth.SignOut();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
