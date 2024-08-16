using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject hat;

    Globals g;
    Rigidbody2D rb;

    float jumpBufferTimer;
    float dashBufferTimer;
    float coyoteTimer;
    float xDirection;

    private InputSystem input;
    private InputAction move;
    private InputAction jump;
    private InputAction dash;
    private InputAction throwHat;
    private void Awake()
    {
        g = FindFirstObjectByType<Globals>();
        rb = GetComponent<Rigidbody2D>();
        g.originalGravity = rb.gravityScale;
        input = new InputSystem();
    }

    private void Update()
    {
        xDirection = (int)move.ReadValue<Vector2>().x;
        if (xDirection != 0)
            g.playerDirection = (int)move.ReadValue<Vector2>().x;
    }
    private void FixedUpdate()
    {
        if (!g.isDashing && !g.actionDelay)
            XMovement(xDirection);

        g.hatOut = (int)throwHat.ReadValue<float>();

        JumpBuffer();
        DashBuffer();
        HatBounce();
        CoyoteTime();
        CanDash();

        if (!g.actionDelay)
            rb.velocity = new Vector3(g.XVelocity, rb.velocity.y);
        else if (g.isDashing)
            rb.velocity = new Vector3(g.XVelocity, 0f);
    }
    void XMovement(float xDirection)
    {

        if (xDirection != 0)
        {
            transform.localScale = new Vector2(g.playerDirection, transform.localScale.y);

            if (g.XVelocity < g.maxRunSpeed && xDirection == 1)
            {
                g.XVelocity += g.acceleration;
            }

            if (g.XVelocity > -g.maxRunSpeed && xDirection == -1)
            {
                g.XVelocity -= g.acceleration;
            }
        }

        if (Mathf.Abs(g.XVelocity) < g.deacceleration && xDirection == 0)
        {
            g.XVelocity = 0;
        }

        if ((xDirection == 0 || Mathf.Abs(g.XVelocity) > g.maxRunSpeed))// && g.onGround)
        {
            if (g.XVelocity > 0)
            {
                g.XVelocity -= g.deacceleration;
            }
            if (g.XVelocity < 0)
            {
                g.XVelocity += g.deacceleration;
            }
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (coyoteTimer > Time.time)
            PerformJump();
        else
            jumpBufferTimer = Time.time + g.jumpBufferTime;
    }

    private void PerformJump()
    {
        if (g.isDashing)
            rb.velocity = new Vector2(rb.velocity.x, g.dashJumpHeight * g.jumpSpeed);
        else
            rb.velocity = new Vector2(rb.velocity.x, g.jumpSpeed);

        DashCancel();
        coyoteTimer = 0;
    }

    private void JumpBuffer()
    {
        if (jumpBufferTimer > Time.time && g.onGround)
        {
            PerformJump();
            jumpBufferTimer = 0;
        }
    }
    private void CoyoteTime()
    {
        if (g.onGround)
        {
            coyoteTimer = Time.time + g.coyoteTime;
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (g.canDash)
        {
            g.isDashing = true;
            StartCoroutine(PerformDash());
        }
        else
            dashBufferTimer = Time.time + g.dashBufferTime;
    }

    private IEnumerator PerformDash()
    {
        g.canDash = false;
        rb.gravityScale = 0;
        g.XVelocity = 0;

        g.actionDelay = true;
        yield return new WaitForSeconds(g.actionDelayTime);
        g.actionDelay = false;

        transform.localScale = new Vector2(g.playerDirection, transform.localScale.y);
        g.XVelocity = transform.localScale.x * g.dashSpeed;

        yield return new WaitForSeconds(g.dashTime);

        rb.gravityScale = g.originalGravity;
        g.isDashing = false;

    }

    private void DashCancel()
    {
        g.isDashing = false;
        rb.gravityScale = g.originalGravity;
    }

    private void CanDash()
    {
        if (!g.isDashing && g.onGround && !g.actionDelay)
        {
            g.canDash = true;
        }
    }

    private void DashBuffer()
    {
        if (dashBufferTimer > Time.time && g.canDash)
        {
            g.isDashing = true;
            StartCoroutine(PerformDash());
            dashBufferTimer = 0;
        }
    }


    private void Throw(InputAction.CallbackContext context)
    {
        if (g.canThrow)
        {
            StartCoroutine(PerformtThrow());
        }
    }

    private IEnumerator PerformtThrow()
    {
        if (g.isDashing && g.actionDelay)
        {
            DashCancel();
            g.canDash = true;
            dashBufferTimer = Time.time + g.dashBufferTime;
        }

        Vector2 originalVelocity = rb.velocity;
        float originalSpeed = g.XVelocity;

        g.canThrow = false;
        rb.gravityScale = 0;
        g.XVelocity = 0;
        rb.velocity = Vector2.zero;

        g.actionDelay = true;
        yield return new WaitForSeconds(g.actionDelayTime);
        g.actionDelay = false;

        Instantiate(hat, transform.position, transform.localRotation);

        if (!g.isDashing)
        {
            rb.gravityScale = g.originalGravity;
            if (originalVelocity.y > 0)
                rb.velocity = originalVelocity;
        }
        g.XVelocity = originalSpeed;
    }

    private void HatBounce()
    {
        if (g.canBounce && g.CollisionCheckSquare(gameObject.GetComponent<BoxCollider2D>(), LayerMask.GetMask("Hat")))
        {
            rb.velocity = new Vector2(rb.velocity.x, g.jumpSpeed);
            DashCancel();
        }
    }
    private void OnEnable()
    {
        move = input.Player.Move;
        move.Enable();

        jump = input.Player.Jump;
        jump.Enable();
        jump.performed += Jump;

        dash = input.Player.Dash;
        dash.Enable();
        dash.performed += Dash;

        throwHat = input.Player.Throw;
        throwHat.Enable();
        throwHat.performed += Throw;
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        dash.Disable();
        throwHat.Disable();
    }
}
