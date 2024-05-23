using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int x;
    public float multi;
    Rigidbody2D rig;
    public bool flag;
    public GameObject target;
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        
    }
    void Update()
    {
        
    }
    float t = 0;
    private void FixedUpdate()
    {
    }
}
