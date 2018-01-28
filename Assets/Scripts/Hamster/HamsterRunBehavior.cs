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

        if (hamsterController.WalkFastAudio != null && !hamsterController.WalkFastAudio.isPlaying)
        {
            // Stop the walk audio first
            if (hamsterController.WalkAudio != null && hamsterController.WalkAudio.isPlaying)
            {
                hamsterController.WalkAudio.Stop();
            }

            hamsterController.WalkFastAudio.PlayDelayed(0.4f);
        }

        // Play the run sound
        if (hamsterController.RunAudio != null && !hamsterController.RunAudio.isPlaying)
        {
            hamsterController.RunAudio.Play();
        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
        HamsterController hamsterController = ((HamsterController)controller);
        runTimer += Time.deltaTime;
        Debug.Log("@@@@@@@@@@@@@@@" + runTimer);
        if (runTimer <= 1.5)
        {
            float MoveSpeed = Mathf.Lerp(hamsterController.walkSpeed, hamsterController.runSpeed, runTimer / 1.5f);
            hamsterController.DoMovement(MoveSpeed);
            Debug.Log("@@@@@@@@@@@@@@@" + MoveSpeed);
        }
       
        if (runTimer < hamsterController.runTime)
        {
            hamsterController.DoMovement(hamsterController.runSpeed);

            // Play the wind down sound
            if (hamsterController.runTime - runTimer <= 1.2)
            {
                hamsterController.DoMovement(Mathf.Lerp(hamsterController.runSpeed, hamsterController.walkSpeed, runTimer / hamsterController.runTime));
                if (hamsterController.runTime - runTimer > 0.4 && hamsterController.RunAudioEnd != null && !hamsterController.RunAudioEnd.isPlaying)
                {
                    hamsterController.RunAudioEnd.PlayDelayed(0.1f);
                }
            }            
            
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
