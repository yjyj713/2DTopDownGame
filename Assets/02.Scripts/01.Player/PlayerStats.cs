using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private Slider playerHPBar;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider mpSlider;
    [SerializeField] private GameObject gameOverUI;

    public static PlayerStats Instance;

    public int EquippedWeaponId { get; private set; }

    public int MaxHP { get; private set; }
    public int CurrentHP { get; private set; }

    public int MaxMP { get; private set; }
    public int CurrentMP { get; private set; }

    public event Action<int> OnKillCountChanged;
    private int killCount;


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
        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            Debug.Log("�÷��̾� ���!");
            GameOver();
        }

        Debug.Log($"HP: {CurrentHP}/{MaxHP}");

        if (playerHPBar != null)
            playerHPBar.value = CurrentHP;
    }
    private void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
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

    public void AddKill()
    {
        killCount++;
        Debug.Log($"[PlayerStats] AddKill ȣ��, ���� ų��: {killCount}");
        OnKillCountChanged?.Invoke(killCount);
    }
    public int KillCount => killCount;
}