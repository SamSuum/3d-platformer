using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{     
    //ref   
    private CharacterController controller;
    public Transform cam;
   

    //properties
    [SerializeField]private bool isGrounded;    
    private Vector3 playerVelocity;
    float maxSpeed;

   

    //fields
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float speed = 0.0f;
    [SerializeField] float walk = 10.0f;
    [SerializeField] float sprint = 40.0f;
    [SerializeField] float acceleration = 0.5f;
    [SerializeField] float deceleration = 3.0f;


    //methods
    private void Start()
    {               
        controller = GetComponent<CharacterController>();
       
    }


    void Update()
    {
        isGrounded = controller.isGrounded;


        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;

        }
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && isGrounded) playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move((playerVelocity) * Time.deltaTime);

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = cam.TransformDirection(move);
        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {

            if (speed < maxSpeed)
            {
                if (Input.GetAxis("Horizontal") != 0) speed += acceleration * Time.deltaTime;
                if (Input.GetAxis("Vertical") != 0) speed += acceleration * Time.deltaTime;
            }
            else if (speed > maxSpeed) speed -= deceleration * Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftShift)) maxSpeed = sprint; else maxSpeed = walk;


        }
        else if (speed > 0) speed -= deceleration * Time.deltaTime;

       
    }

   

  
   

    
   
}
