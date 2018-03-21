using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    public Animator myAnimator;

    public enum PlayerState {
        Idle,
        Walk,
        Jump,
        Punch,
        Die
    }

    public PlayerState myState;

	// Use this for initialization
	void Start () {
        myState = PlayerState.Idle;
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
         CheckPlayerAnimStates();
    }

    public void CheckPlayerAnimStates() {
        switch (myState) {
            case PlayerState.Idle:
                myAnimator.SetBool("Walk", false);
                myAnimator.SetBool("Jump", false);
                break;
            case PlayerState.Jump:
                myAnimator.SetBool("Jump",true);
                myAnimator.SetBool("Walk", false);
                break;
            case PlayerState.Punch:
                myAnimator.SetTrigger("Punch");
                break;
            case PlayerState.Walk:
                myAnimator.SetBool("Walk",true);
                myAnimator.SetBool("Jump", false);
                break;
        }
    }

    public void SetDeath() {
        myState = PlayerState.Die;
        myAnimator.SetBool("Walk", false);
        myAnimator.SetBool("Jump", false);
        myAnimator.SetTrigger("Died");
    }

}
