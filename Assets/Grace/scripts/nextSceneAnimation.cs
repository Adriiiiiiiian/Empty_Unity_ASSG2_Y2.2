/*
 * Author: Grace Foo
 * Date: 20/6/2023
 * Description: Code for the stamina bar
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextSceneAnimation : MonoBehaviour
{
    public static Animator transistion;
    public GameObject fadeUI;
    private static int levelToLoad;
    public GameObject portalMsg;


    /// <summary>
    /// to fade to the menu
    /// </summary>
    public void FadeToMenu()
    {
        levelToLoad = 0;
        transistion.SetTrigger("fadeTrigger");
    }
    /// <summary>
    /// to fade to scene 2
    /// </summary>
    public static void  FadeToLevel2()
    {
        Debug.Log("scene2");
        levelToLoad = 2;
        transistion.SetTrigger("fadeTrigger");
    }
    /// <summary>
    /// to fade to scene 1
    /// </summary>
    public static void FadeToLevel1()
    {
        levelToLoad = 1;
        transistion.SetTrigger("fadeTrigger");
    }
    /// <summary>
    /// to fade to scene 3
    /// </summary>
    public static void FadeToLevel3()
    {
        levelToLoad = 3;
        transistion.SetTrigger("fadeTrigger");
    }
    /// <summary>
    /// to fade to scene 4
    /// </summary>
    public static void FadeToLevel4()
    {
        levelToLoad = 4;
        transistion.SetTrigger("fadeTrigger");
    }
    /// <summary>
    /// to fade to level 5
    /// </summary>
    public static void FadeToLevel5()
    {
        levelToLoad = 5;
        transistion.SetTrigger("fadeTrigger");
    }
    /// <summary>
    /// to show portal message
    /// </summary>
    public void showPortalMsg()
    {
        portalMsg.gameObject.SetActive(true);
    }
    /// <summary>
    /// to load the scene
    /// </summary>
    /// <param name="levelIndex"></param>

    public void OnFadeComplete(int levelIndex)
    {
        SceneManager.LoadScene(levelToLoad);
        Debug.Log("fade ends");
    }

    // Start is called before the first frame update
    void Start()
    {
        transistion = (fadeUI).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
