using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TissueCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Table")
        {
            FindObjectOfType<ChopeGameManager>().IncreaseScore(); // Add score when tissue is placed on table
            gameObject.SetActive(false);
        }
    }

}
