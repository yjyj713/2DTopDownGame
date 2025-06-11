using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;
    private Vector2 direction;

    public void Init(Vector2 dir)
    {
        direction = dir;
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var monster = collision.GetComponent<MonsterController>();
            monster?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}