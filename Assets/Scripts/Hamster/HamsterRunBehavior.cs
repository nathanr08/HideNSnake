using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterRunBehavior : State {

    float runTimer = 0.0f;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        runTimer = 0.0f;
        HamsterController hamsterController = ((HamsterController)controller);
        hamsterController.SetVisibility(true);
        
        // Play the run sound
        if (hamsterController.RunAudio != null && !hamsterController.RunAudio.isPlaying)
        {
            hamsterController.RunAudio.Play();
        }

        if (hamsterController.WalkFastAudio != null && !hamsterController.WalkFastAudio.isPlaying)
        {
            // Stop the walk audio first
            if (hamsterController.WalkAudio != null && hamsterController.WalkAudio.isPlaying)
            {
                hamsterController.WalkAudio.Play();
            }

            hamsterController.WalkFastAudio.PlayDelayed(0.4f);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        HamsterController hamsterController = ((HamsterController)controller);
        runTimer += Time.deltaTime;
        if (runTimer < hamsterController.runTime)
        {
            hamsterController.DoMovement(hamsterController.runSpeed);            
        }
        else
        {
            hamsterController.DoMovement(hamsterController.walkSpeed);
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        HamsterController hamsterController = ((HamsterController)controller);
        hamsterController.StartRunCooldown();
        hamsterController.SetVisibility(false);

        if (hamsterController.WalkFastAudio != null && hamsterController.WalkFastAudio.isPlaying)
        {         
            hamsterController.WalkFastAudio.Stop();
        }
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
