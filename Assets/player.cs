using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
        Debug.Log("Player initialized");
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("1"))
        {
            print("11111");
        }
	}
}
