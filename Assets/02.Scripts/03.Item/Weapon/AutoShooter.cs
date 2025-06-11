using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject heldGun;
    [SerializeField] private Transform firePoint;
    [SerializeField] private SpriteRenderer heldGunSprite;
    [SerializeField] private float fireInterval = 1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;

    private bool hasGun = false;
    private float timer;

    public void EnableGun()
    {
        hasGun = true;
        if (heldGun != null)
            heldGun.SetActive(true);

        if (heldGunSprite != null)
            heldGunSprite.enabled = true;


    }

    private void Update()
    {
        if (!hasGun) return;

        timer += Time.deltaTime;
        if (timer >= fireInterval)
        {
            timer = 0f;
            Fire();
        }

        float direction = playerSpriteRenderer.flipX ? -1f : 1f;

        heldGunSprite.flipX = direction < 0;

        Vector3 localPos = heldGun.transform.localPosition;
        localPos.x = Mathf.Abs(localPos.x) * direction;
        heldGun.transform.localPosition = localPos;

        Vector3 fireLocalPos = firePoint.localPosition;
        fireLocalPos.x = Mathf.Abs(fireLocalPos.x) * direction;
        firePoint.localPosition = fireLocalPos;
    }

    private void Fire()
    {
        // 가장 가까운 적 찾기
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5f, enemyLayer);
        if (hits.Length == 0) return;

        Transform target = hits[0].transform; // 단순히 첫 번째 적
        Vector2 dir = (target.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(dir);
    }
}