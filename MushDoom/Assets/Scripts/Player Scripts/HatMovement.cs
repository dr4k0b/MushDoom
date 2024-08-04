using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HatMovement : MonoBehaviour
{
    Globals g;

    private InputSystem input;
    private InputAction throwHat;
    Rigidbody2D rb;
    void Awake()
    {
        g = FindFirstObjectByType<Globals>();
        input = new InputSystem();
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(HatMove());
    }

    IEnumerator HatMove()
    {
        rb.velocity = new Vector2(g.playerDirection * g.hatSpeed, 0f);
        yield return new WaitForSeconds(g.hatOutTime);
        rb.velocity = Vector3.zero;
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
