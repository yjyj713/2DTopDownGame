using UnityEngine;

public class MonsterController : BaseController
{
    private Transform player;
    private MonsterData data;
    private int currentHp;

    public void Init(MonsterData monsterData, Transform playerTransform)
    {
        data = monsterData;
        player = playerTransform;

        // 데이터 적용
        moveSpeed = data.MoveSpeed;
        currentHp = data.MaxHP;

        // 스프라이트 설정
        string spriteName = GetSpriteName(data.MonsterID); // ID → 스프라이트명 변환
        Sprite sprite = Resources.Load<Sprite>($"Sprites/Monsters/{spriteName}");
        if (sprite != null)
            spriteRenderer.sprite = sprite;
        else
            Debug.LogWarning($"스프라이트 로드 실패: {spriteName}");
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
            gameObject.SetActive(false); // 풀링이면 SetActive(false), 아니면 Destroy(gameObject);
        }
    }

    private string GetSpriteName(string monsterID)
    {
        return monsterID switch
        {
            "M0001" => "skeleton",
            "M0002" => "orc",
            "M0003" => "bat",
            "M0004" => "slime",
            _ => "default_monster"
        };
    }
}