using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{     
        
    private CharacterController controller;
    private Animator anim;

    private bool isGrounded;
    private Vector3 playerVelocity;
    public float jumpHeight = 1.0f;
    public float gravity = -9.81f;

    public float speed = 0.0f;
    public float walk = 10.0f;
    public float sprint = 40.0f;    
    float velocityZ = 0.0f;
    float velocityY= 0.0f;

  
    private void Start()
    {
        
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        
    }


    void Update()
    {
        PlayerControl();


        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(AnimAttack());
        }     
        else anim.SetBool("Slash", false);


    }

    void PlayerControl()
    {
        isGrounded = controller.isGrounded;


        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;

        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprint;
                velocityZ = 1.0f;
            }

            else
            {
                speed = walk;
                velocityZ = 0.5f;
            }

        }
        else
        {
            velocityZ = 0.0f;
        }



        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            velocityY = 1.0f;
        }
        else if (isGrounded) velocityY = 0.0f;

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        anim.SetFloat("Velocity z", velocityZ);
        anim.SetFloat("Velocity y", velocityY);
    }



    IEnumerator AnimAttack ()
    {               
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), 1);
        anim.SetBool("Slash",true);
        yield return new WaitForSeconds(2.05f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), 0);
        
    }    
}
