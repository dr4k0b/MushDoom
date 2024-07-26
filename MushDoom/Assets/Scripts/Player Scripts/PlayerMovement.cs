using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Globals G;

    public Rigidbody2D rb;

    private InputSystem input;
    private InputAction move;
    private InputAction jump;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new InputSystem();
    }

    private void Update()
    {
        XMovement();
        rb.velocity = new Vector2(G.XVelocity, G.YVelocity);
    }
    void YMovement()
    {
        
    }
    void XMovement()
    {
        float xDirection = move.ReadValue<Vector2>().x;

        if (Mathf.Abs(G.XVelocity) < G.maxRunSpeed)
        {
            G.XVelocity += xDirection * G.acceleration;
        }

        if (Mathf.Abs(G.XVelocity) < G.deacceleration)
        {
            G.XVelocity = 0;
        }

        if (xDirection == 0)
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

    private void OnEnable()
    {
        move = input.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }
}
