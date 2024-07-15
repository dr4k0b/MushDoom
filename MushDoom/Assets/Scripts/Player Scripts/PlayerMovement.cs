using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Globals G;
    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMove(InputAction.CallbackContext context)
    {
        rb.velocity = context.ReadValue<Vector2>();
        Debug.Log(context.ReadValue<Vector2>());
    }
}
