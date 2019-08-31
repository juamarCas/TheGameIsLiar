using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interaction : MonoBehaviour
{
   
    [Header("Componentes de tiempo")]
    public float timeBtwnTalk; //tiempo entre poder tocar el botón de hablar
    public PlayerManager thePM;
    private float talkCounter;
    bool canTalk = true; //puede hablar? 
    
    [Header("Dependencies")]
    public PlayerMovement playerMovement;
    NPC npc;
    InteractableObject interactable;
    //private NPC npc;

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
        
        npc = other.GetComponent<NPC>(); // npcs
        //interactable = other.GetComponent<InteractableObject>(); // items
       
        //INTERACCIÓN CON NPCS
        if (npc != null)
        {
            Debug.Log("Not human");
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
                            _npc.GetComponent<NPC>().changeDialogueState(false,false);
                        }
                    }
                    if (npc.haveToGiveYouSomething && !npc.haveToReceiveSomething && !thePM.hasTheCoffee)
                    {
                        thePM.hasTheCoffee = true;
                       
                    }else if(npc.haveToGiveYouSomething && npc.haveToReceiveSomething && thePM.hasTheCoffee && !thePM.hasTheTaco)
                    {
                        thePM.hasTheTaco = true;
                        npc.changeDialogueState(thePM.hasTheCoffee, thePM.hasTheTaco);
                    }
                    else if (!npc.haveToGiveYouSomething && npc.haveToReceiveSomething && thePM.hasTheCoffee && thePM.hasTheTaco)
                    {
                        npc.changeDialogueState(thePM.hasTheCoffee, thePM.hasTheTaco);
                    }

                        if (npc.ActivateEvent && !npc.hasActivatedEvent)
                    {
                        npc.hasActivatedEvent = true;
                        npc.NPCQuest.gameObject.SetActive(true);
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

               
            }

 
            if (npc.state == States.talking)
            {
                Debug.Log("harold");
                playerMovement.canMove = false;
                playerMovement.faceTarget(npc.transform, 10f);
                npc.faceTarget(transform, 10f);
            }
            else
            {
                playerMovement.canMove = true;
            }

        }
    }

    
}
