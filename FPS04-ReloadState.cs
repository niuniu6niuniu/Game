using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : StateMachineBehaviour
{
    public float reloadTime = 0.8f;   // When we do reload after the animation playing(70% of the animation)
    bool hasReloaded = false;   // Consider it is reload or not, we only want to reload once

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hasReloaded = false;   // Not reload when enter the playing of the reloading animation
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (hasReloaded) return;   // If has reloaded then return

        if (stateInfo.normalizedTime >= reloadTime)  // If animation has played over the setting time
        {
            animator.GetComponent<Weapon>().Reload();   // Fetch the Reload function from the Weapon script and execute
            hasReloaded = true;   // Set the reload sign to true
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hasReloaded = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
