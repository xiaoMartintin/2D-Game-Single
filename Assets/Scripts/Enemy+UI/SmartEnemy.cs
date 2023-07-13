using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemy : Enemy
{
    public float speed;
    public float jumpSpeed;
    public float waitTime;
    public Transform[] moveSpots;
    private float jumpInterval;
    public float jumpIntervalConst;

    private Animator myAnim;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D myFeet;
    private int i = 0;
    private bool movingRight = true;
    private float wait;

    private bool isJumping;
    private bool isFalling;
    private bool isGround;
    private float playerGravity;

 
    
    [Header("追击相关参数")]
    public float goSpeed;
    public float radius;
    [SerializeField]
    private bool isChasing = false;
    private Transform playerTransform;
    private bool canJump = true;

    // Use this for initialization
    public new void Start()
    {
        base.Start();
        wait = waitTime;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        playerGravity = myRigidbody.gravityScale;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();
        CheckChase();
        if (isChasing)
        {
            Debug.Log("Chasing");
            Chase();

        }
           
        else
        {
            Debug.Log("Patrolling");
            Patrol();

        }
            
        //Debug.Log("isChasing : " + isChasing);
        Flip();
        Run(); 
        CheckGrounded();
        SwitchAnimation();
        CheckAirStatus();

        jumpInterval -= Time.deltaTime;

        if (jumpInterval <= 0)
        {
            jumpInterval = jumpIntervalConst;
            if(canJump)
                Jump();
        }

        

        
    }

    void Flip()
    {
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (plyerHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Run()
    {

        float moveDir;
        if (movingRight == true) moveDir = 1.0f;
        else moveDir = -1.0f;

        Vector2 playerVel = new Vector2(moveDir * speed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        //Debug.Log(" plyerHasXAxisSpeed : " + plyerHasXAxisSpeed);
        myAnim.SetBool("Run", plyerHasXAxisSpeed);
    }
    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        
    }

    void Patrol()
    {
        Debug.Log("patrolling");
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[i].position) < 0.1f)
        {
            if (waitTime <= 0.01f)
            {
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }

                if (i == 0)
                {

                    i = 1;
                }
                else
                {

                    i = 0;
                }

                waitTime = wait;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }


            //Debug.Log("movingRight: " + movingRight + " i: "+ i + " RigidbodyVelocity" + myRigidbody.velocity.x);
        }


        isChasing = false;
        
    }

    void CheckChase()
    {
        float distance = (transform.position - playerTransform.position).sqrMagnitude;

        if (distance < radius)
        {
            isChasing = true;
            canJump = false;
        }
        else
        {
            isChasing = false;
            canJump = true;
        }
    }
    void Chase()
    {
        //Debug.Log("playerTransform: " + playerTransform.position.x + " " + playerTransform.position.y);
        //Debug.Log("transform: " + transform.position.x + " " + transform.position.y);

        //transform.position = Vector2.MoveTowards(transform.position,
        //                                        playerTransform.position,
        //                                        goSpeed * Time.deltaTime);
        isChasing = true;
        canJump = false;
        if (Mathf.Abs(playerTransform.position.x - transform.position.x) > 0.1f)
        {
            if (playerTransform.position.x > transform.position.x)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                movingRight = true;
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                movingRight = false;
            }
        }

        if (playerTransform.position.y != transform.position.y)
        {
            isGround = false;
        }
            
        
    }


    void Jump()
    {
        //if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
            }
        }
    }


    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }
    }

    void CheckAirStatus()
    {
        isJumping = myAnim.GetBool("Jump");
        isFalling = myAnim.GetBool("Fall");
    }
}
