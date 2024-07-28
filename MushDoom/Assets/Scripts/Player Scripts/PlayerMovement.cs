using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Globals G;
    Rigidbody2D rb;

    private InputSystem input;
    private InputAction move;
    private InputAction jump;
    private void Awake()
    {
        G = FindFirstObjectByType<Globals>();
        rb = GetComponent<Rigidbody2D>();
        input = new InputSystem();
    }

    private void Update()
    {
        XMovement();
        YMovement();
        rb.velocity = new Vector2(G.XVelocity, G.YVelocity);
    }
    void YMovement()
    {
        G.YVelocity -= G.Gravity;

        Ray ray = new Ray(transform.GetChild(0).position, Vector2.down);

        if (Physics.Raycast(ray, out RaycastHit hit,G.YVelocity, G.groundLayerMask))
        {
            if (hit.distance > G.YVelocity)
            {

                G.YVelocity = hit.distance;
            }
        Debug.Log(hit.distance);
        }
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

        jump = input.Player.Jump;
        jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }
}
