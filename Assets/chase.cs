using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class chase : MonoBehaviour {
    public Transform player;
    static Animator anim;
    public AudioClip sound;
    public AudioSource soundSource;
    private bool interestChange;
    private int interestLvl;
    private float goToDist;
    private int rndInterest;
    private bool soundPlayed;

    // Use this for initialization
    void Start() {
        soundSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>(); // Get animator component that is attached to cat
        interestChange = false;
        goToDist = 0;
        soundPlayed = false;
        
        Debug.Log("NPC initialized");

        rndInterest = Random.Range(0, 10);
        //rndInterest = 9;
        // Determine interestLvl: 1(0-5), 2(6-8), or 3(9)
        if (rndInterest < 6) { // Cat will be interested and run
            interestLvl = 0;
            anim.SetBool("isIdle", true);
            anim.SetBool("beingCat", false);
            goToDist = Random.Range(0.5f, 1.5f);
            Debug.Log("Is interested");
        } else if (rndInterest < 9)
        { // Cat will be interested and walk
            interestLvl = 1;
            anim.SetBool("isIdle", true);
            anim.SetBool("beingCat", false);
            goToDist = Random.Range(0.5f, 2.0f);
            Debug.Log("Is interested");
        } else { // Cat will be uninterested
            interestLvl = 2;
            anim.SetBool("isIdle", false);
            anim.SetBool("beingCat", true);
            Debug.Log("is uninterested");
        }

        Debug.Log("interestLvl = " + interestLvl);
        Debug.Log("gotoDist = " + goToDist);

        Debug.Log("Fixed time = " + Time.fixedTime);
        anim.PlayInFixedTime("IdleSit", -1, 10.0f);
    }

    // Update is called once per frame
    void Update() {
        // Toggle interest
        if ((Time.fixedTime % 9) == 0) // Every 11 sec (to go through < 1 idle)
        {
            if (anim.GetBool("isRunning") == false && anim.GetBool("isWalking") == false && anim.GetBool("isIdle") == true && anim.GetInteger("beingCatState") == -1)
            {
                rndInterest = Random.Range(0, 11);
                Debug.Log("rndInterest = " + rndInterest);
                if (rndInterest == 0 || rndInterest == 1)
                {
                    if (interestLvl == 2 && interestChange == false) // 20% chance of becoming interested for uninterested cat
                    {
                        rndInterest = Random.Range(0, 2);
                        interestLvl = rndInterest; // 50% chance of running or walking
                        anim.SetBool("isRunning", false);
                        anim.SetBool("isWalking", false);
                        anim.SetBool("isIdle", true);
                        anim.SetBool("beingCat", false);
                        interestChange = true;
                        Debug.Log("Became interested");
                    }
                    if (interestLvl < 2 && rndInterest == 0 && interestChange == false) // Currently interested
                    {
                        // 10% chance of becoming uninterested for interested cat
                        anim.SetBool("isRunning", false);
                        anim.SetBool("isWalking", false);
                        anim.SetBool("isIdle", false);
                        anim.SetBool("beingCat", true);
                        anim.SetInteger("beingCatState", 0);
                        Debug.Log("Became uninterested");
                        interestLvl = 2;
                        interestChange = true;
                    }
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", false);
                }
            }
            interestChange = false;
        }
        if (interestLvl == 2)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("beingCat", true);
        }
        // \Toggle interest


        if (anim.GetBool("beingCat") == true) {
            // Should only go into if not moving (ie. idle)
            if(anim.GetInteger("beingCatState") == -1)
            {
                anim.SetBool("isIdle", true); // Getting reset after state machine exit for some reason
            }  
        }
        else {
            // Vector3.Distance(a,b) is the same as (a-b).magnitude
            if (Vector3.Distance(player.position, this.transform.position) < 3) { // NPC is in player influence circle
                Vector3 direction = player.position - this.transform.position; // Line between cat and player
                direction.y = 0;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f); // Look at player

                if (direction.magnitude > goToDist) { 
                    anim.SetBool("isIdle", false);
                    if (anim.GetBool("beingCat") == false)
                    {
                        if (interestLvl == 0)
                        {
                            this.transform.Translate(0, 0, 0.01f); // Speed
                            anim.SetBool("isRunning", true);
                        }
                        else
                        {
                            if (interestLvl == 1)
                            {
                                this.transform.Translate(0, 0, 0.005f);
                                anim.SetBool("isWalking", true);
                            }
                        }
                    }
                }
                else {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isIdle", true);
                    // randomly play purr after some time
                    if (soundPlayed == false)
                    {
                        soundSource.PlayOneShot(sound);
                        soundPlayed = true;
                    }
                    
                }
            }
            else
            { // NPC out of player influence circle, will not look or run to player
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
    }
}
