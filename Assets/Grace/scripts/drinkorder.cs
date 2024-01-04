/*
 * Author: Grace Foo
 * Date: 18/12/2023
 * Description: this handles ordering the drinks
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class drinkorder : MonoBehaviour
{
    public TMP_InputField orderInput;
    public GameObject pengDrink;
    public GameObject orderWrong;
    public GameObject hotdrink;
    public GameObject correctUI;
    public GameObject correctButton;
    public GameObject previousStuff;
    public GameObject previousStuff1;

    public AudioSource wrongSound;
    public AudioSource rightSound;
    public void CheckOrder()
    {
        string userInput = orderInput.text.ToLower();
        if (userInput.Contains("teh") || userInput.Contains("kopi"))
        {
            if (userInput.Contains("peng"))
            {
                correctUI.SetActive(true);
                correctButton.SetActive(true);
                pengDrink.SetActive(true);
                rightSound.Play();
                previousStuff.SetActive(false);
                previousStuff1.SetActive(false);

            }
            else
            {
                correctUI.SetActive(true);
                correctButton.SetActive(true);
                hotdrink.SetActive(true);
                rightSound.Play();
                previousStuff.SetActive(false);
                previousStuff1.SetActive(false);
            }
        }
        else
        {
          orderWrong.SetActive(true);
          wrongSound.Play();
        }
    }
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
