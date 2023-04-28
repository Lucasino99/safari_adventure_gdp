using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable //we want to inherit from Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //overwrite interact function & design interaction
    protected override void Interact() 
    {
        //Debug.Log("Interacted with" + gameObject.name);
        doorOpen= !doorOpen; //toggle between true and false
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }
}
