using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private const float LANE_DISTANCE = 3.0f;
	private const float TURN_SPEED = 0.05f;

	private bool IsRunning = false;

	//Animation
	private Animator anim;

	//movemenet
	private CharacterController controller;
	private float jumpForce = 4.0f;
	private float gravity = 12.0f;
	private float verticalVelocity;
	private float speed = 7.0f;
	private int desiredLane = 1; // 0 left , 1 middle , 2 right 
	private void Start(){
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();


	}

	private void Update(){

		if (! IsRunning) {
			return;
		}
		// Gather inputs on which lane we should be 
		if (MobileInputs.Instance.SwipeLeft) 
			//move left 
			MoveLane(false);
		if (MobileInputs.Instance.SwipeRight) 
			//move right
			MoveLane(true);


		// calculate where we should be in the future

		Vector3 targetPosition = transform.position.z * Vector3.forward;
		if (desiredLane == 0)
			//vector 3.let Shorthand for writing Vector3(-1, 0, 0)
			//vector 3.right Shorthand for writing Vector3(1, 0, 0)
			targetPosition += Vector3.left * LANE_DISTANCE;
		else if (desiredLane == 2)
			targetPosition += Vector3.right * LANE_DISTANCE;


		// let's calculate our move delta 

		Vector3 moveVector = Vector3.zero;

		moveVector.x = (targetPosition - transform.position).normalized.x * speed;

		bool isGrounded = IsGrounded ();
		anim.SetBool ("Grounded", isGrounded);

		//calculate Y
		if (isGrounded) 
		{ // if grounded 
			//verticalVelocity = 0.0f;

			if (MobileInputs.Instance.SwipeUp) {
				//jump
				anim.SetTrigger("Jump");
				verticalVelocity = jumpForce;
			}

		} 
		else 
		{
			verticalVelocity -= (gravity * Time.deltaTime);


			//fast falling mechanic 
			if (MobileInputs.Instance.SwipeDown) 
			{
				verticalVelocity = -jumpForce;
			}
		}

		moveVector.y = verticalVelocity;
		moveVector.z = speed;

		//move penguin

		controller.Move (moveVector * Time.deltaTime);

		// Rotate penguin to wher is he going

		Vector3 dir = controller.velocity;
		if (dir != Vector3.zero) 
		{
			dir.y = 0;
			transform.forward = Vector3.Lerp (transform.forward, dir, TURN_SPEED);
		}





	}

	private void MoveLane(bool goingRight){
		desiredLane += (goingRight) ? 1 : -1;
		desiredLane = Mathf.Clamp (desiredLane, 0,2);

	}


	private bool IsGrounded()
	{
		Ray groundRay = new Ray (
			new Vector3 (controller.bounds.center.x,(controller.bounds.center.y - controller.bounds.extents.y) + 0.2f,controller.bounds.center.z),
			Vector3.down);

		Debug.DrawRay (groundRay.origin, groundRay.direction, Color.cyan, 1.0f);

		return (Physics.Raycast (groundRay, 0.2f + 1.0f));
	}

	public void StartRunning(){
		IsRunning = true;
	}

}
