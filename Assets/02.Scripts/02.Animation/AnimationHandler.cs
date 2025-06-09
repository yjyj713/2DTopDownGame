using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    protected Animator animator;
    private SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > 0.5f);

        //
        if (obj.x != 0)
        {
            spriteRenderer.flipX = obj.x < 0;
        }
    }

    public void Dead()
    {
        animator.SetBool(IsDead, true);
    }
}
