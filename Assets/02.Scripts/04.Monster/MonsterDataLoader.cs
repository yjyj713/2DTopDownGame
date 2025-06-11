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
            Debug.LogError("Monster.json�� ã�� �� �����ϴ�!");
            return;
        }

        var wrapper = JsonUtility.FromJson<MonsterList>(json.text);

        MonsterDict = new Dictionary<string, MonsterData>();
        MonsterList = wrapper.Monster;

        foreach (var m in MonsterList)
        {
            m.Icon = Resources.Load<Sprite>($"Sprites/Monsters/{m.IconName}");
            if (m.Icon == null)
                Debug.LogWarning($"������ ��������Ʈ �� ã��: {m.IconName}");

            MonsterDict[m.MonsterID] = m;
        }

        Debug.Log($"���� ������ {MonsterDict.Count}�� �ε� �Ϸ�");
    }
}

[System.Serializable]
public class MonsterList
{
    public List<MonsterData> Monster;
}