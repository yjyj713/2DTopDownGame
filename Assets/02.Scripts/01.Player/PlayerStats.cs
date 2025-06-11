using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int EquippedWeaponId { get; private set; }

    public int MaxHP = 100;
    public int CurrentHP { get; private set; }

    public int MaxMP = 100;
    public int CurrentMP { get; private set; }

    private void Awake()
    {
        Instance = this;
        EquippedWeaponId = 10000;
        CurrentHP = MaxHP;
        CurrentMP = MaxMP;
    }
    public void HealHP(int amount)
    {
        CurrentHP = Mathf.Min(CurrentHP + amount, MaxHP);
        Debug.Log($"�÷��̾� HP ȸ��: {amount} -> ���� HP: {CurrentHP}");
    }

    public void HealMP(int amount)
    {
        CurrentMP = Mathf.Min(CurrentMP + amount, MaxMP);
        Debug.Log($"�÷��̾� MP ȸ��: {amount} -> ���� MP: {CurrentMP}");
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