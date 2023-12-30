/*
 * Author: Grace Foo
 * Date: 28/12/2023
 * Description: this handles tray return code
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trayscript : MonoBehaviour
{
    public AudioSource wrongSound;
    public AudioSource rightSound;

    public GameObject firstMsg;
    public GameObject oldtray;
    public GameObject newtray;
    public GameObject wrongMsg;
    public GameObject rightMsg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("nonhalal"))
        {
            firstMsg.SetActive(false);
            rightSound.Play();
            rightMsg.SetActive(true);
            wrongMsg.SetActive(false);


        }
        if (other.gameObject.CompareTag("halal"))
        {
            oldtray.SetActive(false);
            wrongSound.Play();
            newtray.SetActive(true);
            wrongMsg.SetActive(true);
            firstMsg.SetActive(false);

        }
    }

}
