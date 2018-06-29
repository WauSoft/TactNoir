using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Editor adjustable properties
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravityScale;
    [SerializeField] float crouchHeight;
    [SerializeField] float crouchSpeed;
    
    //Private values
    private bool CanJump;
    private bool IsMoving;
    private bool IsCrouching;
    private Vector3 moveDirection;
    private float controllerHeight = 1.8f;

    InputController playerInput;

    //Reference the component CharacterController
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

	//Initialization
	void Awake ()
    {
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
        //Move speeds
        float moveSpeed = walkSpeed;
        if (playerInput.Run && !IsCrouching)
            moveSpeed = runSpeed;
        if (playerInput.Crouch && controller.isGrounded)
            moveSpeed = crouchSpeed;
        
        //CharacterController move direction based on input
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * GameManager.Instance.InputController.Vertical) + (transform.right * GameManager.Instance.InputController.Horizontal);
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        //Jump function - uses the yStore to calculate Y movement
        CanJump = (controller.isGrounded);

        if (CanJump)
        {
            moveDirection.y = 0f;
            if (playerInput.Jump)
            {
                moveDirection.y = jumpForce;
            }
        }
        //Jump physics calculation
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);

        //Toggle Crouch function and scales the capsule of the CharacterController component
        if (playerInput.Crouch)
            if (!IsCrouching)
            {
                controller.height = crouchHeight;
                IsCrouching = true;
            }
            else
            {
                controller.height = controllerHeight;
                IsCrouching = false;
            }

    }

}
