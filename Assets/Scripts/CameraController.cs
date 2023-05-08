using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    
    [SerializeField] float sensitivity = 1;

    float mouseY, mouseX;

  

    // Update is called once per frame
    void Update()
    {
        

        mouseY += Input.GetAxis("Mouse Y")*sensitivity;
        mouseX += Input.GetAxis("Mouse X")*sensitivity;

        transform.localRotation = Quaternion.Euler(-mouseY, mouseX, 0);
        
    }
   
}
