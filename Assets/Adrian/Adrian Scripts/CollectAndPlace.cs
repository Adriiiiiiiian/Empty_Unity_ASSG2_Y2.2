using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectAndPlace : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject snap1;
    public GameObject square;
    public GameObject door;


    public GameObject chicken;
    public bool chickenbool;
    public TextMeshProUGUI chickenwords;

    public GameObject sauce;
    public bool saucebool;
    public TextMeshProUGUI saucewords;

    public GameObject veggies;
    public bool veggiesbool;
    public TextMeshProUGUI veggieswords;

    public GameObject rice;
    public bool ricebool;

    public GameObject nextui;
    public GameObject playminigame;
    public GameObject drinkui;

    public GameObject meal;

    public TextMeshProUGUI gameinstructions;
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
    public void ChangeWords()
    {
        if (ricebool == false)
        {
            chickenwords.text = "Rice First!";
            veggieswords.text = "Rice First!";
            saucewords.text = "Rice First!";
        }

        else if (ricebool == true)
        {
            chickenwords.text = "Chicken";
            veggieswords.text = "Mixed Vegetables";
            saucewords.text = "Sauce";

        }
                    
    }
    public void PlayGame()
    {
        nextui.SetActive(false);
        playminigame.SetActive(true);
            
    }

    public void ShowNextUI()
    {
        if (ricebool == true && chickenbool == true && veggiesbool == true && saucebool == true)
        {
            drinkui.SetActive(true);
            playminigame.SetActive(false);

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CollectGame();

        ChangeWords();

        if (ricebool == true)
        {
            gameinstructions.text = "Good! Now Continue to add the rest of the ingredients in the same way.";
        }

        if (ricebool == true && chickenbool == true && veggiesbool == true && saucebool == true)
        {
            gameinstructions.text = "Now click on your finished meal to head to the next section.";

        }

    }
}
