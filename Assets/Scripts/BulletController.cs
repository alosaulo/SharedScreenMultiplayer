using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float damage = 10;
    Rigidbody2D myBody;

    public void Init(Transform fPos, float bSpeed, bool isRight)
    {
        transform.position = fPos.position;
        transform.rotation = fPos.rotation;
        gameObject.SetActive(true);
        if(isRight)
            myBody.AddRelativeForce(Vector2.right*bSpeed);
        else
            myBody.AddRelativeForce(Vector2.left * bSpeed);
    }

    void ClearRigidBody() {
        myBody.velocity = Vector2.zero;
        myBody.angularVelocity = 0;
    }

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collision.GetComponentInParent<PlayerController>().DoDamage(damage);
        }
        gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
