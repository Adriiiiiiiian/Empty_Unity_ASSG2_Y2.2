/*
 * Author: Grace Foo
 * Date: 15/12/2023
 * Description: this handles the music sounds
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundScript : MonoBehaviour
{
    public AudioSource wrongSound;
    public AudioSource rightSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void wrongPlay()
    {
        wrongSound.Play();
    }
    public void rightPlay()
    {
        rightSound.Play();
    }
}
