using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterIdleBehavior : State {

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        // Hack, if the game has not started, bail
        if (GameManager.Instance == null || !GameManager.Instance.gameInProgress)
        {
            return;
        }

        HamsterController hamsterController = ((HamsterController)controller);
        hamsterController.CheckRun();
        hamsterController.DoMovement(hamsterController.walkSpeed);

        // Stop moving sounds
        if (hamsterController.WalkAudio != null && hamsterController.WalkAudio.isPlaying)
        {
            hamsterController.WalkAudio.Stop();            
        }

        // Stop moving sounds
        if (hamsterController.RunAudio != null && hamsterController.RunAudio.isPlaying)
        {
            hamsterController.RunAudio.Stop();
        }
    }
}
