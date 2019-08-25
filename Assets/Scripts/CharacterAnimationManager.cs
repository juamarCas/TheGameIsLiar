using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    PlayerMovement playerMovement;
    NPC npc;
    Animator anim;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        npc = GetComponentInParent<NPC>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Player Animations
        if(transform.tag == "Player") { 
            if (playerMovement.state == States.idle)
                anim.SetInteger("param", 0);
            if (playerMovement.state == States.walking)
                anim.SetInteger("param", 1);
            if (playerMovement.state == States.talking)
                anim.SetInteger("param", 2);
        }
        //NPC Animations
        else
        {
            Debug.Log("harold");
            if (npc.state == States.idle)
                anim.SetInteger("param", 0);
            if (npc.state == States.walking)
                anim.SetInteger("param", 1);
            if (npc.state == States.talking)
                anim.SetInteger("param", 2);
        }
            
    }
}
