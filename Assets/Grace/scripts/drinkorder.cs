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
    public void CheckOrder()
    {
        string userInput = orderInput.text.ToLower();
        if (userInput.Contains("teh") || userInput.Contains("kopi"))
        {
            if (userInput.Contains("peng"))
            {
                pengDrink.SetActive(true);
            }
            else
            {
                hotdrink.SetActive(true);
            }
        }
        else
        {
          orderWrong.SetActive(true);
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
