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
        if (flag && Input.GetKeyDown(KeyCode.L))
        {
            rig.AddForce(new Vector2(x, 0f), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (!flag) rig.MovePosition(rig.position + ((Vector2)target.transform.position - rig.position).normalized * Time.deltaTime);
    }
}
