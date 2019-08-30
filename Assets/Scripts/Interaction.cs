using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interaction : MonoBehaviour
{
   
    [Header("Componentes de tiempo")]
    public float timeBtwnTalk; //tiempo entre poder tocar el botón de hablar
    private float talkCounter;
    bool canTalk = true; //puede hablar? 

    [Header("Dependencies")]
    public PlayerMovement playerMovement;

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

        NPC npc = other.GetComponent<NPC>(); // npcs
        InteractableObject interactable = other.GetComponent<InteractableObject>(); // items

        //INTERACCIÓN CON NPCS
        if (npc != null)
        {
            if (other.tag == "NPC" && Input.GetKeyDown(KeyCode.E) && canTalk)
            {
                talkCounter = timeBtwnTalk;
                canTalk = false;

                if (npc.isTalking == false)
                {
                    if (npc.hasDynamicInteraction && npc.startsInteraction)
                    {
                        npc.startsInteraction = false;
                        foreach (GameObject _npc in npc.NPCinteractions)
                        {
                            _npc.GetComponent<NPC>().changeDialogueState();
                        }
                    }
                    npc.Talk();
                    npc.isTalking = true;
                    playerMovement.state = States.talking;
                    npc.state = States.talking;

                }
                else
                {
                    npc.NextSentence();
                }

                if (npc.isTalking)
                {
                    playerMovement.canMove = false;
                    playerMovement.faceTarget(npc.transform, 10f);
                    npc.faceTarget(transform, 10f);
                }
                else
                {
                    playerMovement.canMove = true;
                }
            }

            // INTERACCIÓN CON ITEMS
            else if (interactable != null)
            {
                if (other.tag == "Interactable" && Input.GetKeyDown(KeyCode.F) && canTalk)
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
            else
            {
                return;
            }

        }
    }
}
