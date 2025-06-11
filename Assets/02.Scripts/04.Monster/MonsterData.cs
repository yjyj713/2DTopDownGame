using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class MonsterData
{
    public string MonsterID;
    public string Name;
    public string Description;
    public int Attack;
    public float AttackMul;
    public int MaxHP;
    public float AttackRange;
    public float AttackRangeMul;
    public float AttackSpeed;
    public float MoveSpeed;
    public int MinExp;
    public int MaxExp;
    public int DropItem;
    public float AttackCooldown;
    public string IconName;

    [System.NonSerialized]
    public Sprite Icon;
}