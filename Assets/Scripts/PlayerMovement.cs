using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States { idle, walking, talking}
public class PlayerMovement : MonoBehaviour
{
    [Header("Componentes de velocidad")]
    public float moveSpeed;

    [Header("States")]
    public States state;

    public float timeBtwnTalk; //tiempo entre poder tocar el botón de hablar
    private float talkCounter;
    bool canTalk = true; //puede hablar? 

    private float angle;
    private Quaternion targetRotation;
    private Vector2 input;
    Transform cam; 


   
    private Rigidbody rb;
    private bool canMove = true; //se puede mover? 
   

    // Start is called before the first frame update
    void Start()
    {
        talkCounter = 0f;
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(talkCounter <= 0)
        {
            canTalk = true;
        }
        else
        {
            talkCounter -= Time.deltaTime;
        }
       
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            PlayerInput();
            if(Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) {
                state = States.idle;
                return; }
            CalculateDirection();
            PlayerRotate();
            Move();
        }
        
    }

    private void PlayerInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    #region Movement
    private void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }
    private void PlayerRotate()
    {
        targetRotation = Quaternion.Euler(0f, angle, 0f);
        transform.rotation = targetRotation;

    }
    private void Move()
    {
        state = States.walking;
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
    #endregion

    private void OnTriggerStay(Collider other)
    {

        NPC npc = other.GetComponent<NPC>();
        if (npc != null)
        {
            if (other.tag == "NPC" && Input.GetKeyDown(KeyCode.F) && canTalk)
            {
                talkCounter = timeBtwnTalk;
                canTalk = false;

                if (npc.isTalking == false)
                {
                    //comienza parla
                    npc.Talk();
                    npc.isTalking = true;
                    state = States.talking;
                    npc.state = States.talking;
                    faceTarget(npc.transform, 20f);
                    npc.faceTarget(transform, 20f);
                }
                else
                {
                    npc.NextSentence();
                }

            }

            if (npc.isTalking)
            {
                canMove = false;
            }
            else
            {
                canMove = true;
            }
        }
        else
        {
            return;
        }


    }

    void faceTarget(Transform target, float damping)
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
}
