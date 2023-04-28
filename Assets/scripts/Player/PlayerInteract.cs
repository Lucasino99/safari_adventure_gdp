using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script will contain all the logic to detect interactables and handle player input
public class PlayerInteract : MonoBehaviour 
{    
    private Camera cam; 
    [SerializeField]
    private float distance= 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        //because we already have our camera on the 'playerlook' script 
        //which is attached to our player game object we can just assign it to the cam variable
        cam= GetComponent<PlayerLook>().cam;
        //in order to detect interactables we will use a 'raycast'
        playerUI= GetComponent<PlayerUI>();
        inputManager= GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty); //clear message when not looking at interactable
        //create a ray at the center of the camera shooting outwards (infinite distance by default)
        Ray ray= new Ray(cam.transform.position, cam.transform.forward);
        //to see our ray in Scene view;
        Debug.DrawRay(ray.origin,ray.direction * distance); 
        //variable to store our raycasthit & check if we actually hit anything
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, distance, mask)) //raycast returns a boolean
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null) //if we are looking at an interactable
            {
                //Debug.Log(hitInfo.collider.GetComponent<Interactable>().promptMessage); //display the prompt message
                //because we're calling the "hitInfo.collider.GetComponent<Interactable>();" line so often we'll store it in a variable
                Interactable interactable= hitInfo.collider.GetComponent<Interactable>(); 
                playerUI.UpdateText(interactable.promptMessage);
                if(inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract(); //inside the BaseInteract function we're calling the interact function (from 'interactable.cs)
                }
            } 
        }
    }
}
