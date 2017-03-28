using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beCat : StateMachineBehaviour {
    public AudioClip sound;
    public AudioSource soundSource;
    private int rndState;
    private bool soundPlayed;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex) {
        soundSource.PlayOneShot(sound); // doesn't work, make seperate script to play audio.
        if (anim.GetInteger("beingCatState") > 0) //==3
        {
            if (soundPlayed == false)
            {
                soundSource.PlayOneShot(sound);
                soundPlayed = true;
            }
        }
        Debug.Log("State Enter");
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //    
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator anim, AnimatorStateInfo stateInfo, int layerIndex) {
        soundPlayed = false;
        Debug.Log("State Exit");
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
    override public void OnStateMachineEnter(Animator anim, int stateMachinePathHash) {
        soundSource = anim.GetComponentInParent<AudioSource>();
        soundPlayed = false;
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("beingCat", true);
        rndState = Random.Range(0, 5);
        if (rndState == 4) rndState = 0; // More likely to go to idle
        anim.SetInteger("beingCatState", rndState);
        Debug.Log("StateMachine Enter = " + anim.GetInteger("beingCatState"));
    }

	// OnStateMachineExit is called when exiting a statemachine via its Exit Node
	override public void OnStateMachineExit(Animator anim, int stateMachinePathHash) {
        anim.SetInteger("beingCatState", -1); // Means exited
        anim.SetBool("isIdle", true);
        Debug.Log("StateMachine Exit");
    }
}
