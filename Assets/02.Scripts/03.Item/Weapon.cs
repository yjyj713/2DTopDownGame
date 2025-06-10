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
                Debug.Log($"적에게 {atk} 데미지를 입힘");
            }
            else
            {
                Debug.LogWarning($"[Weapon] Enemy 태그인데 MonsterController 없음 , 충돌 대상: {collision.name}");
            }
        }
    }
}
