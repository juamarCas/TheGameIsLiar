using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRoof : MonoBehaviour
{
    public List<GameObject> roof;
    private bool playerIsInside = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject r in roof) {
                r.SetActive(false);
            }
            playerIsInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject r in roof)
            {
                r.SetActive(true);
            }
            playerIsInside = false;
        }
    }
}
