using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAndPlace : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject snap1;
    public GameObject square;
    public GameObject door;

    public int count;

    public void CollectGame()
    {
        if (square.transform.position == snap1.transform.position)
        {
            Debug.Log("DOOR OPEN");
            door.SetActive(false);
        }
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
