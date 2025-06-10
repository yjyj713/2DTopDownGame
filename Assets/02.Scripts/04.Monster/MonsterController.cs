using UnityEngine;

public class MonsterController : BaseController
{
    private Transform player;
    private MonsterData data;
    private int currentHp;

    //private void Awake()
    //{
    //    base.Awake();
    //}

    public void Init(MonsterData monsterData, Transform playerTransform)
    {
        data = monsterData;
        player = playerTransform;

        moveSpeed = data.MoveSpeed;
        currentHp = data.MaxHP;
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
        if (currentHp <= 0)
        {
            Die();
            Destroy(gameObject); // 풀링 쓰고 싶으면 SetActive(false)
        }
    }
}