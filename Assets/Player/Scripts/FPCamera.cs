using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPCamera : MonoBehaviour {

    // Editor adjustable values
    //[SerializeField] float xSensitivity;
    //[SerializeField] float ySensitivity;
    [SerializeField] float maxViewAngle;
    [SerializeField] float minViewAngle;
    [SerializeField] bool InvertY;

    public Vector3 offset;
    public Transform target; // Target for camera to affix itself to
    public Transform pivot; // Pivot that the camera rotates around

    //public bool LockMouse;
    InputController playerInput;
    public Slider sensitivitySlider;

    public float LookSensitivity { get; set; }

    // Initialization
    void Start ()
    {
        // Sets the offset to the target
        offset = target.position - transform.position;
        playerInput = GameManager.Instance.InputController;
        LookSensitivity = 3f;
	}
	
	void Update ()
    {
        if(Time.timeScale != 0)
        MouseLook();
    }

    void MouseLook()
    {
        // Rotate player and camera with the x movement of the mouse
        float horizontal = playerInput.MouseInput.x * LookSensitivity;
        target.Rotate(0f, horizontal, 0f);

        // Rotate camera with y movement of mouse
        float vertical = playerInput.MouseInput.y * LookSensitivity;
        if (InvertY)
            pivot.Rotate(vertical, 0f, 0f);
        else
            pivot.Rotate(-vertical, 0f, 0f);

        // Vertical look limit UP
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, transform.rotation.eulerAngles.y, 0);
        }
        // Vertical look limit DOWN
        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, transform.rotation.eulerAngles.y, 0);
        }

        // Sets desired angles for target and pivot
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;

        // Applies the values in Quaternion Euler (X,Y,Z) axes
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0f);

        // Applies the Quaternion rotations to desired objects
        transform.position = pivot.position;
        transform.rotation = rotation;

    }

    public void InvertLook()
    {
        if (InvertY)
        {
            InvertY = false;
            return;
        }
        if (!InvertY)
        {
            InvertY = true;
        }
    }


}
