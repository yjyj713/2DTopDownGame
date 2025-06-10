using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int EquippedWeaponId { get; private set; }

    private void Awake()
    {
        Instance = this;
        EquippedWeaponId = 10000; 
    }

    public void EquipWeapon(int itemId)
    {
        EquippedWeaponId = itemId;
    }

    public int GetCurrentAttack()
    {
        if (ItemDataLoader.ItemDict.TryGetValue(EquippedWeaponId, out var itemData))
        {
            return Mathf.RoundToInt(itemData.MaxAtk * itemData.MaxAtkMul);
        }

        return 0;
    }
}