using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)] 
public class MonsterDataLoader : MonoBehaviour
{

    public static Dictionary<string, MonsterData> MonsterDict { get; private set; }

    [System.Serializable]
    private class MonsterDataWrapper
    {
        public List<MonsterData> Monster;
    }

    private void Awake()
    {
        TextAsset ta = Resources.Load<TextAsset>("Data/Monster");
        if (ta == null)
        {
            Debug.LogError("Resources/Data/Monster.json�� ��ã�Ҿ��");
            return;
        }

        MonsterDataWrapper wrap = JsonUtility.FromJson<MonsterDataWrapper>(ta.text);
        if (wrap == null || wrap.Monster == null)
        {
            Debug.LogError("Monster.json ������ MonsterDataWrapper�� �����ʽ��ϴ�.");
            return;
        }

        MonsterDict = new Dictionary<string, MonsterData>();
        foreach (var m in wrap.Monster)
        {
            MonsterDict[m.MonsterID] = m;
        }

        Debug.Log($"���� ������ {MonsterDict.Count}�� �ε� �Ϸ�!");
    }
}