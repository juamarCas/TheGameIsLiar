using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class NPC : MonoBehaviour
{
    

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI textDisplay;
    public States state = States.idle;
    public GameObject[] NPCinteractions; //esta variable guardará a todos los npc en el cual este les afecte la interacción
    public string Name;
    public int dialogueState; // estado de dialogo 
    public bool itHasInteractions;//tiene alguna interacción con otro NPC?
    public bool firsToInteract;//determinará el orden con el que debes hablar con las interacciones con el resto
    public bool hasTalkedToOther = false; // ya hablaste con el otro NPC?
    public bool startInteractions; // quien inicia las interacciones? 
   
    
    //public string[] dialogues; 
    private int index;
    public bool isTalking;  // está hablando con el jugador?

    [System.Serializable]
    public class Sentences
    {
        public string[] sentences;
    }

    public List<Sentences> dialogue = new List<Sentences>();

    public GameObject chatBox;
  
    void Start()
    {
        isTalking = false;
        dialogueState = 0; 
    }

  

    public void Talk()
    {
        
        textDisplay.text = "";
        if (nameText != null)
        {
            chatBox.gameObject.SetActive(true);
            nameText.text = Name;
        }
        StartCoroutine(Type());
    }

    //muestra en el chatbox letra por letra en la converzación
    IEnumerator Type()
    {
        foreach (char letter in dialogue[dialogueState].sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    //pasa a la siguiente oración del NPC
    public void NextSentence()
    {
        if(index < dialogue[dialogueState].sentences.Length - 1)
        {
          
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            index = 0;
            textDisplay.text = "";
            isTalking = false;
            chatBox.gameObject.SetActive(false);
            state = States.idle;
        }
    }

    public void faceTarget(Transform target, float damping)
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    public void ChangeOriginalNPCDialogueState(NPC npc)
    {

    }

    public void ChangeConversationState(NPC npc)
    {
        if(npc != null)
        {
            npc.dialogueState = 1;
            
        }
    }

    public void ChangeOtherNPCState() // cambiar dialogo según con quien hayas hablado
    {
       
        firsToInteract = false;
        NPC otherNPC = NPCinteractions[0].gameObject.GetComponent<NPC>();
        if(otherNPC != null)
        {
            ChangeConversationState(otherNPC);
        }
        else
        {
            return;
        }
        
    }
}
