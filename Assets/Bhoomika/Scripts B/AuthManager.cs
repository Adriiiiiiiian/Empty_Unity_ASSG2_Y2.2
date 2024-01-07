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
    public TMP_InputField addDisplayNameInput;
    public TMP_InputField fpEmailInput;
    //public TMP_InputField displayNameUpdateInput;

    //Buttons
    public GameObject signupBtn;
    public GameObject loginBtn;
    //public GameObject updateBtn;
    //public GameObject playGameBtn;
    public GameObject forgotPasswordBtn;

    //Text
    //public TMP_Text displayNameText;

    //Page
    public GameObject fPDonePage;
    public GameObject fPPage;

    //Initialize firebases
    private void Awake()
    {
        InitializeFirebase();
        mDatabaseref = FirebaseDatabase.DefaultInstance.RootReference;
    }

    //Initialize firebases
    void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    //Sign up new user function
    public void SignUpNewUser()
    {
        //User input converted to string 
        string signupemail = signupEmailInput.text.Trim();
        string signuppassword = signupPasswordInput.text.Trim();
        string displayname = addDisplayNameInput.text.Trim();
        Debug.Log("SignUpUser func working" + "displayname: " + displayname);

        //Firebase function to create account using email and password
        auth.CreateUserWithEmailAndPasswordAsync(signupemail, signuppassword).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                //To show error
                Debug.LogError("ERROR: " + task.Exception);
            }
            if (task.IsCompleted)
            {
                //Returns user
                FirebaseUser newPlayer = task.Result.User;

                //Create user details under 'players' in realtime database if there is user
                if (auth.CurrentUser != null)
                {
                    CreateUser(GetCurrentUser().UserId, newPlayer.Email, displayname);
                    //UpdatePlayerNickname(displayname);
                }//UpdateDisplayName();
                ShowDisplayName();
                Debug.LogFormat("Welcome back", newPlayer.UserId, newPlayer.Email);

                //Change scene 
                PlayGame();
            }
        });
    }

    //Creates user details using UUID in realtime database under 'players'
    public void CreateUser(string uuid, string signupemail, string DisplayName)
    {
        User player = new User(DisplayName, signupemail);
        Debug.Log("displayname in create user" + DisplayName + ": signupemail" + signupemail);
        string json = JsonUtility.ToJson(player);
        Debug.Log("json.. " + json);
        mDatabaseref.Child("players").Child(uuid).SetRawJsonValueAsync(json);//User
        //UpdatePlayerNickname(DisplayName);
        Debug.Log("Database");
    }

    //Returns user
    public FirebaseUser GetCurrentUser()
    {
        return auth.CurrentUser;
    }

    public string GetUserID()
    {
        if (auth.CurrentUser == null)
        {
            Debug.Log("current user is null");
        }
        return auth.CurrentUser.UserId;
    }

    public string GetCurrentUserDisplayName()
    {
        if (auth.CurrentUser != null)
        {
            return auth.CurrentUser.DisplayName;
        }
        else
        {
            Debug.LogWarning("No current user available");
            return "Unknown User";
        }
    }

    //Log in user function
    public void LogInNewUser()
    {
        //User input converted to string 
        string loginemail = loginEmailInput.text.Trim();
        string loginpassword = loginPasswordInput.text.Trim();
        Debug.Log("LogInUser func working");

        //Firebase function to create account using email and password
        auth.SignInWithEmailAndPasswordAsync(loginemail, loginpassword).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                //Returns user
                FirebaseUser currentPlayer = task.Result.User;
                ShowDisplayName();
                Debug.LogFormat("Welcome back", currentPlayer.UserId, currentPlayer.Email);

                //Change scene 
                PlayGame();
            }
        });
    }

    //Forget password function
    public void ForgetPassword()
    {
        //User input converted to string 
        string fpemail = fpEmailInput.text.Trim();

        //Firebase function to create account using email and password
        auth.SendPasswordResetEmailAsync(fpemail).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Password reset sent");
                fPDonePage.gameObject.SetActive(true);
                fPPage.gameObject.SetActive(false);
            }

        });
    }

    public void ShowDisplayName()
    {
        if (IsUserLoggedIn())
        {
            Debug.Log("worked");
            //displayNameText.text = GetUserProfile();
            //displayNameUpdateInput.text = auth.CurrentUser.DisplayName;
        }
    }

    /*public void UpdateDisplayName()
    {
        string updatename = displayNameUpdateInput.text.Trim();
        if(updatename == "")
        {
            updatename = auth.CurrentUser.UserId;
        }
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        Debug.Log("current user name is " + auth.CurrentUser.DisplayName);

        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = updatename
            };

            user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError("ERROR update displayname: " + task.Exception);
                }
                else if (task.IsCompleted)
                {
                    //
                    Debug.Log("user name is updated");
                    Debug.Log("new user name is : " + auth.CurrentUser.DisplayName);
                }
            });
        }
    }*/

    public string GetUserProfile()
    {
        return "Display Name: " + auth.CurrentUser.DisplayName;
    }

    public bool IsUserLoggedIn()
    {
        return auth.CurrentUser != null;
    }

    //Not used
    public void SignOutUser()
    {
        if (auth.CurrentUser != null)
        {
            auth.SignOut();

        }
    }

    //Change scene to main menu
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}