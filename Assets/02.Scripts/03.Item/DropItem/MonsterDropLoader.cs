using System.Collections.Generic;
using UnityEngine;

public class MonsterDropLoader : MonoBehaviour
{
    public static Dictionary<string, List<DropItemData>> DropTableDict = new();

    private void Awake()
    {
        TextAsset json = Resources.Load<TextAsset>("Data/MonsterDropTable");
        MonsterDropTable table = JsonUtility.FromJson<MonsterDropTable>(json.text);

        foreach (var monsterDrop in table.MonsterDropList)
        {
            DropTableDict[monsterDrop.MonsterID] = monsterDrop.DropItems;
        }

        Debug.Log("[DropLoader] 몬스터 드롭 테이블 로드 완료");
    }
}