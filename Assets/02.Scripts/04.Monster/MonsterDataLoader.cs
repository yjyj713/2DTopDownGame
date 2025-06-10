using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class MonsterDataLoader : MonoBehaviour
{
    public static Dictionary<string, MonsterData> MonsterDict { get; private set; }

    private void Awake()
    {
        TextAsset json = Resources.Load<TextAsset>("Data/Monster");
        MonsterList wrapper = JsonUtility.FromJson<MonsterList>(json.text);
        MonsterDict = new Dictionary<string, MonsterData>();

        foreach (var data in wrapper.Monster)
        {
            if (!MonsterDict.ContainsKey(data.MonsterID))
                MonsterDict.Add(data.MonsterID, data);
        }

        Debug.Log($"���� ������ {MonsterDict.Count}�� �ε� �Ϸ�");
    }
}

[System.Serializable]
public class MonsterList
{
    public List<MonsterData> Monster;
}