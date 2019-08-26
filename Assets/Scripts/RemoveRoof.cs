using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRoof : MonoBehaviour
{
    public GameObject roof;
    private bool playerIsInside = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            roof.SetActive(false);
            playerIsInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            roof.SetActive(true);
            playerIsInside = false;
        }
    }
}
