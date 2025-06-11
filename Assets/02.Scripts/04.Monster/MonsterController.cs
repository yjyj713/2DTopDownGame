using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : BaseController
{
    [SerializeField] private GameObject healthBarPrefab;

    private float lastAttackTime;

    private Transform player;
    private MonsterData data;
    private int currentHp;
    private Animator animator;
    public MonsterData Data => data;
    private bool isInitialized = false;
    private Coroutine hitEffectCoroutine;
    public bool IsDead => isDead;

    private Slider healthSlider;
    private GameObject healthBarGO;

    private void Start()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
    }
    public void Init(MonsterData monsterData, Transform playerTransform, bool resetHp = true)
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        animator.Rebind();
        animator.Update(0);
        isDead = false;

        data = monsterData;
        player = playerTransform;
        moveSpeed = data.MoveSpeed;

        if (resetHp || !isInitialized)
        {
            currentHp = data.MaxHP;
            isInitialized = true;
        }

        // �ｺ�� ������ �ν��Ͻ�ȭ �� �����̴� ����
        if (healthBarGO == null)
        {
            healthBarGO = Instantiate(healthBarPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity, transform);
            healthSlider = healthBarGO.GetComponentInChildren<Slider>();

            if (healthSlider == null)
            {
                Debug.LogError("HealthBar �ȿ� Slider ������Ʈ�� ã�� �� �����ϴ�!");
                return;
            }
        }


        // �����̴� �� ����
        healthSlider.maxValue = data.MaxHP;
        healthSlider.value = currentHp;

        Debug.Log($"[{data.MonsterID}] Init ȣ��� - currentHP: {currentHp}, resetHp: {resetHp}");

    }

    private void Update()
    {
        if (isDead || player == null) return;

        Vector2 dir = player.position - transform.position;

        float distanceToPlayer = dir.magnitude;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= data.AttackRange)
        {
            if (Time.time - lastAttackTime >= data.AttackCooldown)
            {
                Debug.Log($"{data.MonsterID} ��Ÿ��: {data.AttackCooldown}, ����������: {lastAttackTime}, ����ð�: {Time.time}");
                Debug.Log($"{data.MonsterID} Attack() ȣ�� ���� ����");
                Attack();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            Move(dir.normalized);
        }

        if (healthBarGO != null)
        {
            healthBarGO.transform.position = transform.position + new Vector3(0, 1f, 0);
        }
    }

    private void Attack()
    {
        Debug.Log($"[{data.MonsterID}] Attack() �����");

        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.TakeDamage(data.Attack);
            Debug.Log($"[{data.MonsterID}] �÷��̾�� {data.Attack} ����!");
        }
        else
        {
            Debug.LogWarning("PlayerStats.Instance �� null��!");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHp -= damage;
        Debug.Log($"{data.MonsterID} �ǰݵ� ���� HP: {currentHp}");
        if (healthSlider != null)
            healthSlider.value = currentHp;

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

        // �״� �ִϸ��̼�
        animator.SetTrigger("IsDead");

        DropItem();

        StartCoroutine(ReturnAfterDelay(1.0f));
    }

    private void DropItem()
    {
        if (!MonsterDropLoader.DropTableDict.TryGetValue(data.MonsterID, out var dropList)) return;

        foreach (var drop in dropList)
        {
            float rand = Random.Range(0f, 1f);
            if (rand <= drop.DropRate)
            {
                GameObject itemPrefab = Resources.Load<GameObject>($"Prefabs/Items/{drop.ItemID}");
                if (itemPrefab != null)
                {
                    Instantiate(itemPrefab, transform.position, Quaternion.identity);
                    Debug.Log($"[DropItem] {drop.ItemID} ��ӵ�");
                }
                else
                {
                    Debug.LogWarning($"[DropItem] ������ ����: {drop.ItemID}");
                }
            }
        }
    }

    private IEnumerator ReturnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        MonsterPool.Instance.ReturnToPool(this);
    }
}