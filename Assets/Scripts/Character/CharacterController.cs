using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    float m_JumpForce;
    float m_Speed;
    Vector2 m_Rotation = Vector2.zero;
    float m_LookSpeed;
    bool m_IsOnGround;
    public bool m_Pause;
    public Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Speed = 5.0f;
        m_LookSpeed = 3.0f;
        m_JumpForce = 5.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        PauseController();
        if(!m_Pause){
            MovementController();
            Look();
        }
    }

    void PauseController(){
        if (m_Pause){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else{
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    //Fix issue with controller maintaining velocity on collision with other objects
    void MovementController(){
        if(m_IsOnGround){
            //move forward
            if (Input.GetKey("w"))
            {
                // print("w was pressed");
                //Translate Method
                // transform.Translate(Vector3.forward * Time.deltaTime * m_Speed);

                //Physics Method
                m_Rigidbody.velocity = transform.forward * m_Speed;
            }
            //move left
            if (Input.GetKey("a"))
            {
                // print("a was pressed");
                //Translate Method
                // transform.Translate(Vector3.right * Time.deltaTime * -m_Speed);

                m_Rigidbody.velocity = -transform.right * m_Speed;
            }
            //move back
            if (Input.GetKey("s"))
            {
                // print("s was pressed");
                //Translate Method
                // transform.Translate(Vector3.forward * Time.deltaTime * -m_Speed);

                //Physics Method
                m_Rigidbody.velocity = -transform.forward * m_Speed;
            }
            //move right
            if (Input.GetKey("d"))
            {
                // print("d was pressed");
                //Translate Method
                // transform.Translate(Vector3.right * Time.deltaTime * m_Speed);

                //Physics Method
                m_Rigidbody.velocity = transform.right * m_Speed;

            }
            //jump
            if (Input.GetKey("space"))
            {
                print("space was pressed");
                m_Rigidbody.velocity = transform.up * m_JumpForce;
            }

            moveDirection = m_Rigidbody.velocity;
        } 
        //sprint
            if(Input.GetKeyDown(KeyCode.LeftShift)){
                m_Speed = m_Speed * 2; 
            }
            else if(Input.GetKeyUp(KeyCode.LeftShift)){
                m_Speed = m_Speed / 2;
            }
    }

    void Look(){
        m_Rotation.y += Input.GetAxis ("Mouse X");
        m_Rotation.x += -Input.GetAxis("Mouse Y");
        m_Rotation.x = Mathf.Clamp(m_Rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0, m_Rotation.y) * m_LookSpeed;
        Camera.main.transform.localRotation = Quaternion.Euler(m_Rotation.x * m_LookSpeed, 0, 0);
    }

    void OnTriggerEnter(Collider other){
        m_IsOnGround = true;
    }
    void OnTriggerExit(Collider other){
        m_IsOnGround = false;
    }

    

}
