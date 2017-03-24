using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour {
	public Transform player;
    static Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>(); // get animator component that is attatched to cat
	}

	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(player.position, this.transform.position) < 10) {
			Vector3 direction = player.position - this.transform.position; // line between cat and player
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.1f);
            anim.SetBool("isIdle", false);
            if(direction.magnitude > 1.5) {
                this.transform.Translate(0,0,0.01f); // will move random distances towards the ball
                anim.SetBool("isRunning", true);
            }
		}
	}
}
