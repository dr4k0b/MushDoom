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
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new InputSystem();
    }

    private void Update()
    {
        G.XVelocity = move.ReadValue<Vector2>().x * G.speed;

        rb.velocity = new Vector2(G.XVelocity,G.YVelocity);
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
