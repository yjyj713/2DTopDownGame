using UnityEngine;

public class AutoAttacker : MonoBehaviour
{
    [SerializeField] private float attackInterval = 1f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private int damage = 5;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float distanceFromPlayer = 0.2f;

    private Transform center;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= attackInterval)
        {
            timer = 0f;
            Attack();
        }
    }

    public void Init(Transform target)
    {
        center = target;

        Vector3 offset = Vector3.right * distanceFromPlayer;
        transform.position = center.position + offset;
    }

    private void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (var hit in hits)
        {
            var monster = hit.GetComponent<MonsterController>();
            if (monster != null)
            {
                monster.TakeDamage(damage);
                Debug.Log($"[AutoAttack] {monster.name}에게 {damage} 데미지!");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}