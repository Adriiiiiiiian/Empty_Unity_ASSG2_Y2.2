    /*
 * Author: Kesler
 * Date: 1/5/2024
 * Description: Manages tissue collision and increases score when a tissue object collides with the tables/chairs.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TissueCollider : MonoBehaviour
{   
    public bool isUsed = false;
    public GameObject chopeGameManager; // Reference to ChopeGameManager
    public ChopeGameManager chopeGameMngr;

    public AudioSource tissuePlacedSound;
    public AudioSource tissueUsedSound;

    void Start()
    {   
        tissuePlacedSound = GetComponent<AudioSource>();
        chopeGameManager =  GameObject.Find("ChopeGameManager");
        chopeGameMngr = chopeGameManager.GetComponent<ChopeGameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {   
        Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);

        if ((isUsed == false) && (collision.gameObject.tag == "Table"))
        {   
            tissuePlacedSound.Play();
            isUsed = true;
            Debug.Log(gameObject.name + " collided with table.");
            chopeGameMngr.IncreaseScore(); // Add score when tissue is placed on table
            //chopeGameManager.IncreaseScore(); // Add score when tissue is placed on table
        }   

        else if (isUsed == true)
        {   
            tissueUsedSound.Play();
            Debug.Log(gameObject.name + " already used.");
        }
    }

}
