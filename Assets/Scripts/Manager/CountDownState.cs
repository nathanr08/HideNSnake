using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownState : State {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GameManager gameManager = (GameManager)controller;
        // set timers
        gameManager.gameTimeRemaining = gameManager.gameLength;
        gameManager.gameStartCountdown = gameManager.gameStartCountdownLength;
        gameManager.startCountdownText.gameObject.SetActive(true);
        gameManager.startCountdownText.text = Mathf.Ceil(gameManager.gameStartCountdown).ToString();
        gameManager.matchTimerText.text = Mathf.Ceil(gameManager.gameTimeRemaining).ToString();
        gameManager.matchTimerText.gameObject.SetActive(true);
        // disable input
        gameManager.SetPlayerInput(false);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GameManager gameManager = (GameManager)controller;
        gameManager.gameStartCountdown -= Time.deltaTime;
        gameManager.startCountdownText.text = Mathf.Ceil(gameManager.gameStartCountdown).ToString();
        if(gameManager.gameStartCountdown <= 0.0f)
        {
            gameManager.startCountdownText.text = "Go!";
            animator.SetTrigger(GameManager.animStartMatchTrigger);
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GameManager gameManager = (GameManager)controller;
        // hide intro countdown timer
        gameManager.startCountdownText.gameObject.SetActive(false);

        gameManager.SetPlayerInput(true);
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
