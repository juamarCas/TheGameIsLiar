using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Camera Follow")]
    public Transform target;
    public Vector3 offsetPosition;
    Vector3 targetPosition;

    [Header("Camera Rotation")]
    public GameObject pivot;
    public float cameraRotSpeed = 0.5f;
    bool rotatingCamera = false;
    int i = 0;
    float t = 0;


    private void Start()
    {
        
        transform.position = target.position - offsetPosition;
        transform.LookAt(target.position);
    }

    void Update()
    {
        pivot.transform.position = target.position;
        changeAngle();
    }

    void changeAngle()
    {
        if (Input.GetKeyDown(KeyCode.R) && t <= 0)
        {
            if (i == 0)
            {
                StartCoroutine(rotateCamera(90));
                i++;
            }
            else if(i == 1)
            {
                StartCoroutine(rotateCamera(180));
                i++;
            }
            else if(i == 2)
            {
                StartCoroutine(rotateCamera(270));
                i++;
            }
            else
            {
                StartCoroutine(rotateCamera(0));
                i= 0;
            }
        }
    }

    IEnumerator rotateCamera(float targetRotY)
    {
        t = 0.25f;
        Quaternion targetRot = Quaternion.Euler(pivot.transform.rotation.x, targetRotY, pivot.transform.rotation.z);
        while (t>0)
        {
            Debug.Log(t);
            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, targetRot, cameraRotSpeed * Time.deltaTime);
            t -= Time.deltaTime;
            yield return null;
        }        
    }
}
