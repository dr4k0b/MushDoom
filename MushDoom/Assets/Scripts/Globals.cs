using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    [Header("Player Statistics")]

    [SerializeField] public float Gravity;

    [SerializeField] public float acceleration;
    [SerializeField] public float deacceleration;
    [SerializeField] public float maxRunSpeed;
    [SerializeField] public float jumpSpeed;
    [SerializeField] public float jumpBufferTime;
    [SerializeField] public float coyoteTime;

    [SerializeField] public bool isDashing;
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashTime;


    [SerializeField] public LayerMask groundLayerMask;


    [Header("Information")]

   // [HideInInspector]

    public float XVelocity;
    public float YVelocity;
    public bool onGround;
}
