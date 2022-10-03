using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Collision coll;
    private Rigidbody2D rb2D;

    [Space]
    [Header("Stats")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float wallJumpForce;
    [SerializeField] float slideSpeed;
    [SerializeField] float wallJumpLerp;
    public float fallMultiplier = 3f;
    public float lowJumpMultiplier = 8f;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;

    [Space]

    private bool groundTouch;
    public int side = 1;

    private void Start()
    {
        coll = GetComponent<Collision>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        if (Input.GetButtonDown("Jump"))
        {
            //anim.SetTrigger("jump");

            if (coll.onGround)
                Jump(Vector2.up, false);
            if (coll.onWall && !coll.onGround)
                WallJump();
        }

        if (rb2D.velocity.y < 0)
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        #region grabWall
        if (coll.onWall && Input.GetButton("Fire3") && canMove)
        {
            //if (side != coll.wallSide)
            //    anim.Flip(side * -1);
            wallGrab = true;
            wallSlide = false;
        }
        if (wallGrab)
        {
            rb2D.gravityScale = 0;
            if (x > .2f || x < -.2f)
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        }
        else
        {
            rb2D.gravityScale = 1;
        }

        if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }
        #endregion
        #region WallSlide
        var timerCoroutine = timerWallSlide(1f);
        if (coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                wallSlide = true;
                //StartCoroutine(timerCoroutine);
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
        {
            wallSlide = false;
            //wallJumped = false;
            //StopCoroutine(timerCoroutine);
        }
        #endregion
        if (wallGrab || wallSlide || !canMove)
            return;


        if (coll.onGround && !groundTouch)
        {
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }
    }

    IEnumerator timerWallSlide(float time)
    {
        yield return new WaitForSeconds(time);
        WallSlide();
    }
    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            //anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.2f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / wallJumpForce + wallDir / wallJumpForce), true);

        wallJumped = true;
    }
    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
    void Walk(Vector2 dir)
    {
        if (!canMove)
            return;
        if (wallGrab)
            return;
        if (!wallJumped)
            rb2D.velocity = new Vector2(dir.x * speed, rb2D.velocity.y);
        else
            rb2D.velocity = Vector2.Lerp(rb2D.velocity, new Vector2(dir.x * speed, rb2D.velocity.y), wallJumpLerp * Time.deltaTime);
    }

    private void WallSlide()
    {
        //if (coll.wallSide != side)
        //    anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if ((rb2D.velocity.x > 0 && coll.onRightWall) || (rb2D.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb2D.velocity.x;

        rb2D.velocity = new Vector2(push, -slideSpeed);
    }

    private void Jump(Vector2 dir, bool wall)
    {
        //slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        //ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        rb2D.velocity += dir * jumpForce;

        //particle.Play();
    }
}
