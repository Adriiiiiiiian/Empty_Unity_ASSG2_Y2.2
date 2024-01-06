using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{   
    public ChopeGameManager chopeGameManager;     
    public GameObject tissuePrefab;
    public Transform spawnPoint; 
    private int tissueCount = 0; // Counter for the number of tissues spawned
    private const int maxTissues = 3; // Maximum number of tissues allowed

    public AudioSource btn_sound;

    void Start()
    {   
        btn_sound = GetComponent<AudioSource>();
        //_animator = GetComponent<Animator>(); // Get the Animator component
    }

    public void OnRayInteract()
    {
        if (tissueCount < maxTissues)
        {   

            btn_sound.Play();
            SpawnTissue();
            tissueCount++;

            if (tissueCount == 1) // Start the countdown on the first button press
            {
                chopeGameManager.StartCountdown();
                Debug.Log("Countdown started.");
            }

            Debug.Log("Tissue spawned. Total tissues: " + tissueCount);

        }
        else
        {
            Debug.Log("Maximum number of tissues reached.");
        }
    }

    private void SpawnTissue()
    {
        GameObject newTissue = Instantiate(tissuePrefab, spawnPoint.position, spawnPoint.rotation);
        newTissue.name = "Tissue Packet " + tissueCount; // Unique name for each tissue
    }
            
}
