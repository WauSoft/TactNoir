using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    // Axis input values
    public float Vertical;
    public float Horizontal;
    public Vector2 MouseInput;

    // Action input bools
    public bool Jump;
    public bool Run;
    public bool Crouch;
    public bool ToggleRun;
    public bool ToggleCrouch;
	
	// Update is called once per frame
	void Update ()
    {
        // Axis inputs
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        // Actions
        Jump = Input.GetButtonDown("Jump");
        Run = Input.GetButton("Run");
        ToggleRun = Input.GetButtonDown("ToggleRun");
        Crouch = Input.GetButton("Crouch");
        ToggleCrouch = Input.GetButtonDown("ToggleCrouch");
	}

}
