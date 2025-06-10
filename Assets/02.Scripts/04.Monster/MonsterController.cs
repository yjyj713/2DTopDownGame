using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Transform player;
    private MonsterData data;
    private int hp;

    public void Init(MonsterData _data, Transform _player)
    {
        data = _data;
        player = _player;
        hp = data.maxHp;

        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/Monsters/{data.spriteName}");
    }

    private void Update()
    {
        if (player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * data.moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Debug.Log($"Monster {data.name} 죽음!");
            gameObject.SetActive(false);
            // TODO: 드롭 아이템 생성
        }
    }
}