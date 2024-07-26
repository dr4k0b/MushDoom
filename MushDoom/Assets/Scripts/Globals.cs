using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    [Header("Player Statistics")]

    [SerializeField] public float Gravity = 2;

    [SerializeField] public float acceleration = 2;
    [SerializeField] public float deacceleration = 2;
    [SerializeField] public float maxRunSpeed = 0;

    public float XVelocity = 0;
    public float YVelocity = 0;
}
