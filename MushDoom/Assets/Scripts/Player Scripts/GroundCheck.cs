using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    Globals g;
    BoxCollider2D groundCheck;
    void Awake()
    {
        g = FindFirstObjectByType<Globals>();
        groundCheck = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        g.onGround = Physics2D.OverlapArea(groundCheck.bounds.min, groundCheck.bounds.max, g.groundLayerMask);
    }
}
