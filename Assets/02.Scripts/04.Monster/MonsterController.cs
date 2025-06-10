using UnityEngine;
using UnityEngine.UIElements;

public class MonsterController : BaseController
{
    private Transform player;
    private MonsterData data;
    private int currentHp;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        base.Awake();

        spriteRenderer = transform.Find("MainSprite").GetComponent<SpriteRenderer>();
    }

    public void Init(MonsterData monsterData, Transform playerTransform)
    {
        data = monsterData;
        player = playerTransform;

        // 데이터 적용
        moveSpeed = data.MoveSpeed;
        currentHp = data.MaxHP;

        // 스프라이트 설정
        string spriteName = GetSpriteName(data.MonsterID);
        var sprite = Resources.Load<Sprite>($"Sprites/Monsters/{spriteName}");
        if (sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
        else
        {
            Debug.LogError($"스프라이트 {spriteName} 못 찾음");
        }
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