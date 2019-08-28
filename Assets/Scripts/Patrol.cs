using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public Transform[] moveSpots;

    private int rand;
    private int nextPos;

    public bool isRandom;
    /*
        rand se usará en caso de que el movimiento se requiera aleatorio.
        nextPos se usará en caso de que el movimiento se quiera predeterminado
        isRandom controlará esa desición.
    */
    public float startWaitTime;
    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        if (isRandom)
        {
            rand = Random.Range(0, moveSpots.Length);
        }
        else
        {
            nextPos = 0; // iniciará moviendose por defecto al primer lugar
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRandom)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveSpots[rand].position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, moveSpots[rand].position) < 0.8f)
            {
                if(waitTime <= 0)
                {
                    rand = Random.Range(0, moveSpots.Length);
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, moveSpots[nextPos].position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, moveSpots[nextPos].position) < 0.8f)
            {
                if (waitTime <= 0)
                {
                    nextPos += 1;
                    if(nextPos > moveSpots.Length - 1)
                    {
                        nextPos = 0; 
                    }
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

       
    }


 
}
