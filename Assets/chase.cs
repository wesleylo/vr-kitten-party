using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class chase : MonoBehaviour {
    public Transform player;
    static Animator anim;
    private int interestLvl;
    private float goToDist;
    private int rndInterest;
    

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>(); // Get animator component that is attached to cat
        goToDist = 0;
        
        Debug.Log("NPC initialized");

        //anim.SetBool("isIdle", false);
        //anim.SetBool("beingCat", true); //for debugging becat

        rndInterest = Random.Range(0, 10);
        // Determine interestLvl: 1(0-5), 2(6-8), or 3(9)
        Debug.Log("rndInterest = " + rndInterest);
        if (rndInterest < 6) { // Cat will be interested and run
            interestLvl = 0;
            anim.SetBool("isIdle", true);
            anim.SetBool("beingCat", false);
            goToDist = Random.Range(0.5f, 1.5f);
        } else if (rndInterest < 9)
        { // Cat will be interested and walk
            interestLvl = 1;
            anim.SetBool("isIdle", true);
            anim.SetBool("beingCat", false);
            goToDist = Random.Range(0.5f, 2.0f);
        } else { // Cat will be uninterested
            interestLvl = 2;
            anim.SetBool("isIdle", false);
            anim.SetBool("beingCat", true);
        }

        Debug.Log("interestLvl = " + interestLvl);
        Debug.Log("gotoDist = " + goToDist);

        Debug.Log("Fixed time = " + Time.fixedTime);
        anim.PlayInFixedTime("IdleSit", -1, 10.0f);
    }

    // Update is called once per frame
    void Update() {
        
        if ((Time.fixedTime % 10) == 0) // Every 10 sec
        {
            rndInterest = Random.Range(0, 11);
            if (rndInterest == 0)
            {
                if (interestLvl < 2) // Currently interested
                {
                    // 10% chance of becoming uninterested for interested cat
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isIdle", false);
                    anim.SetBool("beingCat", true);
                    Debug.Log("Became uninterested");
                }
                else // 10% chance of becoming interested for uninterested cat
                {
                    rndInterest = Random.Range(0, 2);
                    interestLvl = rndInterest; // 50% chance of running or walking
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isIdle", true);
                    anim.SetBool("beingCat", false);
                    Debug.Log("Became interested");
                }
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", false);
            }
        }
        if (interestLvl == 2)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("beingCat", true);
        }
        // randomly toggle beingCat here
        if (anim.GetBool("beingCat") == true) {
            // Should only go into if not moving (ie. idle)
        }
        else {
            // Vector3.Distance(a,b) is the same as (a-b).magnitude
            if (Vector3.Distance(player.position, this.transform.position) < 3) { // npc in player influence circle
                Vector3 direction = player.position - this.transform.position; // line between cat and player
                direction.y = 0;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f); // look at player (uninterested doesn't affect) maybe make into cone

                if (direction.magnitude > goToDist) { // 0.5 will be final runto distance (will be randomized, high chance of going all the way)
                                                // magnitude > 0 will make cat circle player. maybe walk circle once and then go into sit and meow.. nah takes too long
                                                // either go to 1st degree circle (random distance within), 2nd degree circle, or will become uninterested and stop randomly along the way

                    anim.SetBool("isIdle", false);
                    if (interestLvl == 0) {
                        this.transform.Translate(0, 0, 0.01f); // speed
                        anim.SetBool("isRunning", true);
                    } else {
                        if (interestLvl == 1) {
                            this.transform.Translate(0, 0, 0.005f);
                            anim.SetBool("isWalking", true);
                        }
                    }
                }
                else {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isIdle", true);
                }
            }
            else { // npc out of player influence circle
              // npc will not look or run to player
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
    }
}
