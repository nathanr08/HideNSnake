using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// initializes the state system
/// </summary>
public class StateController : MonoBehaviour {

    public Animator animator;
    public State currState;

	// Use this for initialization
	protected virtual void Start () {
        if (animator == null)
            animator = GetComponent<Animator>();

        // set our stateBehaviours reference
        StateMachineBehaviour[] stateBehaviours = animator.GetBehaviours<StateMachineBehaviour>();
        foreach (State stateBehaviour in stateBehaviours)
        {
            stateBehaviour.controller = this;
        }
    }

}
