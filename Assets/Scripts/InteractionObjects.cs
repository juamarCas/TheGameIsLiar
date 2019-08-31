using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjects : MonoBehaviour
{


    [Header("Componentes de tiempo")]
    public float timeBtwnTalk; //tiempo entre poder tocar el botón de hablar
    private float talkCounter;
    bool canTalk = true; //puede hablar? 

    [Header("Dependencies")]
    public PlayerMovement playerMovement;
    // Start is called before the first frame update

    private void Start()
    {
        talkCounter = 0f;
    }

    void Update()
    {
        if (talkCounter <= 0)
        {
            canTalk = true;
        }
        else
        {
            talkCounter -= Time.deltaTime;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        InteractableObject interactable = other.gameObject.GetComponent<InteractableObject>();
        if (interactable != null)
        {
            Debug.Log("Entered");
            if (other.tag == "Interactable" && Input.GetKeyDown(KeyCode.E) && canTalk)
            {
                talkCounter = timeBtwnTalk;
                canTalk = false;
                if (interactable.isInteracting == false)
                {
                    interactable.Interact();
                    interactable.isInteracting = true;
                }
                else
                {
                    interactable.NextSentence();
                }
            }

            if (interactable.isInteracting)
            {
                playerMovement.canMove = false;
                playerMovement.faceTarget(interactable.transform, 10f);

            }
            else
            {
                playerMovement.canMove = true;
            }
        }
    }
}
