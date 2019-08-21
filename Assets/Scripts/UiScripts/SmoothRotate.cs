using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SmoothRotate : MonoBehaviour
{
    float rotationSpeed = 0.2f;

    void OnMouseDrag()
    {
        if(SceneManager.GetActiveScene().name == "MakeChar" || SceneManager.GetActiveScene().name == "Inventory")
        {
            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
            // select the axis by which you want to rotate the GameObject
            transform.RotateAround(Vector3.down, XaxisRotation);
            // transform.RotateAround(Vector3.right, YaxisRotation);
        }

    }
}

