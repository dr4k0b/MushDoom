using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float groundCheckRadius;

    Globals G;

    void Awake()
    {
        G = FindFirstObjectByType<Globals>();
    }
    void Update()
    {
        if (!G.onGround)
            G.onGround = Physics2D.OverlapCircle(transform.position, groundCheckRadius, G.groundLayerMask);
    }
}
