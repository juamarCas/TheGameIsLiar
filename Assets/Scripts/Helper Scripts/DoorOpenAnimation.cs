using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenAnimation : MonoBehaviour
{
    public GameObject door_Right;
    public GameObject door_Left;

    public Transform door_Right_target;
    public Transform door_Left_target;

    public float openSpeed;
    Vector3 door_Right_originalPos;
    Vector3 door_Left_originalPos;
    bool openDoor;

    private void Start()
    {
        door_Right_originalPos = door_Right.transform.position;
        door_Left_originalPos = door_Left.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            openDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            openDoor = false;
    }

    private void Update()
    {
        if (openDoor)
        {
            door_Right.transform.position = Vector3.Lerp(door_Right.transform.position, door_Right_target.position, openSpeed * Time.deltaTime);
            door_Left.transform.position = Vector3.Lerp(door_Left.transform.position, door_Left_target.position, openSpeed * Time.deltaTime);
        }
        else
        {
            door_Right.transform.position = Vector3.Lerp(door_Right.transform.position, door_Right_originalPos, openSpeed * Time.deltaTime);
            door_Left.transform.position = Vector3.Lerp(door_Left.transform.position, door_Left_originalPos, openSpeed * Time.deltaTime);
        }
    }
}
