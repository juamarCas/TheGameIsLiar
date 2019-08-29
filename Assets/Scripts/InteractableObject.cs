using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractableObject : MonoBehaviour
{

    [Header("Componentes de texto")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI textDisplay;
    public GameObject chatBox;


    public string objectName;
    public string[] sentences;
    private int index;
    public bool isInteracting;
    // Start is called before the first frame update
    void Start()
    {
        objectName = "";
        isInteracting = false;
    }

    // Update is called once per frame
 

    public void Interact()
    {

        textDisplay.text = "";
        if (nameText != null)
        {
            chatBox.gameObject.SetActive(true);
            nameText.text = objectName;
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
        if (index < sentences.Length - 1)
        {

            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            index = 0;
            textDisplay.text = "";
            isInteracting = false;
            chatBox.gameObject.SetActive(false);
            
        }
    }

    public void faceTarget(Transform target, float damping)
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
}
