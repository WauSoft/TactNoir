using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCamera : MonoBehaviour {

    //Editor adjustable values
    [SerializeField] float xSensitivity;
    [SerializeField] float ySensitivity;
    [SerializeField] float maxViewAngle;
    [SerializeField] float minViewAngle;

    public Vector3 offset;
    public Transform target; //Target for camera to affix itself to
    public Transform pivot; //Pivot that the camera rotates around

    public bool LockMouse;

    //Initialization
    void Start ()
    {
        //Sets the offset to the target
        offset = target.position - transform.position;
        //Hides cursor and locks mouse to window when the game starts
        if (LockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
	}
	
	void Update ()
    {
        // Rotate player and camera with the x movement of the mouse
        float horizontal = Input.GetAxis("Mouse X") * xSensitivity;
        target.Rotate(0f, horizontal, 0f);

        // Rotate camera with y movement of mouse
        float vertical = Input.GetAxis("Mouse Y") * ySensitivity;
        pivot.Rotate(-vertical, 0f, 0f);

        // Vertical look limit UP (Not entirely functional - camera inverts when it exceeds limit)
        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0f, 0f);
            transform.rotation = Quaternion.Euler(maxViewAngle, 0f, 0f);
        }
        // Vertical look limit DOWN (Not entirely functional - camera inverts when it exceeds limit)
        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0f, 0f);
            transform.rotation = Quaternion.Euler(360f + minViewAngle, 0f, 0f);
        }

        // Sets desired angles for target and pivot
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;

        // Applies the values in Quaternion Euler (X,Y,Z) axes
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0f);

        // Applies the Quaternion rotations to desired objects
        transform.position = target.position - (rotation * offset);
        transform.rotation = rotation;
	}

}
