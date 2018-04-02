using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMoveScript : MonoBehaviour {
private bool touchingCollider = false;

public GameObject FPSController;
public GameObject Elevator;
public GameObject M_Corp_Elevator;

	Animator anim;
	
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (touchingCollider) 
		{
			M_Corp_Elevator.GetComponent<Animation>().Play("Elevator_Move");
			Debug.Log ("Object Entered the trigger");
			}
		}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "ProximityTrigger") 
		{
			touchingCollider = true;
			}
		}

	void OnTriggerExit(Collider other) 
	{
		if (other.tag == "ProximityTrigger") 
		{
			touchingCollider = false;
			}
		}
	}
