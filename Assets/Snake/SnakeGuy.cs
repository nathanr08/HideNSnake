using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGuy : MonoBehaviour {

	Rigidbody MyRigidBody;
	Animator AnimationController;

	Camera MainCamera;

	float SpeedMultiplier = 1000.0f;

	// Use this for initialization
	void Start () {
		MyRigidBody = GetComponent<Rigidbody>();
		AnimationController = GetComponent<Animator>();

		MainCamera = GameObject.FindObjectOfType<Camera>();


	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.W))
		{
			Move(MainCamera.transform.up, 1.0f * SpeedMultiplier);
		}

		if(Input.GetKey(KeyCode.D))
		{
			Move(MainCamera.transform.right, 1.0f * SpeedMultiplier);
		}

		if(Input.GetKey(KeyCode.A))
		{
			Move(-MainCamera.transform.right, 1.0f * SpeedMultiplier);
		}

		if(Input.GetKey(KeyCode.S))
		{
			Move(-MainCamera.transform.up, 1.0f * SpeedMultiplier);
		}

	}

	void Move(Vector3 Direction, float force)
	{
		//MyRigidBody.AddRelativeTorque(new Vector3(0.0f,10.0f,0.0f));
		MyRigidBody.AddForce(Direction * force);
		transform.forward = MyRigidBody.velocity.normalized;

	}
}
