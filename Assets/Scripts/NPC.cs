using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI textDisplay;

    public string Name;
    public string[] sentences;
    private int index;
    public bool isTalking;  // está hablando con el jugador?
    public bool canType = true; // puede presionar F para seguir hablando?


    public GameObject chatBox;
  
    void Start()
    {
        isTalking = false;
        canType = true;
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

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void NextSentence()
    {
        if(index < sentences.Length - 1)
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
        }
    }
}
