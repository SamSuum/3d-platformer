using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animations : MonoBehaviour
{
    //references
    private Animator anim;
    
    //Fields
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    [SerializeField] float maxVelocity;
    [SerializeField] float acceleration = 0.5f;
    [SerializeField] float deceleration = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void Update()
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

        # region moving acceleration
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxVelocity = 1.0f;
        }else
        {
            maxVelocity = 0.5f;
        }

        if ((Input.GetAxis("Horizontal") != 0))
        {
            if ((Input.GetAxis("Horizontal")>0) && velocityX < maxVelocity) velocityX +=  acceleration * Time.deltaTime;
            if ((Input.GetAxis("Horizontal") < 0) && velocityX > -maxVelocity) velocityX += -1 * acceleration * Time.deltaTime;
        }
        if ((Input.GetAxis("Vertical") != 0))
        {
            if ((Input.GetAxis("Vertical") > 0) && (velocityZ < maxVelocity)) velocityZ +=   acceleration * Time.deltaTime;
            if ((Input.GetAxis("Vertical") < 0) &&  (velocityZ > -maxVelocity)) velocityZ += -1 * acceleration * Time.deltaTime;
        }


        if ((Input.GetAxis("Horizontal") == 0))
        {
            if (velocityX < 0) velocityX -= deceleration * Time.deltaTime;
            if (velocityX > 0) velocityX += deceleration * Time.deltaTime;
        }
        if ((Input.GetAxis("Vertical") == 0))
        {
            if (velocityZ < 0) velocityZ -= deceleration*2 * Time.deltaTime;
            if (velocityZ > 0) velocityZ += deceleration *2* Time.deltaTime;
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
