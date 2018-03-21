using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float life = 100;

    public float speed = 200;

    public float JumpForce = 1000;

    public bool isGrounded, isShooting, isRight = true;

    float hAxis, vAxis, jAxis;

    string hAxisStr, vAxisStr, fAxisStr, jAxisStr;

    public float bulletSpeed;

    public int PlayerNumber;

    public Transform gunTip;

    public Collider2D Feet;

    public Collider2D Body;

    BoxCollider2D myCollider;

    Rigidbody2D myBody;

    PlayerAnimationController AnimationController;     

    private void Awake()
    {
        hAxisStr = Tags.horizontal + PlayerNumber;
        vAxisStr = Tags.vertical + PlayerNumber;
        fAxisStr = Tags.fire + PlayerNumber;
        jAxisStr = Tags.jump + PlayerNumber;
    }

    // Use this for initialization
    void Start () {
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        AnimationController = GetComponent<PlayerAnimationController>();
    }
	
	// Update is called once per frame
	void Update () {
        if(AnimationController.myState != PlayerAnimationController.PlayerState.Die) {
            Movement();
            Jump();
            CheckShooting();
        }
    }

    public void CheckShooting()
    {
        if (Input.GetButtonDown(fAxisStr) && isShooting == false)
        {
            AnimationController.myState = PlayerAnimationController.PlayerState.Punch;
        }
    }

    public void Shot()
    {
        isShooting = true;
        GameObject gb = ObjectPooling._instance.GetPooledObject("Bullet");
        gb.GetComponent<BulletController>().Init(gunTip, bulletSpeed, isRight);
        isShooting = false;
    }

    void Jump() {
        jAxis = Input.GetAxis(jAxisStr);
        if(jAxis != 0) {
            if (isGrounded) { 
                float jSpeed = jAxis * JumpForce * Time.deltaTime;
                if (jAxis != 0) {
                    AnimationController.myState = PlayerAnimationController.PlayerState.Jump;
                    myBody.velocity = new Vector2(myBody.velocity.x, jSpeed);
                }
            }
        }
    }

    void Movement() {
        hAxis = Input.GetAxis(hAxisStr);
        vAxis = Input.GetAxis(vAxisStr);
        float htemp = hAxis * speed * Time.deltaTime;
        myBody.velocity = new Vector2(htemp, myBody.velocity.y);
        if (isGrounded) { 
            if (hAxis > 0)
            {
                isRight = true;
                AnimationController.myState = PlayerAnimationController.PlayerState.Walk;
                transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else
            {
                if (hAxis < 0) {
                    isRight = false;
                    AnimationController.myState = PlayerAnimationController.PlayerState.Walk;
                    transform.eulerAngles = new Vector3(0, -90, 0);
                }
                else if(Mathf.Abs(hAxis) < 0.2)
                {
                    AnimationController.myState = PlayerAnimationController.PlayerState.Idle;
                }
            }
        }
    }

    public void DoDamage(float damage)
    {
        life = life - damage;
        if (life <= 0)
        {
            SetDeath();
        }
    }

    public void SetDeath() {
        AnimationController.SetDeath();
        Feet.isTrigger = true;
        Body.isTrigger = true;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

}
