using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isJumping;
    private float jumpTimer;
    public float speed= 5f;
    public float gravity= -9.81f;
    public float jumpHeight= 3f;
    public AudioSource jumpAudioSource; 
    public AudioSource walkingAudioSource; 
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        controller= GetComponent<CharacterController>();
        Transform characterModel = transform.Find("Safari_Steve_V2");
        animator = characterModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded= controller.isGrounded;

        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= animator.GetCurrentAnimatorStateInfo(0).length)
            {
                animator.SetBool("IsJumping", false);
                isJumping = false;
                jumpTimer = 0f;
            }
        }
    }

    //receive the inputs for our InputManager.cs and apply them to our character controller
    private bool isWalking = false;
    public void ProcessMove(Vector2 input)
    {
        //move around with WASD
        Vector3 moveDirection= Vector3.zero;
        moveDirection.x= input.x;
        moveDirection.z= input.y;

        // Check if player is grounded and moving
        if (isGrounded && moveDirection.magnitude > 0)
        {
            // Play walking sound effect & animation
            if (!isWalking)
            {
                walkingAudioSource.Play();
                animator.SetBool("IsRunning", true);
                isWalking = true; // only affects sound, not the animation
            }
        }
        else
        {
            // Stop walking sound effect
            if (isWalking)
            {
                walkingAudioSource.Stop();
                animator.SetBool("IsRunning", false);//
                isWalking = false;
            }
        }

        // Apply movement to controller
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        // Add gravity
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y= -2f;
        }
            
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y= Mathf.Sqrt(jumpHeight * -3 * gravity);
            jumpAudioSource.Play(); // Play the jump sound effect
            animator.SetBool("IsJumping", true);
            isJumping = true;
        }
    }
}
