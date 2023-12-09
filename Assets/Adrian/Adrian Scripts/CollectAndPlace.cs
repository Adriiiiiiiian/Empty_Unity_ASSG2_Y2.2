using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAndPlace : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject snap1;
    public GameObject square;
    public GameObject door;


    public GameObject chicken;
    public bool chickenbool;

    public GameObject sauce;
    public bool saucebool;

    public GameObject veggies;
    public bool veggiesbool;

    public GameObject rice;
    public bool ricebool;



    public int count;

    public AudioSource audSource;

    public void CollectGame()
    {
        if (square.transform.position == snap1.transform.position)
        {
            Debug.Log("DOOR OPEN");
            door.SetActive(false);
        }
    }

    public void AddRice()
    {
        rice.SetActive(true);
        ricebool = true;

    }

    public void AddChicken()
    {
        if (ricebool == true)
        {
            chicken.SetActive(true);
            chickenbool = true;

        }
        

    }
    public void AddVeg()
    {
        if (ricebool == true)
        {
            veggies.SetActive(true);
            veggiesbool = true;

        }


    }

    public void AddSauce()
    {
        if (ricebool == true)
        {
            sauce.SetActive(true);
            saucebool = true;

           



        }


    }

    public void ResetFood()
    {
        rice.SetActive(false);
        ricebool = false;

        chicken.SetActive(false);
        chickenbool = false;

        veggies.SetActive(false);
        veggiesbool = false;

        sauce.SetActive(false);
        saucebool = false;



    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CollectGame();


    }
}
