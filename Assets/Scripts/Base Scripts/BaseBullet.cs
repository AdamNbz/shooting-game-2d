using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    protected float speed = 25f;
    protected Vector3 direction;
    protected Rigidbody2D rb;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        rb.velocity = direction.normalized * speed;
    }
    
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
        if (rb)
            rb.velocity = direction * speed;
    }


    protected virtual void OnTriggerEnter2D(Collider2D other) { }
}
