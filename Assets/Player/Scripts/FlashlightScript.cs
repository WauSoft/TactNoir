using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour {

    [SerializeField] float lightIntensity;

    public bool LightOn;
    Light gunLightComponent;

    InputController playerInput;


    // Use this for initialization
	void Start ()
    {
        playerInput = GameManager.Instance.InputController;
        gunLightComponent = GetComponent<Light>();
    }


	// Update is called once per frame
	void Update ()
    {
        // Toggle Flashlight
        if (playerInput.Flashlight)
        {
            if (!LightOn)
            {
                LightOn = true;
            }
            else
            {
                LightOn = false;
            }
        }

        if (gunLightComponent != null)
        {
            if (LightOn)
            {
                gunLightComponent.intensity = lightIntensity;
            }
            else
            {
                gunLightComponent.intensity = 0f;
            }
        }

    }
}
