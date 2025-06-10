using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int damage;

    public float rotateSpeed = 180f;
    private Transform center;

    public void Init(Transform target)
    {
        center = target;
    }

    private void Update()
    {
        if (center == null) return;

        transform.RotateAround(center.position, Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    public void SetDamage(int value)
    {
        damage = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            int atk = PlayerStats.Instance.GetCurrentAttack();
            MonsterController monster = collision.GetComponentInParent<MonsterController>();

            if (monster != null)
            {
                monster.TakeDamage(atk);
                Debug.Log($"������ {atk} �������� ����");
            }
            else
            {
                Debug.LogWarning($"[Weapon] Enemy �±��ε� MonsterController ���� , �浹 ���: {collision.name}");
            }
        }
    }
}
