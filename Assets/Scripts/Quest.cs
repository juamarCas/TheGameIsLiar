using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{


    #region Singleton
    public static Quest instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    #endregion


    [Header("Quest Component")]
    public bool hasTalkedToOfficer; //1era parte del juego, hablar con el oficial y el tendero para poder salir de la estación
    public bool hasReadTheOmicideReport;//tiene que leer el reporte de homicidio en la mesa
    public bool hasTalkedToTheNPCInTheCoffe; // tiene que ver con la activación del evento del diablo
    public GameObject[] KeyNPC; //NPC claves
    public bool AlienAndCoffeEvent;
    public bool TalkToChiefToFinish; 

    [Space]
    [Header("Events")]
    public GameObject devilNPC;
    public GameObject ghostNPC;

   

    // Start is called before the first frame update
    void Start()
    {
        devilNPC.gameObject.SetActive(false);
        if(ghostNPC != null)
            ghostNPC.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
