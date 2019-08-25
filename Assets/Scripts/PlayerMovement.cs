using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private CharacterController controller;
    public Rigidbody rb;
    private float lastRotation; 
    private Vector3 moveDirection;
    public float rotateSpeed;
    private bool canMove = true; //se puede mover? 
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            PlayerMove();
            PlayerRotate();
        }
        
    }

    private void PlayerMove()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
    }

    #region Rotation
    private void PlayerRotate()
    {
        if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            Rotate(-90);    
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            Rotate(90);
        }else if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            Rotate(0);
        }else if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            Rotate(180);
        }else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            Rotate(45);
        }
        else if (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            Rotate(-135);
        }else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            Rotate(135);
        }else if (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            Rotate(-45);
        }
        else
        {
            Rotate(lastRotation);
        }
    }
    private void Rotate(float y)
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0f, y , 0f);
        lastRotation = y; 
    }
    #endregion

    private void OnTriggerStay(Collider other)
    {

        NPC npc = other.GetComponent<NPC>();
        if (npc != null)
        {
            if (other.tag == "NPC" && Input.GetKeyDown(KeyCode.F) )
            {

                if (npc.isTalking == false)
                {
                    //comienza parla
                    npc.Talk();
                    npc.isTalking = true;
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
}
