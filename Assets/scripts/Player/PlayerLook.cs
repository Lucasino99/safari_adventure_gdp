using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation= 0f;

    public float xSensitivity= 30f;
    public float ySensitivity= 30f;
    
    // we removed start and update in this script
    public void ProcessLook(Vector2 input)
    {
        float mouseX= input.x;
        float mouseY= input.y;
        //calculate camera rotation for looking up + down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation= Mathf.Clamp(xRotation, -80, 80f);
        //apply this to our camera transform
        cam.transform.localRotation= Quaternion.Euler(xRotation, 0, 0);
        //rotate player to look left + right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

}
