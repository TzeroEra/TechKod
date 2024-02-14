using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySelector : MonoBehaviour
{
    [SerializeField]
    private TestScript testScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Press 'E' to select ability");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                testScript.Start();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}