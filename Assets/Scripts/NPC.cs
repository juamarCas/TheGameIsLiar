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
    


    public GameObject chatBox;
  
    void Start()
    {
        isTalking = false;
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
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    //pasa a la siguiente oración del NPC
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
