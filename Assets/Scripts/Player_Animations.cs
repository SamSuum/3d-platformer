using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animations : MonoBehaviour
{
    //references
    private Animator anim;

    //Fields    
    [SerializeField] float acceleration = 2.0f;
    [SerializeField] float deceleration = 2.0f;

    //properties
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    float maxVelocity;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         
         #region Attack

         if (Input.GetKeyDown(KeyCode.Mouse0))
         {
             StartCoroutine(AnimAttack());
         }
         else anim.SetBool("Slash", false);
         #endregion
        
        
        #region Jump
        if (Input.GetButtonDown("Jump")) anim.SetBool("Jump",true);
        else anim.SetBool("Jump", false);
        #endregion
         
        #region moving acceleration
        
        
        maxVelocity=(Input.GetKey(KeyCode.LeftShift))? 1.0f : 0.5f; //check for sprint button & sets animation to sprint or walk

        switch(Input.GetAxis("Vertical")) //check for Z movement
        {
            case 0: // decelerates when player stops
                if ((velocityZ < 0)) velocityZ += deceleration * Time.deltaTime;
                if ((velocityZ > 0)) velocityZ -= deceleration * Time.deltaTime;
                break;
            case float n when (n <0): // accelerates until maxVelocity reached
                if (velocityZ > -maxVelocity) velocityZ -= acceleration * Time.deltaTime;
                if (velocityZ < -maxVelocity) velocityZ += deceleration * Time.deltaTime;
                break;
            case float n when (n > 0):// idem
                if (velocityZ < maxVelocity) velocityZ += acceleration * Time.deltaTime;
                if (velocityZ > maxVelocity) velocityZ -= deceleration * Time.deltaTime;
                break;
        }

        switch (Input.GetAxis("Horizontal")) // check for X movement
        {
            case 0:
                if ((velocityX < 0)) velocityX += deceleration * Time.deltaTime;
                if ((velocityX > 0)) velocityX -= deceleration * Time.deltaTime;
                break;
            case float n when (n < 0):

                if(Input.GetAxis("Vertical")<0)
                {
                    if (velocityX < maxVelocity) velocityX += acceleration * Time.deltaTime;
                    if (velocityX > maxVelocity) velocityX += -deceleration * Time.deltaTime;
                }
                else
                {
                    if (velocityX > -maxVelocity) velocityX += -acceleration * Time.deltaTime;
                    if (velocityX < -maxVelocity) velocityX += deceleration * Time.deltaTime;
                }
               
                break;

            case float n when (n > 0):
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (velocityX > -maxVelocity) velocityX += -acceleration * Time.deltaTime;
                    if (velocityX < -maxVelocity) velocityX += deceleration * Time.deltaTime;
                }
                else
                {
                    if (velocityX < maxVelocity) velocityX += acceleration * Time.deltaTime;
                    if (velocityX > maxVelocity) velocityX += -deceleration * Time.deltaTime;
                    
                }
                break;
        }

       


        #endregion



        anim.SetFloat("Velocity z ", velocityZ);
        anim.SetFloat("Velocity x", velocityX);
    }

 
 
    IEnumerator AnimAttack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), 1);
        anim.SetBool("Slash", true);
        yield return new WaitForSeconds(2.05f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), 0);
    }
}
