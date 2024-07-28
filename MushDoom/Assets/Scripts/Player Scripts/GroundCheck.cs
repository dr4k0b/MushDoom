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
        Ray2D ray = new Ray2D(transform.position, Vector2.down);
      //  G.onGround = Physics.OverlapSphere(transform.position, groundCheckRadius, G.groundLayerMask).Count() > 0;
    }
}
