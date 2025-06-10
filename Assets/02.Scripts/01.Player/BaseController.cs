using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;

    protected Rigidbody2D rb;
    protected AnimationHandler animationHandler;
    protected bool isDead = false;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected void Move(Vector2 direction)
    {
        if (isDead) return;

        rb.MovePosition(rb.position + direction * (moveSpeed * Time.deltaTime));
        animationHandler?.Move(direction);
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        animationHandler?.Dead();
    }
}
