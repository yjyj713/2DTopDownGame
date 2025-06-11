using UnityEngine;

public class GunPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var shooter = collision.GetComponent<AutoShooter>();
            if (shooter != null)
            {
                shooter.EnableGun(); // 자동 공격 시작
                Destroy(gameObject); // 총 사라짐
            }
        }
    }
}
