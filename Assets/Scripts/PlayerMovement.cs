using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;

    public Transform playerRotation;
    //private float animationRotationSpeed = 1.0f;
    float ref0 = 0.0f;
    private float smootherIntensity = 1.0f;
    private Animator characterAnimator;

    //movement variables
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    public float speed = 1;
    bool isRunning = false;

    // Getting Hit variables
    public float speedModifierMagnitude = 1.0f;
    private float speedModifierDuration = 0.25f;

        //gravity variables
    float gravity = -9.8f;
    float groundedGravity = -.05f;
    
    // jump variables-----------------------
    bool isJumpPressed = false;
    //float jumpVelocity;
    [SerializeField] float jumpPower = 3.0f;
    //float maxJumpHeight = 1.0f;
    //float maxJumpTime = 0.5f;
    bool isJumping = false;
    [SerializeField] float onLandDebounceValue = 0.1f;
    private float lastJumpTime;

    //Pretty Stuff, like Particles
    ParticleSystem theParticleSystem;


    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        theParticleSystem = GetComponentInChildren<ParticleSystem>();

        playerInput.CharacterControls.Move.started += context => { context.ReadValue<Vector2>(); };
        playerInput.CharacterControls.Jump.started += OnJump;
        playerInput.CharacterControls.Jump.canceled += OnJump;
        //setupJumpVariables();
        characterAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleSpeedModifier();
        HandleMovement();
        HandleGravity();
        HandleJump();
    }

    void OnJump (InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        //Debug.Log("Is Jump Pressed?" + isJumpPressed);
        
    }
    void setupJumpVariables()
    {
        //float timeToApex = maxJumpTime / 2;
        //gravity = -9.8f;//(-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        //jumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void HandleMovement()
    {
        currentMovementInput = playerInput.CharacterControls.Move.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = 0;//currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        characterController.Move(currentMovement * Time.deltaTime * speed * speedModifierMagnitude);

        //Code for Turning Player Model
        if(currentMovement.x != 0)
        {
            isRunning = true;
            //Vector3 targetAngle = new Vector3(SmoothFloat(playerRotation.rotation.x,currentMovement.x),0f,0f);
            Vector3 targetAngle = new Vector3(currentMovement.x,0,0);
            //playerRotation.rotation = Quaternion.Slerp(playerRotation.rotation,targetangle,1);
            //playerRotation.Rotate(targetangle.eulerAngles);
            //playerRotation.rotation = Quaternion.LookRotation(targetAngle,Vector3.up);
            playerRotation.rotation = Quaternion.LookRotation(targetAngle,Vector3.up);
        }
        else {isRunning = false;}
        if (isRunning)
        {
            characterAnimator.SetBool("Running", true);
        }
        else
        {
            characterAnimator.SetBool("Running", false);
        }
    }

    float SmoothFloat(float a, float b)
    {
        float smoothedFloat = Mathf.SmoothDamp(a, b,ref ref0,smootherIntensity);

        return smoothedFloat;
    }

    void HandleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            isJumping = true;
            lastJumpTime = Time.time;
            BlastJumpParticles();
            currentMovement.y = jumpPower;
        }
        if (isJumping && characterController.isGrounded && Time.time > onLandDebounceValue + lastJumpTime) // Added onLand with Debounce
        {
            isJumping = false;
            OnlandOnGround();
        }
        if (isJumping)
        {
            characterAnimator.SetBool("Jumping",true);
        }
        else
        {
            characterAnimator.SetBool("Jumping", false);
        }
    }

    void OnlandOnGround()
    {
        //Do Something
        //Debug.Log("The Player has Landed");
    }

    void BlastJumpParticles()
    {
        //Make the particle system fire a particle when the player jumps
        theParticleSystem.Emit(1);
    }
    void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
        }
        else
        {

            currentMovement.y += gravity * Time.deltaTime;
        }
    }
    void HandleSpeedModifier()
    {
        if(speedModifierDuration < 0)
        {
            speedModifierDuration = 0.001f;
        }
        speedModifierMagnitude = Mathf.MoveTowards(speedModifierMagnitude, 1, (1/speedModifierDuration) * Time.deltaTime);

    }

    public void PlayerHasBeenHit(float magnitude, float duration)
    {
        speedModifierMagnitude = magnitude;
        speedModifierDuration = duration;
    }

    public void TeleportPlayer(Vector3 coords)
    {
        transform.position = coords;
        Physics.SyncTransforms();
        UnityEngine.Debug.Log(transform.position);
    }
    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }
    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
    }