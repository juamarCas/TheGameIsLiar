using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States { idle, walking, talking}
public class PlayerMovement : MonoBehaviour
{
    [Header("Componentes de velocidad")]
    public float moveSpeed;
    public bool canMove = true; //se puede mover? 

    [Header("Mesh Movement")]
    public Transform mesh;
    private float meshRotY;

    [Header("States")]
    public States state;

    private float angle;
    private Quaternion targetRotation;    
    private Vector2 input;
    Transform cam; 
    
    private Rigidbody rb;
   

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        meshRotY = mesh.rotation.y;
    }

    

    private void FixedUpdate()
    {
        Vector3 TargetPos = new Vector3(transform.position.x, transform.position.y - 0.93f, transform.position.z);
        mesh.position = TargetPos;

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

        Quaternion targetRotationMesh = Quaternion.Euler(0f, angle + 90f, 0f);
        mesh.rotation = Quaternion.Lerp(mesh.rotation, targetRotationMesh, 10f * Time.deltaTime);   
    }
    private void Move()
    {
        state = States.walking;
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
    #endregion

    

    public void faceTarget(Transform target, float damping)
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
}
