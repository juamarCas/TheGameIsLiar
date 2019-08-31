using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

[System.Serializable]
public class NPC : MonoBehaviour
{

    [Header("General Components")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI textDisplay;
    public States state = States.idle;
    public string Name;
    public bool isKeyNPC; // es un NPC clave

    [Header("Dynamic Interactions")]
    public bool hasDynamicInteraction = false;
    public bool startsInteraction = false;
    public bool ActivateEvent = false; //activa algún evento este NPC?
    public bool hasActivatedEvent = false;
    public bool haveToGiveYouSomething = false;
    public bool haveToReceiveSomething = false; 
    public int dialogueState; // estado de dialogo 
    public GameObject[] NPCinteractions; //esta variable guardará a todos los npc en el cual este les afecte la interacción
    public GameObject NPCQuest; // este se activará en caso de que este active un evento a otro NPC

    [Header("Dialogue")]
    public bool isTalking;  // está hablando con el jugador?
    private int index;
    public List<Sentences> dialogue = new List<Sentences>();
    public GameObject chatBox;
    private Queue<string> conversation = new Queue<string>();
    [System.Serializable]
    public class Sentences
    {
        [TextArea(3, 10)]
        public string[] sentences;
        
    }
    
  
    void Start()
    {
        
        isTalking = false;
        dialogueState = 0; 
    }


    #region Talking
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
            yield return new WaitForSeconds(0.00025f);
        }
    }

    //pasa a la siguiente oración del NPC
    public void NextSentence()
    {
        if(index < dialogue[dialogueState].sentences.Length - 1)
        {
            textDisplay.text = "";
            index++;
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            index = 0;
            isTalking = false;
            chatBox.gameObject.SetActive(false);
            state = States.idle;
        }
    }

    public void faceTarget(Transform target, float damping)
    {
        Debug.Log("faceando");
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
    #endregion

    
    public void changeDialogueState(bool coffee, bool taco)
    {
        if (dialogueState == dialogue.Count-1)
            return;
        if (!haveToGiveYouSomething && !haveToReceiveSomething)
        {
            dialogueState++;
            startsInteraction = true;
        }
        if(haveToGiveYouSomething && haveToReceiveSomething && coffee)
        {
            dialogueState++;
            startsInteraction = true;
        }
        if (!haveToGiveYouSomething && haveToReceiveSomething && taco)
        {
            dialogueState++;
            startsInteraction = true;
        }

    }


}
