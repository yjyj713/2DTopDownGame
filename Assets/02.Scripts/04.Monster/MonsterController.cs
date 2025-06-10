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

        // ������ ����
        moveSpeed = data.MoveSpeed;
        currentHp = data.MaxHP;

        // ��������Ʈ ����
        string spriteName = GetSpriteName(data.MonsterID); // ID �� ��������Ʈ�� ��ȯ
        Sprite sprite = Resources.Load<Sprite>($"Sprites/Monsters/{spriteName}");
        if (sprite != null)
            spriteRenderer.sprite = sprite;
        else
            Debug.LogWarning($"��������Ʈ �ε� ����: {spriteName}");
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
            gameObject.SetActive(false); // Ǯ���̸� SetActive(false), �ƴϸ� Destroy(gameObject);
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