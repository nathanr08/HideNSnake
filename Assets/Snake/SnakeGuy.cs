using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGuy : MonoBehaviour {

	Rigidbody MyRigidBody;
	Animator AnimationController;

	Camera MainCamera;

   [SerializeField]
	float SpeedMultiplier = 100000.0f;

	[SerializeField]
	float SonarDistance = 100000.0f;

	[SerializeField]
	float SonarVisibilyDuration = 4.0f;

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


		if(Input.GetKey(KeyCode.W))
		{
			Move(MainCamera.transform.up, SpeedMultiplier);
		}
		if(Input.GetKey(KeyCode.S))
		{
			Move(-MainCamera.transform.up, SpeedMultiplier);
		}

		if(Input.GetKey(KeyCode.A))
		{
			Move(-MainCamera.transform.right, SpeedMultiplier);
		}

		if(Input.GetKey(KeyCode.D))
		{
			Move(MainCamera.transform.right, SpeedMultiplier);
		}


		//if(Input.GetButtonDown(baseControllable.InputHandles.Action))
		//{
        // Sonar();
		//}

		if(MyRigidBody.velocity.magnitude > 1.0f)
		transform.forward = MyRigidBody.velocity.normalized;

	}

   void Sonar( )
   {
		foreach(GameObject HM in GameObject.FindGameObjectsWithTag("Hamster"))
		{
			HamsterController HMCon = HM.GetComponent<HamsterController>();

			if((HM.transform.position - transform.position).magnitude > SonarDistance)
			{
				HMCon.Freeze();
				HMCon.SetVisibility(true, SonarVisibilyDuration);
			}
			else
				HMCon.SetVisibility(true, SonarVisibilyDuration);
		}
   }

	void Move(Vector3 Direction, float force)
	{
		//MyRigidBody.AddRelativeTorque(new Vector3(0.0f,10.0f,0.0f));
		MyRigidBody.AddForce(Direction * force * Time.deltaTime);

	}
}
