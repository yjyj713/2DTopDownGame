using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider mpSlider;

    public static PlayerStats Instance;

    public int EquippedWeaponId { get; private set; }

    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }

    public int MaxMP { get; private set; }
    public int CurrentMP { get; private set; }


    private void Start()
    {
        CurrentHP = MaxHP;
        CurrentMP = MaxMP;
    }

    private void Awake()
    {
        Instance = this;
        EquippedWeaponId = 10000;

        MaxHP = playerData.maxHP;
        MaxMP = playerData.maxMP;

        CurrentHP = MaxHP;
        CurrentMP = MaxMP;

        hpSlider.maxValue = MaxHP;
        hpSlider.value = CurrentHP;

        mpSlider.maxValue = MaxMP;
        mpSlider.value = CurrentMP;
    }
    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        CurrentHP = Mathf.Max(CurrentHP, 0);
        hpSlider.value = CurrentHP;

        if (CurrentHP == 0)
        {
            Debug.Log("플레이어 사망!");
        }

        Debug.Log($"HP: {CurrentHP}/{MaxHP}");
    }

    public void RecoverHP(int amount)
    {
        CurrentHP = Mathf.Min(CurrentHP + amount, MaxHP);
        hpSlider.value = CurrentHP;
    }

    public void RecoverMP(int amount)
    {
        CurrentMP = Mathf.Min(CurrentMP + amount, MaxMP);
        mpSlider.value = CurrentMP;
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