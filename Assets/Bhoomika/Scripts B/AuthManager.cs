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

    //Page
    public GameObject fpPage;
    public GameObject fpSuccessPage;

    //Audio
    public AudioSource sound;

    //Text
    //public TMP_Text displayNameText;
    public TMP_Text signupErrorText;
    public TMP_Text loginErrorText;
    public TMP_Text fpErrorText;

    //Start firebases
    private void Awake()
    {
        InitializeFirebase();
        mDatabaseref = FirebaseDatabase.DefaultInstance.RootReference;
        
    }

    //Start firebases
    void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    //Sign up user
    public void SignUpNewUser()
    {
        //Change input to string
        string signupemail = signupEmailInput.text.Trim();
        string signuppassword = signupPasswordInput.text.Trim();
        string displayname = addDisplayNameInput.text.Trim();
        //displayNameText.text = "signing up user.";
        //Debug.Log("SignUpUser func working" + "displayname: " + displayname);

        //Sign up user with firebase func
        auth.CreateUserWithEmailAndPasswordAsync(signupemail, signuppassword).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("ERROR: " + task.Exception);
                //displayNameText.text = "error in signing up user.";

                //Alerts user that signup failed via text active on screen
                signupErrorText.gameObject.SetActive(true);
            }
            if (task.IsCompleted)
            {
                //displayNameText.text = "completed signing up user.";

                //Returns user
                FirebaseUser newPlayer = task.Result.User;
                if (auth.CurrentUser != null)
                {
                    //Creates user in realtime database under "players", using string values inputed
                    CreateUser(GetCurrentUser().UserId, newPlayer.Email, displayname);
                    //UpdatePlayerNickname(displayname);
                }
                //UpdateDisplayName();
                ShowDisplayName();

                //Change scene to main menu
                PlayGame();
                Debug.LogFormat("Welcome back", newPlayer.UserId, newPlayer.Email);


            }
        });
    }

    //Create user in realtime database code
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

    //Gets UUID
    public string GetUserID()
    {
        if (auth.CurrentUser == null)
        {
            Debug.Log("current user is null");
        }
        return auth.CurrentUser.UserId;
    }

    //Gets auth display name
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

    //Logs in user
    public void LogInNewUser()
    {
        //Changes the input to string
        string loginemail = loginEmailInput.text.Trim();
        string loginpassword = loginPasswordInput.text.Trim();
        Debug.Log("LogInUser func working");

        //Users firebase func to sign in
        auth.SignInWithEmailAndPasswordAsync(loginemail, loginpassword).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("ERROR: " + task.Exception);

                //Alerts user that signup failed via text active on screen
                loginErrorText.gameObject.SetActive(true);
            }
            else if (task.IsCompleted)
            {
                //Returns user
                FirebaseUser currentPlayer = task.Result.User;
                ShowDisplayName();

                //Change scene to main menu
                PlayGame();
                Debug.LogFormat("Welcome back", currentPlayer.UserId, currentPlayer.Email);
            }
        });
    }

    //Forget password func
    public void ForgetPassword()
    {
        //Change input to string
        string fpemail = loginEmailInput.text.Trim();

        //Uses firebase function
        auth.SendPasswordResetEmailAsync(fpemail).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("ERROR: " + task.Exception);

                //Alerts user that signup failed via text active on screen
                fpErrorText.gameObject.SetActive(true);
            }
            else if (task.IsCompleted)
            {
                //Successful
                Debug.Log("Password reset sent");

                //Change pages if successful to alert user that successful
                fpPage.gameObject.SetActive(false);
                fpSuccessPage.gameObject.SetActive(true);
            }

        });
    }

    public void ShowDisplayName()
    {
        if (IsUserLoggedIn())
        {
            //Debug.Log("worked");
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

    //Checks for user
    public bool IsUserLoggedIn()
    {
        return auth.CurrentUser != null;
    }

    //Sign out (not used in the scene)
    public void SignOutUser()
    {
        if (auth.CurrentUser != null)
        {
            auth.SignOut();
        }
    }

    //Play audio
    public void PlaySound()
    {
        sound.Play();
    }

    //Change scene upon login/signup
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}