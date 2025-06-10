using System.Collections.Generic;
using UnityEngine;

public class MonsterDataLoader : MonoBehaviour
{
    public static Dictionary<string, MonsterData> MonsterDict;

    private void Awake()
    {
        var json = Resources.Load<TextAsset>("Data/Monster").text;
        MonsterDataList wrapper = JsonUtility.FromJson<MonsterDataList>(json);
        MonsterDict = new Dictionary<string, MonsterData>();
        foreach (var data in wrapper.monsters)
        {
            MonsterDict[data.id] = data;
        }
    }

    [System.Serializable]
    private class MonsterDataList
    {
        public List<MonsterData> monsters;
    }
}