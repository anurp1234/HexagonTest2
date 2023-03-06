using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviourController : StateMachineBehaviour
{
    [SerializeField]
    int numberOfAnimations = 3;

    int currentState;

    [SerializeField]
    int animFrameDelay = 10;


    int frameDelayCounter = 0;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetNextIdleState(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (frameDelayCounter > 0)
        {
            frameDelayCounter--;
        }
        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            Debug.LogWarning("Changing animation state"+ stateInfo.normalizedTime);
            SetNextIdleState(animator);
        }
    }


    void SetNextIdleState(Animator animator)
    {
        currentState++;
        currentState %= numberOfAnimations;
        Debug.LogWarning("Current state = "+ currentState);
        animator.SetFloat("Blend", (float)currentState);
        frameDelayCounter = animFrameDelay;
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
