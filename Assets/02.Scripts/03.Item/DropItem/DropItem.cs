using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private int itemId;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        switch (itemId)
        {
            case 20000:
                PlayerStats.Instance.RecoverHP(50);
                break;
            case 20001:
                PlayerStats.Instance.RecoverMP(50);
                break;
            case 20002:
                PlayerStats.Instance.RecoverHP(50);
                PlayerStats.Instance.RecoverMP(50);
                break;
        }

        Destroy(gameObject);
    }
}