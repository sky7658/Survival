using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int x;
    public float multi;
    Rigidbody2D rig;
    private void Awake()
    {
        //Physics2D.IgnoreCollision(etComponent<Collider2D>(), GetComponentInChildren<Collider2D>());
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = new Vector2(x, 0f);
    }
    void Update()
    {
        //if (rig.velocity == Vector2.zero)
        //{
        //    multi += 1f;
        //    rig.velocity = new Vector2(x, 0f) * multi;
        //}
    }
}
