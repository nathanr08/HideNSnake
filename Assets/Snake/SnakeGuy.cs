using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGuy : MonoBehaviour {

	Rigidbody MyRigidBody;
	Animator AnimationController;

	Camera MainCamera;

   [SerializeField]
	float SpeedMultiplier = 100000.0f;

   BaseControllable baseControllable;

	// Use this for initialization
	void Start () {
		MyRigidBody = GetComponent<Rigidbody>();
		AnimationController = GetComponent<Animator>();

		MainCamera = GameObject.FindObjectOfType<Camera>();

      baseControllable = GetComponent<BaseControllable>();
	}
	
	// Update is called once per frame
	void Update () {
		
      Move(MainCamera.transform.up, SpeedMultiplier * Input.GetAxis(baseControllable.InputHandles.VerticalAxis));
      Move(MainCamera.transform.right, SpeedMultiplier * Input.GetAxis(baseControllable.InputHandles.HorizontalAxis));
		if(Input.GetButtonDown(baseControllable.InputHandles.Action))
		{
         Sonar();
		}
	}

   void Sonar( )
   {

   }

	void Move(Vector3 Direction, float force)
	{
		//MyRigidBody.AddRelativeTorque(new Vector3(0.0f,10.0f,0.0f));
		MyRigidBody.AddForce(Direction * force);
		transform.forward = MyRigidBody.velocity.normalized;

	}
}
