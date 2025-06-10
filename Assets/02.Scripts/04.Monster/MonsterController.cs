using System.Collections;
using UnityEngine;

public class MonsterController : BaseController
{
    private Transform player;
    private MonsterData data;
    private int currentHp;
    private Animator animator;
    public MonsterData Data => data;
    private bool isInitialized = false;
    private Coroutine hitEffectCoroutine;

    private void Start()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
    }
    public void Init(MonsterData monsterData, Transform playerTransform, bool resetHp = true)
    {
        data = monsterData;
        player = playerTransform;
        moveSpeed = data.MoveSpeed;

        if (resetHp || !isInitialized)
        {
            currentHp = data.MaxHP;
            isInitialized = true;
        }
        Debug.Log($"[{data.MonsterID}] Init ȣ��� - currentHP: {currentHp}, resetHp: {resetHp}");
    }

    private void Update()
    {
        if (isDead || player == null) return;

        Vector2 dir = player.position - transform.position;
        Move(dir.normalized);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHp -= damage;
        Debug.Log($"{data.MonsterID} �ǰݵ� ���� HP: {currentHp}");

        animator.Play("Hit", -1, 0f);

        if (hitEffectCoroutine != null)
            StopCoroutine(hitEffectCoroutine);

        hitEffectCoroutine = StartCoroutine(FlashColor());

        if (currentHp <= 0)
        {
            MonsDie();
            gameObject.SetActive(false); // Ǯ���̶��
        }
    }
    private IEnumerator FlashColor()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void MonsDie()
    {
        isDead = true;

        if (animator != null)
        {
            animator.Play("Dead", -1, 0f); // ���� �ִϸ��̼� ���� ���
            Debug.Log("[MonsterController] Dead �ִϸ��̼� ���� ���");
        }

        StartCoroutine(DisableAfterDelay(2f));
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}