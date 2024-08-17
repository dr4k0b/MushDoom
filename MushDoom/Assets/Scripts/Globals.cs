using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    [Header("PLAYER STATISTICS")]

    [Header("Player General")]

    [Range(0f, 1f)]
    [SerializeField] public float actionDelayTime;

    [HideInInspector] public bool onGround;
    [HideInInspector] public BoxCollider2D playerHitbox;
    [HideInInspector] public bool actionDelay;
    [HideInInspector] public float originalGravity;
    [HideInInspector] public int playerDirection;

    [Header("Player Walk")]

    [Range(0f, 5f)]
    [SerializeField] public float acceleration;
    [Range(0f, 5f)]
    [SerializeField] public float deacceleration;
    [Range(0f, 10f)]
    [SerializeField] public float maxRunSpeed;

    [HideInInspector] public float XVelocity;

    [Header("Player Jump")]

    [Range(0f, 40f)]
    [SerializeField] public float jumpSpeed;
    [Range(0f, 1f)]
    [SerializeField] public float dashJumpHeight;
    [Range(0f, 1f)]
    [SerializeField] public float jumpBufferTime;
    [Range(0f, 1f)]
    [SerializeField] public float coyoteTime;

    [Header("Player Dash")]

    [Range(0f, 20f)]
    [SerializeField] public float dashSpeed;
    [Range(0f, 1f)]
    [SerializeField] public float dashTime;
    [Range(0f, 1f)]
    [SerializeField] public float dashBufferTime;

    [HideInInspector] public bool isDashing;
    [HideInInspector] public bool canDash;

    [Header("Player Throw")]

    [Range(0f, 30f)]
    [SerializeField] public float hatSpeed;
    [Range(0f, 1f)]
    [SerializeField] public float hatOutTime;

    [HideInInspector] public bool canThrow;
    [HideInInspector] public int hatOut;
    [HideInInspector] public bool canBounce;

    [Header("WORLD STATISTICS")]

    [Header("Camera")]
    [SerializeField] public int frameRate;
    [Header("Ground")]

    [SerializeField] public LayerMask groundLayerMask;

    private void Update()
    {
        Application.targetFrameRate = frameRate;
    }

    public bool CollisionCheckSquare(BoxCollider2D collider)
    {
        return Physics2D.OverlapArea(collider.bounds.min, collider.bounds.max, groundLayerMask);
    }
    public bool CollisionCheckSquare(BoxCollider2D collider, Vector2 offset)
    {
        collider.offset += offset;
        return Physics2D.OverlapArea(collider.bounds.min, collider.bounds.max, groundLayerMask);
    }
    public bool CollisionCheckSquare(BoxCollider2D collider, LayerMask layer)
    {
        return Physics2D.OverlapArea(collider.bounds.min, collider.bounds.max, layer);
    }
}
