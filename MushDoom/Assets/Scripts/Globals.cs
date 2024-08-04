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

    [HideInInspector] public bool isDashing;
    [HideInInspector] public bool canDash;

    [Header("Player Throw")]

    [SerializeField] public bool canThrow;
    [Range(0f, 30f)]
    [SerializeField] public float hatSpeed;
    [Range(0f, 1f)]
    [SerializeField] public float hatOutTime;

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

}
