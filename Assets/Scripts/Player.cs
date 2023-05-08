using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{     
    //ref   
    private CharacterController controller;
    public Transform cam;
    private Animator anim;
    //properties
    private bool isGrounded;
    public bool jumpover = false;
    private Vector3 playerVelocity;
    float maxSpeed;
    float movey = 0;
    float x, y, z;
    string animationName;
    //fields
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float speed = 0.0f;
    [SerializeField] float walk = 10.0f;
    [SerializeField] float sprint = 40.0f;
    [SerializeField] float acceleration = 0.5f;
    [SerializeField] float deceleration = 3.0f;
    [SerializeField] float step = 0.3f;

    //methods
    private void Start()
    {               
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        isGrounded = controller.isGrounded;


        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;

        }

        Jump();

        if (jumpover)
        {          
            StartCoroutine(Wait());
            jumpover = false;
        }
    }

    private void FixedUpdate()
    {
        
        Move();                
    }

    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), movey, Input.GetAxis("Vertical"));
        move = cam.TransformDirection(move);
        controller.Move(move * Time.deltaTime * speed);
        
        if (move != Vector3.zero)
        {
            
            
           

            if (speed < maxSpeed) 
            { 
                if (Input.GetAxis("Horizontal")!=0) speed +=  acceleration * Time.deltaTime;
                if (Input.GetAxis("Vertical") != 0) speed +=  acceleration * Time.deltaTime;
            }
            else if (speed > maxSpeed) speed -= deceleration * Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                maxSpeed = sprint;
            }

            else
            {
                maxSpeed = walk;              
            }

        }
        else if (speed > 0) speed -= deceleration * Time.deltaTime;      
        
    }
    private void Jump()
    {
       
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && isGrounded) playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall1")|| other.CompareTag("Wall2"))
        {
            if (other.CompareTag("Wall1"))
            {
                x = 0; y = 0; z = 2;
                animationName = "JumpOver";
            }
            else if (other.CompareTag("Wall2"))
            {
                x = 0; y = 3; z = 0;
                animationName = "Climb";
            }

            jumpover = true;            
        }
        else jumpover = false;

        if (other.CompareTag("stairs"))
        
        {
            movey = step;
            
        }
        else if (other.CompareTag("downstairs") && movey>0 && !anim.GetBool("Stairs"))
        {
           
            movey =  -step;
        }
        else 
        { 
            movey = 0;
           
        }
    }

    
    IEnumerator Wait()
    {
        controller.enabled = false;
        anim.SetBool(animationName, true);
        yield return new WaitForSeconds(1.0f);
        transform.position += new Vector3(x,y,z);
        controller.enabled = true;
        anim.SetBool(animationName, false);
    }
}
