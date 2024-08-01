using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    [Header("PLAYER STATISTICS")]

    [Header("Player General")]

    [Range(0f, 1f)]
    [SerializeField] public float actionDelayTime;

    [Header("Player Walk")]

    [Range(0f, 1f)]
    [SerializeField] public float acceleration;
    [Range(0f, 1f)]
    [SerializeField] public float deacceleration;
    [Range(0f, 10f)]
    [SerializeField] public float maxRunSpeed;

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

    [Header("Player Throw")]

    [SerializeField] public bool canThrow;

    [Header("WORLD STATISTICS")]

    [Header("Ground")]

    [SerializeField] public LayerMask groundLayerMask;


    [Header("INFORMATION")]

    // [HideInInspector]

    public float XVelocity;
    public float originalGravity;
    public bool isDashing;
    public bool canDash;
    public bool onGround;
}
