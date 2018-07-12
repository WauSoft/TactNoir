using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Editor adjustable properties
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float crouchHeight;
    [SerializeField] float crouchSpeed;
    [SerializeField] float jumpHeight;
    
    // Private variables
    private bool CanJump;
    private bool IsMoving;
    private bool IsCrouching;
    private bool HeadHit;
    private bool IsRunning;
    private bool ToggleRun;
    private bool ToggleCrouch;

    private float moveSpeed;

    private Vector3 moveDirection = Vector3.zero;
    public Animator animator;
    InputController playerInput;

    // Reference the component CharacterController
    private Rigidbody m_rigidbody;
    public Rigidbody playerRB
    {
        get
        {
            if (m_rigidbody == null)
                m_rigidbody = GetComponent<Rigidbody>();
            return m_rigidbody;
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
        if (!PauseMenu.IsPaused)
        {
            Move();
            Actions();
            Animations();
        }
	}

    // Movement properties
    void Move()
    {
        // Ready LT to button
        if (playerInput.JoyReadyItem == 1f && !playerInput.ReadyItem)
            playerInput.ReadyItem = true;
        if (playerInput.JoyReadyItem < 1f && !playerInput.ReadyItem)
            playerInput.ReadyItem = false;

        // MOVEMENT
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
        moveSpeed = walkSpeed;
        if (playerInput.Run && !IsCrouching) // When run button is pressed
            if(!ToggleRun)
                moveSpeed = runSpeed; // Set run speed
            if (ToggleRun)
                moveSpeed = walkSpeed; // Walks if run button is used while run is toggled
            if (IsCrouching)
                moveSpeed = crouchSpeed; // Cancels out the run call if crouched
        if (playerInput.Crouch && CanJump) // When crouch button is pressed
            moveSpeed = crouchSpeed; // Set crouch speed
        if (ToggleRun && !playerInput.Run) // Makes sure the player isn't running while crouched
            if(!IsCrouching)
            moveSpeed = runSpeed;
        if (!IsCrouching && HeadHit && CanJump) // If currently under crouch roof, keep the speed to crouch regardless
            moveSpeed = crouchSpeed;

        // Calculate move direction
        moveDirection = Vector3.zero;
        moveDirection = (transform.forward * playerInput.Vertical) + (transform.right * playerInput.Horizontal);
        moveDirection = moveDirection.normalized * moveSpeed;

        // Value for Raycast direction
        Vector3 up = transform.TransformDirection(Vector3.up);

        // Raycast checks for overhead collision
        if (Physics.Raycast(transform.position, up, 2f))
            HeadHit = true;
        else
            HeadHit = false;

        //JUMP
        // Value for Raycast ground check direction
        Vector3 down = transform.TransformDirection(Vector3.down);

        // Raycast checks for ground collision to see if player can jump - (not called in Fixed update due to being an instantaneous movement)
        if (Physics.Raycast(transform.position, down, 1.4f) && !HeadHit)
            CanJump = true;
        else
            CanJump = false;

        // Jump function
        if (playerInput.Jump && CanJump)
        {
            playerRB.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        // CROUCHING
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

        /*
        // Scales the capsule of the CharacterController component when crouched
        if (IsCrouching)
            playerRB.height = crouchHeight;
        if (!IsCrouching && !HeadHit)
            playerRB.height = controllerHeight;
        */
    }

    private void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void Actions()
    {
        // Other actions besides movement here
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        // Raycast checks for touching
        if (Physics.Raycast(transform.position, fwd, 2f))
            Debug.Log("Touching");

    }

    void Animations()
    {
        animator.SetBool("IsAiming", playerInput.ReadyItem);
    }

}
