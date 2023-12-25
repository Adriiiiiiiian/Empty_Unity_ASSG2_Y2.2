using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginBtn : MonoBehaviour
{
    public GameObject button; // Reference to your VR button object
    private bool isActive = true;

    private void Start()
    {
        // Start the coroutine to deactivate the button after 3 seconds
        StartCoroutine(DeactivateButtonAfterDelay(3f));
    }

    IEnumerator DeactivateButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isActive)
        {
            // Deactivate the button after the delay
            button.SetActive(false);
            isActive = false;
        }
    }
}
