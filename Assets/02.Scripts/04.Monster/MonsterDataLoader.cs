using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class MonsterDataLoader : MonoBehaviour
{
    MonsterData data;
    public static Dictionary<string, MonsterData> MonsterDict { get; private set; }
    public static List<MonsterData> MonsterList { get; private set; }

    private void Awake()
    {
        TextAsset json = Resources.Load<TextAsset>("Data/Monster");
        if (json == null)
        {
            Debug.LogError("Monster.json을 찾을 수 없습니다!");
            return;
        }

        var wrapper = JsonUtility.FromJson<MonsterList>(json.text);

        MonsterDict = new Dictionary<string, MonsterData>();
        MonsterList = wrapper.Monster;

        foreach (var m in MonsterList)
        {
            m.Icon = Resources.Load<Sprite>($"Sprites/Monsters/{m.IconName}");
            if (m.Icon == null)
                Debug.LogWarning($"아이콘 스프라이트 못 찾음: {m.IconName}");

            MonsterDict[m.MonsterID] = m;
        }

        Debug.Log($"몬스터 데이터 {MonsterDict.Count}개 로딩 완료");
    }
}

[System.Serializable]
public class MonsterList
{
    public List<MonsterData> Monster;
}