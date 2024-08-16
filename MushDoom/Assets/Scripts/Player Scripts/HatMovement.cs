using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class HatMovement : MonoBehaviour
{
    Globals g;

    private InputSystem input;
    private InputAction throwHat;
    Rigidbody2D rb;
    BoxCollider2D bc;
    void Awake()
    {
        g = FindFirstObjectByType<Globals>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        input = new InputSystem();
        StartCoroutine(HatMove());
    }

    IEnumerator HatMove()
    {
        float hatOutTime = Time.time + g.hatOutTime;
        rb.velocity = new Vector2(g.hatSpeed * g.playerDirection, 0f);
        yield return new WaitUntil(() => g.CollisionCheckSquare(bc, rb.velocity) || Time.time > hatOutTime);
        rb.velocity = Vector2.zero;
        g.canBounce = true;
        yield return new WaitUntil(() => g.hatOut == 0);
        g.canThrow = true;
        g.canBounce = false;
        Destroy(gameObject);
    }


    private void OnEnable()
    {
        throwHat = input.Player.Throw;
        throwHat.Enable();
    }

    private void OnDisable()
    {
        throwHat.Disable();
    }
}
