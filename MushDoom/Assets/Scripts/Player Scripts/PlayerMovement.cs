using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject hat;

    Globals G;
    Rigidbody2D rb;

    float jumpBufferTimer;
    float coyoteTimer;

    private InputSystem input;
    private InputAction move;
    private InputAction jump;
    private InputAction dash;
    private InputAction throwHat;
    private void Awake()
    {
        G = FindFirstObjectByType<Globals>();
        rb = GetComponent<Rigidbody2D>();
        G.originalGravity = rb.gravityScale;
        input = new InputSystem();
    }

    private void Update()
    {
        if (!G.isDashing)
            XMovement();

        JumpBuffer();
        CoyoteTime();
        CanDash();

        if (!G.isDashing)
            rb.velocity = new Vector3(G.XVelocity, rb.velocity.y);
        else
            rb.velocity = new Vector3(G.XVelocity, 0f);
    }

    void XMovement()
    {
        float xDirection = move.ReadValue<Vector2>().x;

        if (xDirection != 0)
        {
            transform.localScale = new Vector2(xDirection, transform.localScale.y);

            if (G.XVelocity < G.maxRunSpeed && xDirection == 1)
            {
                G.XVelocity += G.acceleration;
            }

            if (G.XVelocity > -G.maxRunSpeed && xDirection == -1)
            {
                G.XVelocity -= G.acceleration;
            }
        }

        if (Mathf.Abs(G.XVelocity) < G.deacceleration && xDirection == 0)
        {
            G.XVelocity = 0;
        }

        if ((xDirection == 0 || Mathf.Abs(G.XVelocity) > G.maxRunSpeed) && G.onGround)
        {
            if (G.XVelocity > 0)
            {
                G.XVelocity -= G.deacceleration;
            }
            if (G.XVelocity < 0)
            {
                G.XVelocity += G.deacceleration;
            }
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (coyoteTimer > Time.time)
            PerformJump();
        else
            jumpBufferTimer = Time.time + G.jumpBufferTime;
    }

    private void PerformJump()
    {
        if (G.isDashing)
        {
            rb.velocity = new Vector2(rb.velocity.x, G.dashJumpHeight * G.jumpSpeed);
        }
        else
            rb.velocity = new Vector2(rb.velocity.x, G.jumpSpeed);
        DashCancel();
        coyoteTimer = 0;
    }

    private void JumpBuffer()
    {
        if (jumpBufferTimer > Time.time && G.onGround)
        {
            PerformJump();
            jumpBufferTimer = 0;
        }
    }

    private void CoyoteTime()
    {
        if (G.onGround)
        {
            coyoteTimer = Time.time + G.coyoteTime;
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (!G.isDashing && G.canDash)
        {
            G.isDashing = true;
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        G.canDash = false;
        rb.gravityScale = 0;
        G.XVelocity = 0;

        yield return new WaitForSeconds(G.actionDelayTime);

        G.XVelocity = transform.localScale.x * G.dashSpeed;

        yield return new WaitForSeconds(G.dashTime);

        rb.gravityScale = G.originalGravity;
        G.isDashing = false;

    }

    private void DashCancel()
    {
        G.isDashing = false;
        rb.gravityScale = G.originalGravity;
    }

    private void CanDash()
    {
        if (G.onGround)
        {
            G.canDash = true;
        }
    }

    private void Throw(InputAction.CallbackContext context)
    {
        if (G.canThrow)
        {
            StartCoroutine(PerformtThrow());
        }
    }
    private IEnumerator PerformtThrow()
    {
        float originalSpeed = G.XVelocity;

        G.canThrow = false;
        rb.gravityScale = 0;
        G.XVelocity = 0;

        yield return new WaitForSeconds(G.actionDelayTime);

        Instantiate(hat, transform.position, transform.localRotation);
        rb.gravityScale = G.originalGravity;
        G.XVelocity = originalSpeed;
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
