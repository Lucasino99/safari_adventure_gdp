using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot; //reference to OnFoot action map

    private PlayerMotor motor; //private property for PlayerMotor.cs
    private PlayerLook look;   //private property for PlayerLook.cs

    void Awake() //note that i changed to original start function to awake!
    {
        playerInput= new PlayerInput();
        onFoot= playerInput.OnFoot;
        motor= GetComponent<PlayerMotor>();
        //anytime our onfoot.jump is performed we want it to call out (ctx) our motor.jump function
        look= GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump(); 
    }

    
    void FixedUpdate() //note that i use FixedUpdate
    {
        //tell the player motor to move using the value from our movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>()); 
    } //basically the same as above but using look stuff

    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }

}
