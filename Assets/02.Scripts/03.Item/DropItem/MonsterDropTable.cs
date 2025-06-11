using System.Collections.Generic;

[System.Serializable]
public class DropItemData
{
    public int ItemID;
    public float DropRate;
}

[System.Serializable]
public class MonsterDropData
{
    public string MonsterID;
    public List<DropItemData> DropItems;
}

[System.Serializable]
public class MonsterDropTable
{
    public List<MonsterDropData> MonsterDropList;
}
