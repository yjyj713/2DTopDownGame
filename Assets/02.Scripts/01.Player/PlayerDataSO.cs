using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public int maxHP;
    public int maxMP;
}