using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpTheCharacterJoints : MonoBehaviour {

	HingeJoint ExampleCJoint;
	Rigidbody ParentRBody;
	SphereCollider SpCol;

	int skipamount = 1;
	// Use this for initialization
	void Start () {
		GameObject loopparent = gameObject;
		GameObject child = gameObject.transform.GetChild(0).gameObject;
		HingeJoint ChildCJoint;
		Rigidbody ChildRBody;

		SpCol = GetComponent<SphereCollider>();
		ExampleCJoint = loopparent.GetComponent<HingeJoint>();

		//int count = 0;
		while(child != null)
		{

			loopparent.tag = "Snake";

			//++count;
			ParentRBody = loopparent.GetComponent<Rigidbody>();

			ChildRBody=child.AddComponent<Rigidbody>();
			ChildCJoint=child.AddComponent<HingeJoint>();

			if(SpCol)
			{
				SphereCollider temp = child.AddComponent<SphereCollider>();
				temp.radius = SpCol.radius;
				temp.center = SpCol.center;
				temp.isTrigger = SpCol.isTrigger;
			}

			ChildRBody.mass = ParentRBody.mass;
			
			ChildRBody.useGravity = ParentRBody.useGravity;
			ChildRBody.isKinematic = ParentRBody.isKinematic;
			ChildRBody.constraints = ParentRBody.constraints;
			ChildRBody.drag = ParentRBody.drag;
			ChildRBody.angularDrag = ParentRBody.angularDrag;

			//HindgeJOINTS

			ChildCJoint.useLimits = ExampleCJoint.useLimits;
			ChildCJoint.useMotor = ExampleCJoint.useMotor;
			ChildCJoint.useSpring = ExampleCJoint.useSpring;
			ChildCJoint.spring = ExampleCJoint.spring;
			ChildCJoint.motor = ExampleCJoint.motor;
			ChildCJoint.limits = ExampleCJoint.limits;
			ChildCJoint.enablePreprocessing = ExampleCJoint.enablePreprocessing;
			ChildCJoint.enableCollision = ExampleCJoint.enableCollision;
			ChildCJoint.massScale = ExampleCJoint.massScale;
			ChildCJoint.connectedMassScale = ExampleCJoint.connectedMassScale;
			ChildCJoint.axis = ExampleCJoint.axis;
			ChildCJoint.anchor = ExampleCJoint.anchor;
			ChildCJoint.autoConfigureConnectedAnchor = ExampleCJoint.autoConfigureConnectedAnchor;
			ChildCJoint.connectedAnchor = ExampleCJoint.connectedAnchor;


			// ConfigJOINTS
			//ChildCJoint.angularXDrive = ExampleCJoint.angularXDrive;
			//ChildCJoint.angularXLimitSpring = ExampleCJoint.angularXLimitSpring;
			//ChildCJoint.angularXMotion = ExampleCJoint.angularXMotion;
			//ChildCJoint.angularYLimit = ExampleCJoint.angularYLimit;
			//ChildCJoint.angularYMotion = ExampleCJoint.angularYMotion;
			//ChildCJoint.angularYZDrive = ExampleCJoint.angularYZDrive;
			//ChildCJoint.angularYZLimitSpring = ExampleCJoint.angularYZLimitSpring;
			//ChildCJoint.angularZLimit = ExampleCJoint.angularZLimit;
			//ChildCJoint.angularZMotion = ExampleCJoint.angularZMotion;
			//
			//ChildCJoint.lowAngularXLimit = ExampleCJoint.lowAngularXLimit;
			//ChildCJoint.highAngularXLimit = ExampleCJoint.highAngularXLimit;
			//
			//ChildCJoint.rotationDriveMode = ExampleCJoint.rotationDriveMode;
			//
			//ChildCJoint.projectionAngle = ExampleCJoint.projectionAngle;
			//ChildCJoint.projectionDistance = ExampleCJoint.projectionDistance;
			//ChildCJoint.projectionMode = ExampleCJoint.projectionMode;
			//
			//ChildCJoint.xDrive = ExampleCJoint.xDrive;
			//ChildCJoint.yDrive = ExampleCJoint.yDrive;
			//ChildCJoint.zDrive = ExampleCJoint.zDrive;
			//
			//ChildCJoint.xMotion = ExampleCJoint.xMotion;
			//ChildCJoint.yMotion = ExampleCJoint.yMotion;
			//ChildCJoint.zMotion = ExampleCJoint.zMotion;
			//
			//ChildCJoint.linearLimit = ExampleCJoint.linearLimit;
			//ChildCJoint.linearLimitSpring = ExampleCJoint.linearLimitSpring;
			//
			//ChildCJoint.lowAngularXLimit = ExampleCJoint.lowAngularXLimit;
			//ChildCJoint.linearLimitSpring = ExampleCJoint.linearLimitSpring;
			//ChildCJoint.linearLimitSpring = ExampleCJoint.linearLimitSpring;
			//
			//ChildCJoint.enableCollision = ExampleCJoint.enableCollision;
			//ChildCJoint.enablePreprocessing = ExampleCJoint.enablePreprocessing;
			//ChildCJoint.configuredInWorldSpace = ExampleCJoint.configuredInWorldSpace;
			//
			//ChildCJoint.slerpDrive = ExampleCJoint.slerpDrive;



			//CharacterJOINT
			//ChildCJoint.anchor = ExampleCJoint.anchor;
			//ChildCJoint.axis = ExampleCJoint.axis;
			//ChildCJoint.connectedAnchor = ExampleCJoint.connectedAnchor;
			//ChildCJoint.swingAxis = ExampleCJoint.swingAxis;
			//ChildCJoint.swing1Limit = ExampleCJoint.swing1Limit;
			//ChildCJoint.swing2Limit = ExampleCJoint.swing2Limit;
			//ChildCJoint.swingLimitSpring = ExampleCJoint.swingLimitSpring;
			//ChildCJoint.twistLimitSpring = ExampleCJoint.twistLimitSpring;
			//ChildCJoint.lowTwistLimit = ExampleCJoint.lowTwistLimit;
			//ChildCJoint.highTwistLimit = ExampleCJoint.highTwistLimit;
			//ChildCJoint.projectionAngle = ExampleCJoint.projectionAngle;
			//ChildCJoint.projectionDistance = ExampleCJoint.projectionDistance;
			//ChildCJoint.enablePreprocessing = ExampleCJoint.enablePreprocessing;
			//ChildCJoint.massScale = ExampleCJoint.massScale;
			//ChildCJoint.connectedMassScale = ExampleCJoint.connectedMassScale;


			ChildCJoint.connectedBody = ParentRBody;
			int count = 0;

			loopparent.transform.SetParent(null);

			GameObject SkipParent = loopparent;
			loopparent = child;

			while(child && count < skipamount)
			{
				++count;
				SkipParent = child;

			
				if(SkipParent)
				{
					if(SkipParent.transform.childCount <= 0)
					{
						child = null;
						break;
					}
					child = SkipParent.transform.GetChild(0).gameObject;
				}
				else
				{
					child = null;
					break;
				}

			}
		}



		//Destroy(ExampleCJoint);

		//ExampleCJoint = gameObject.transform.GetChild(0).gameObject.GetComponent<HingeJoint>();

	}


	
	// Update is called once per frame
	void Update () {
		return;

		GameObject parent = gameObject;

		GameObject child = parent.transform.GetChild(0).gameObject;

		HingeJoint ChildCJoint;
		Rigidbody ChildRBody;

		//int count = 0;
		while(child != null)
		{

			//++count;
			ParentRBody = parent.GetComponent<Rigidbody>();

			ChildRBody=child.GetComponent<Rigidbody>();
			ChildCJoint=child.GetComponent<HingeJoint>();

			ChildRBody.mass = ParentRBody.mass;

			ChildRBody.useGravity = ParentRBody.useGravity;
			ChildRBody.isKinematic = false;
			ChildRBody.constraints = ParentRBody.constraints;
			ChildRBody.drag = ParentRBody.drag;
			ChildRBody.angularDrag = ParentRBody.angularDrag;


			//HindgeJOINTS

			//ChildCJoint.useLimits = ExampleCJoint.useLimits;
			//ChildCJoint.useMotor = ExampleCJoint.useMotor;
			//ChildCJoint.useSpring = ExampleCJoint.useSpring;
			//ChildCJoint.spring = ExampleCJoint.spring;
			//ChildCJoint.motor = ExampleCJoint.motor;
			//ChildCJoint.limits = ExampleCJoint.limits;
			//ChildCJoint.enablePreprocessing = ExampleCJoint.enablePreprocessing;
			//ChildCJoint.enableCollision = ExampleCJoint.enableCollision;
			//ChildCJoint.axis = ExampleCJoint.axis;
			//ChildCJoint.massScale = ExampleCJoint.massScale;
			//ChildCJoint.connectedMassScale = ExampleCJoint.connectedMassScale;
			//ChildCJoint.anchor = ExampleCJoint.anchor;
			//ChildCJoint.connectedBody = ParentRBody;

			parent = child;

			if(parent)
			{
				if(parent.transform.childCount <= 0)
					break;
				child = parent.transform.GetChild(0).gameObject;
			}
			else
				break;

		}


		
	}
}
