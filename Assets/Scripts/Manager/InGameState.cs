using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameState : State {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GameManager gameManager = (GameManager)controller;
        gameManager.startCountdownText.text = "Go!";
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        GameManager gameManager = (GameManager)controller;
        
        if(gameManager.gameStartCountdown <= -3.0)
        {
            gameManager.startCountdownText.gameObject.SetActive(false);
        }
        else
        {
            gameManager.gameStartCountdown -= Time.deltaTime;
        }

        gameManager.gameTimeRemaining -= Time.deltaTime;
        gameManager.matchTimerText.text = Mathf.Ceil(gameManager.gameTimeRemaining).ToString();
        if (gameManager.gameTimeRemaining <= 0.0f || gameManager.DidSnakeWin())
        {
            if (gameManager.gameTimeRemaining <= 0.0f)
                gameManager.matchResults = GameManager.hamsterPlayer;
            else
                gameManager.matchResults = GameManager.snakePlayer;

            animator.SetTrigger(GameManager.animEndMatchTrigger);
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
