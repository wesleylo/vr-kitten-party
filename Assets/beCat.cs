using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beCat : StateMachineBehaviour {

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex) {
        
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //    
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex) {
        int rndState = Random.Range(0, 5);
        if (rndState == 4) anim.SetBool("isIdle", true); // Exit state machine
        anim.SetInteger("beingCatState", rndState);
        Debug.Log("State Exit = " + anim.GetInteger("beingCatState"));
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMachineEnter is called when entering a statemachine via its Entry Node
    override public void OnStateMachineEnter(Animator anim, int stateMachinePathHash){
        anim.SetBool("isIdle", false);
        int rndState = Random.Range(0, 5);
        if (rndState == 4) rndState = 0; // More likely to go to idl
        anim.SetInteger("beingCatState", rndState);
        Debug.Log("StateMachine Enter = " + anim.GetInteger("beingCatState"));
    }

	// OnStateMachineExit is called when exiting a statemachine via its Exit Node
	//override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
	//
	//}
}
