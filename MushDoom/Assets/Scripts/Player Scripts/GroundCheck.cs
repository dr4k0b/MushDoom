using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    Globals G;
    BoxCollider2D groundCheck;
    void Awake()
    {
        G = FindFirstObjectByType<Globals>();
        groundCheck = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        G.onGround = Physics2D.OverlapArea(groundCheck.bounds.min, groundCheck.bounds.max, G.groundLayerMask);
    }
}
