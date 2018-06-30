using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Editor adjustable properties
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravityScale;
    [SerializeField] float crouchHeight;
    [SerializeField] float crouchSpeed;
    
    // Private values
    public bool CanJump;
    private bool IsMoving;
    private bool IsCrouching;
    private bool HeadHit;
    public bool IsRunning;
    public bool ToggleRun;
    public bool ToggleCrouch;
    private Vector3 moveDirection;
    private float controllerHeight = 2f;

    InputController playerInput;

    // Reference the component CharacterController
    private CharacterController m_controller;
    public CharacterController controller
    {
        get
        {
            if (m_controller == null)
                m_controller = GetComponent<CharacterController>();
            return m_controller;
        }
    }

	// Initialization
	void Awake ()
    {
        // Gets the input functions from InputController.cs
        playerInput = GameManager.Instance.InputController;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
	}

    // Movement properties
    void Move()
    {
        // Converts Joycon trigger axis to button equivalent
        if (playerInput.JoyCrouch == 1f && !playerInput.Crouch)
            playerInput.Crouch = true;
        if (playerInput.JoyCrouch < 1f && !playerInput.Crouch)
            playerInput.Crouch = false;
        
        // Toggles between default run or default walk
        if (playerInput.ToggleRun)
        {
            if (!ToggleRun)
            {
                ToggleRun = true;
            }
            else
            {
                ToggleRun = false;
            }
        }
        // Move speeds with run toggle
        float moveSpeed = walkSpeed; // Default movement
        if (playerInput.Run && !IsCrouching) // When run button is pressed
            if(!ToggleRun)
                moveSpeed = runSpeed; // Set run speed
            if (ToggleRun)
                moveSpeed = walkSpeed; // Walks if run button is used while run is toggled
            if (IsCrouching)
                moveSpeed = crouchSpeed; // Cancels out the run call if crouched
        if (playerInput.Crouch && controller.isGrounded) // When crouch button is pressed
            moveSpeed = crouchSpeed; // Set crouch speed
        if (ToggleRun && !playerInput.Run) // Makes sure the player isn't running while crouched
            if(!IsCrouching)
            moveSpeed = runSpeed;
        if (!IsCrouching && HeadHit && CanJump) // If currently under crouch roof, keep the speed to crouch regardless
            moveSpeed = crouchSpeed;
        
        // CharacterController move direction based on input
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * GameManager.Instance.InputController.Vertical) + (transform.right * GameManager.Instance.InputController.Horizontal);
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;


        // Jump function - uses the yStore to calculate Y movement
        CanJump = (controller.isGrounded);

        if (CanJump && !HeadHit)
        {
            moveDirection.y = 0f;
            if (playerInput.Jump)
            {
                moveDirection.y = jumpForce;
            }
        }
        // Jump physics calculation
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);

        // Toggle crouch by default
        if (playerInput.ToggleCrouch && !playerInput.Crouch)
        {
            if (!ToggleCrouch)
            {
                ToggleCrouch = true;
            }
            else
            {
                ToggleCrouch = false;
            }
        }

        // Hold to crouch
        if (playerInput.Crouch && !playerInput.ToggleCrouch)
            IsCrouching = true;
        if (!playerInput.Crouch && !playerInput.ToggleCrouch)
            IsCrouching = false;
        // Toggle to crouch
        if (ToggleCrouch && !playerInput.Crouch)
            IsCrouching = true;
        if (!ToggleCrouch && !playerInput.Crouch)
            IsCrouching = false;

        // Value for Raycast direction
        Vector3 up = transform.TransformDirection(Vector3.up);
        
        // Raycast checks for overhead collision
        if (Physics.Raycast(transform.position, up, 2f))
            HeadHit = true;
        else
            HeadHit = false;

        // Scales the capsule of the CharacterController component when crouched
        if (IsCrouching)
            controller.height = crouchHeight;
        if (!IsCrouching && !HeadHit)
            controller.height = controllerHeight;

    }

}
