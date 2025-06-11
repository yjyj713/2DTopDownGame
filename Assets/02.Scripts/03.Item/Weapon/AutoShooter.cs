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

    private float lastFireTime;
    private bool hasGun = false;
    private float timer;
    private float baseInterval;

    private void Start()
    {
        baseInterval = fireInterval;

        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.OnKillCountChanged += AdjustFireInterval;
            Debug.Log("[AutoShooter] 킬 이벤트 구독 완료");
        }
        else
        {
            Debug.LogWarning("AutoShooter Start: PlayerStats.Instance가 null");
        }
    }

    private void OnDestroy()
    {
        if (PlayerStats.Instance != null)
            PlayerStats.Instance.OnKillCountChanged -= AdjustFireInterval;
    }

    private void AdjustFireInterval(int killCount)
    {
        Debug.Log($"[AutoShooter] AdjustFireInterval 호출됨, 킬수: {killCount}");
        int steps = killCount / 1;
        fireInterval = baseInterval * Mathf.Pow(0.9f, steps);
        Debug.Log($"[AutoShooter] 발사간격: {fireInterval:F2}s");
    }

    public void EnableGun()
    {
        hasGun = true;
        if (heldGun != null)
            heldGun.SetActive(true);
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
        float now = Time.time;
        Debug.Log($"Fire() 호출! 간격: {now - lastFireTime:F2}s");
        lastFireTime = now;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5f, enemyLayer);
        if (hits.Length == 0) return;

        Transform target = hits[0].transform;
        Vector2 dir = (target.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(dir);
    }
}