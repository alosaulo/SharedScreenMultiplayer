using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootController : MonoBehaviour {

    public PlayerController playerControl;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(gameObject.tag);
        if (collision.gameObject.layer == 8)
        {
            playerControl.isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(gameObject.tag);
        if (collision.gameObject.layer == 8)
        {
            playerControl.isGrounded = false;
        }
    }

}
