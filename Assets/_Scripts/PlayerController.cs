using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Movements")]
    [SerializeField] float walkspeed;
    [SerializeField] float acceleration;
    [Space]
    [Header("GroundCheck")]
    [SerializeField] float groundOffset;
    [SerializeField] float groundRadius;
    [SerializeField] LayerMask groundLayer;
    private bool isGrounded;
    private float timeSinceGrounded;
    private Collider2D[] colliderGround = new Collider2D[1];

    [Space]
    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] [Tooltip("SUR LE COTÉ")] float wallJumpForce;
    [SerializeField] [Tooltip("VERS LE HAUT")] float wallJumpUpForce;
    [SerializeField] float timeMinBetweenJump;
    [SerializeField] float velocityFallMin;
    //[SerializeField] float velocitySlide;
    [SerializeField] [Tooltip("Gravity otherwise")] float gravity;
    [SerializeField] [Tooltip("Gravity when the player goes up and press jump")] float gravityUpJump;
    [SerializeField] float jumpInputTimer = 0.1f;
    [SerializeField] float coyoteTime;
    private bool inputJump;
    private float timeSinceJumpPressed;
    private float timerNoJump;

    [Space]
    [Header("Slope")]
    [SerializeField] PhysicsMaterial2D physicsFriction;
    //[SerializeField] PhysicsMaterial2D wallFriction;
    [SerializeField] PhysicsMaterial2D physicsNoFriction;
    [SerializeField] float slopeDetectOffset;
    private bool isOnSlope;

    [Space]
    [Header("Corner (in progress)")]
    [SerializeField] Vector2 offsetToReplace;
    [SerializeField] Vector2 offsetCollisionBox;
    private float[] directions = new float[] { -1, 1 };
    [SerializeField] Vector2 collisionBox;

    private Rigidbody2D rb;
    private Vector2 inputs;
    private CapsuleCollider2D cc2D;
    private RaycastHit2D[] hitResults = new RaycastHit2D[1];
    private Collision coll;
    //private Vector2 collisionBox;
    private bool wallGrab;

    //Checkpoint
    public static Vector2 lastCheckpoint;

    //Life
    private int life = 3;
    [SerializeField] Image[] lifeHearts;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc2D = GetComponent<CapsuleCollider2D>();
        coll = GetComponent<Collision>();
        lastCheckpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
        HandleCorners();

    }

    private void FixedUpdate()
    {
        HandleGrounded();
        HandleMovements();
        HandleJump();
        HandleSlope();
        HandleWall();
        HandleGrabWall();
    }
    void HandleInputs()
    {
        inputs.x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("moveX", inputs.x);
        inputs.y = Input.GetAxisRaw("Vertical");

        inputJump = Input.GetKey(KeyCode.Space);
        if (inputJump)
        {
            animator.SetBool("jumped", true);
        }
        if(isGrounded)
            animator.SetBool("jumped", false);
        if (Input.GetKeyDown(KeyCode.Space))
            timeSinceJumpPressed = 0;

        if (coll.onWall && Input.GetButton("Fire3"))
            wallGrab = true;
        if (Input.GetButtonUp("Fire3") || !coll.onWall)
            wallGrab = false;
    }
    void HandleMovements()
    {
        var velocity = rb.velocity;
        Vector2 wantVelocity = new Vector2(inputs.x * walkspeed, velocity.y);
        rb.velocity = Vector2.MoveTowards(velocity, wantVelocity, acceleration * Time.deltaTime);
    }
    void HandleWall()
    {
        if (!isGrounded && coll.onWall && rb.velocity.y <= coll.wallSide && !wallGrab)
        {
            if (coll.onLeftWall)
                rb.velocity = new Vector2(rb.velocity.x, -coll.wallSide);
            else if (coll.onRightWall)
                rb.velocity = new Vector2(rb.velocity.x, coll.wallSide);
        }
    }
    void HandleGrabWall()
    {
        if (wallGrab)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        else
        {
            rb.gravityScale = 1;
        }


    }
    void HandleJump()
    {
        timerNoJump -= Time.deltaTime;
        if (inputJump && (rb.velocity.y <= 0 || isOnSlope) && (isGrounded || timeSinceGrounded < coyoteTime) && timerNoJump <= 0 && timeSinceJumpPressed < jumpInputTimer)
        {
            //print(rb.velocity.y + "avant");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            timerNoJump = timeMinBetweenJump;
            //print(rb.velocity.y + "apres");
        }
        else if (inputJump && coll.onWall && !isGrounded && rb.velocity.y <= 0)
        {
            //print(rb.velocity.y+"avant");
            wallGrab = false;

            if (coll.onLeftWall)
                rb.velocity = new Vector2(rb.velocity.x + wallJumpForce, wallJumpUpForce);
            //WallJump();
            else if (coll.onRightWall)
                rb.velocity = new Vector2(rb.velocity.x - wallJumpForce, wallJumpUpForce);
            //print(rb.velocity.y+"apres");
        }

        if (!isGrounded)
        {
            //print("jump");
            if (rb.velocity.y < 0)
                rb.gravityScale = gravity;
            else
                rb.gravityScale = inputJump ? gravityUpJump : gravity;
        }
        else
        {
            rb.gravityScale = gravity;
        }

        if (rb.velocity.y < velocityFallMin)
            rb.velocity = new Vector2(rb.velocity.x, velocityFallMin);



        timeSinceJumpPressed += Time.deltaTime;
    }
    void HandleGrounded()
    {
        timeSinceGrounded += Time.deltaTime;

        Vector2 point = transform.position + Vector3.up * groundOffset;
        bool currentGrounded = Physics2D.OverlapCircleNonAlloc(point, groundRadius, colliderGround, groundLayer) > 0;
        if (!currentGrounded && isGrounded)
            timeSinceGrounded = 0;
        isGrounded = currentGrounded;
    }

    void HandleSlope()
    {
        Vector3 origin = transform.position + Vector3.up * groundOffset;
        bool slopeRight = Physics2D.RaycastNonAlloc(origin, Vector2.right, hitResults, slopeDetectOffset, groundLayer) > 0;
        bool slopeLeft = Physics2D.RaycastNonAlloc(origin, -Vector2.right, hitResults, slopeDetectOffset, groundLayer) > 0;

        isOnSlope = (slopeRight || slopeLeft) && (!slopeRight || !slopeLeft);

        if (Mathf.Abs(inputs.x) < 0.1f && (slopeLeft || slopeRight))
            cc2D.sharedMaterial = physicsFriction;
        //else if (coll.onWall && !isGrounded && rb.velocity.y <= 0)
        //    cc2D.sharedMaterial = physicsFriction;
        else
            cc2D.sharedMaterial = physicsNoFriction;
    }


    void HandleCorners()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            float dir = directions[i];

            if (Mathf.Abs(inputs.x) > 0.1f && Math.Abs(Mathf.Sign(dir) - Mathf.Sign(inputs.x)) < 0.001f && !isGrounded && !isOnSlope)
            {
                Vector3 position = transform.position + new Vector3(offsetCollisionBox.x + dir * offsetToReplace.x, offsetCollisionBox.y, 0);

                int result = Physics2D.BoxCastNonAlloc(position, collisionBox, 0, Vector2.zero, hitResults, 0, groundLayer);

                if (result > 0)
                {
                    position = transform.position + new Vector3(offsetCollisionBox.x + dir * offsetToReplace.x, offsetCollisionBox.y + offsetToReplace.y, 0);
                    result = Physics2D.BoxCastNonAlloc(position, collisionBox, 0, Vector2.zero, hitResults, 0, groundLayer);

                    if (result == 0)
                    {
                        Debug.Log("replace");
                        transform.position += new Vector3(dir * offsetToReplace.x, offsetToReplace.y);
                        if (rb.velocity.y < 0)
                            rb.velocity = new Vector2(rb.velocity.x, 0);
                    }
                }
            }
        }
    }

    public void Die()
    {
        life--;
        if (life <= 0)
        {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
        else
        {
            gameObject.transform.position = lastCheckpoint;
        }
        
        if (life == 3)
        {
            lifeHearts[0].gameObject.SetActive(true);
            lifeHearts[1].gameObject.SetActive(true);
            lifeHearts[2].gameObject.SetActive(true);
        }
        else if (life == 2)
        {
            lifeHearts[0].gameObject.SetActive(true);
            lifeHearts[1].gameObject.SetActive(true);
            lifeHearts[2].gameObject.SetActive(false);
        }
        else if (life == 1)
        {
            lifeHearts[0].gameObject.SetActive(true);
            lifeHearts[1].gameObject.SetActive(false);
            lifeHearts[2].gameObject.SetActive(false);
        }
        else if (life == 0)
        {
            lifeHearts[0].gameObject.SetActive(false);
            lifeHearts[1].gameObject.SetActive(false);
            lifeHearts[2].gameObject.SetActive(false);
        }
    }
}
