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
    public bool Flashlight;
    public bool ReadyItem;
    public bool Pause;
    public float JoyCrouch;
    public float JoyReadyItem;

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
        JoyCrouch = Input.GetAxis("Crouch");
        ToggleCrouch = Input.GetButtonDown("ToggleCrouch");
        Flashlight = Input.GetButtonDown("Flashlight");
        ReadyItem = Input.GetButton("Ready");
        JoyReadyItem = Input.GetAxis("Ready");
        Pause = Input.GetButtonDown("Pause");
        
        // Note: "GetButtonDown" = actions per button press | while "GetButton" = actions fire when button is held
	}

}
