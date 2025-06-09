using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;

    protected Rigidbody2D rb;
    protected AnimationHandler animationHandler;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }

    protected void Move(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * (moveSpeed * Time.deltaTime));
        animationHandler?.Move(direction);
    }

    public void Die()
    {
        animationHandler?.Dead();
    }
}
