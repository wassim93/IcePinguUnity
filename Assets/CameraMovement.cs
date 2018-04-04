﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public Transform lookAt; // our penguin  object looking at 
	public Vector3 offset = new Vector3(0,5.0f,-10.0f);

	// Use this for initialization
	private void Start () {
		transform.position = lookAt.position + offset;
	}

	// Update is called once per frame
	void Update () {

	}

	// we use late update to make sure that camear move after player movment
	private void LateUpdate(){
		Vector3 desiredPosition = lookAt.position + offset;
		desiredPosition.x = 0;
		transform.position = Vector3.Lerp (transform.position, desiredPosition, Time.deltaTime);

	}
}
