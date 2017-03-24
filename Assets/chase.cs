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
    void Start () {
        anim = GetComponent<Animator>(); // get animator component that is attatched to cat
        Debug.Log("NPC initialized");

        goToDist = 0;

        // determine interest 1, 2, or 3
        rndInterest = Random.Range(0, 10);
        //0-5, 6-8, 9
        Debug.Log("rndInterest = " + rndInterest);
        if (rndInterest < 6) // run
        {
            interestLvl = 0;
        }
        else if (rndInterest < 9) // walk
        {
            interestLvl = 1;
        }
        else
        { // uninterested
            interestLvl = 2;
        }
        Debug.Log("interestLvl = " + interestLvl);

        goToDist = Random.Range(0.0f, 1.5f);
        Debug.Log("gotoDist = " + goToDist);
    }

	// Update is called once per frame
	void Update () {
        goToDist = 1;
        // Vector3.Distance(a,b) is the same as (a-b).magnitude
        if (Vector3.Distance(player.position, this.transform.position) < 3) { // npc in player influence circle
            Vector3 direction = player.position - this.transform.position; // line between cat and player
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f); // look at player (uninterested doesn't affect)

            
            
            // calc runto distance



            if (direction.magnitude > .9) { // 1.5 will be final runto distance (will be randomized, high chance of going all the way)
                                            // .magnit.. > 0 will make circle. maybe walk circle once and then go into sit and meow
                                            // either go to 1st degree circle (random distance within), 2nd degree circle, or will become uninterested and stop randomly along the way

                anim.SetBool("isIdle", false);
                if (interestLvl == 0) {
                    this.transform.Translate(0, 0, 0.01f);
                    anim.SetBool("isRunning", true);
                } else {
                    if (interestLvl == 1) {
                        this.transform.Translate(0, 0, 0.005f);
                        anim.SetBool("isWalking", true);
                    }
                }
                
                
            } else
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
            }
		} else { // npc out of player influence circle
            // npc will not look or run to player
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", true);
        }
    }

    private void Reset()
    {
        
    }
}
