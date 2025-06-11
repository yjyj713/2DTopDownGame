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
                shooter.EnableGun(); // �ڵ� ���� ����
                Destroy(gameObject); // �� �����
            }
        }
    }
}
