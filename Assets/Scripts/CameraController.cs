using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //fields
    public Transform followTarget;
    [SerializeField] float sensitivity = 1;
    //properties
    float mouseY, mouseX;
    
    // Update is called once per frame
    void Update()
    {

       // Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));

        mouseX += Input.GetAxis("Mouse Y")*sensitivity; 
        mouseY += Input.GetAxis("Mouse X")*sensitivity;



        if ( mouseX < 45 && mouseX > -25)
        {
            followTarget.transform.localRotation = Quaternion.Euler(-mouseX,0, 0);                   
            transform.localRotation = Quaternion.Euler(0, mouseY, 0);            
        }

    }
   
}
