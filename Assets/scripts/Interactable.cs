using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//template for all of our subclasses. Anything thatour player needs to interact
//with will inherit from out 'Interactable' script
public abstract class Interactable : MonoBehaviour

{
    public bool useEvents; //use this to add/remove interaction event 
    //message that is displayed to the player when looking at an interactable
    [SerializeField]
    public string promptMessage; 

    //this function will be called from our player (script)
    public void BaseInteract()
    {
        if(useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }

    protected virtual void Interact()
    {
        //we wont have any code called from the base interactable script
        //any custom code will go inside of this method on our inherited script
    }

}
