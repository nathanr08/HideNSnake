using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpTheCharacterJoints : MonoBehaviour {

	Rigidbody ParentRBody;

	// Use this for initialization
	void Start () {

		GameObject parent = gameObject;
		GameObject child = parent.transform.GetChild(0).gameObject;
		CharacterJoint ChildCJoint;
		Rigidbody ChildRBody;

		while(child != null)
		{
			ParentRBody = parent.GetComponent<Rigidbody>();

			ChildRBody=child.AddComponent<Rigidbody>();
			ChildCJoint=child.AddComponent<CharacterJoint>();

			ChildCJoint.connectedBody = ParentRBody;
			ChildRBody.constraints = ParentRBody.constraints;

			ChildRBody.useGravity = false;

			parent = child;

			if(parent)
				child = parent.transform.GetChild(0).gameObject;
			else
				break;

		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
