using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public static MonsterPool Instance;

    private Dictionary<string, GameObject> prefabDict = new();

    private void Awake()
    {
        Instance = this;

        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/Monsters");
        foreach (var prefab in prefabs)
        {
            string id = prefab.name;
            prefabDict[id] = prefab;
        }
    }

    public MonsterController SpawnMonster(string monsterId, Vector3 spawnPos, Transform player)
    {
        string path = $"Prefabs/Monsters/{monsterId}";
        GameObject prefab = Resources.Load<GameObject>(path);

        if (prefab == null)
        {
            Debug.LogError($"[MonsterPool] Resources.Load 실패: {path} 경로에 프리팹이 없음");
            return null;
        }

        GameObject go = Instantiate(prefab, spawnPos, Quaternion.identity);
        MonsterController controller = go.GetComponent<MonsterController>();
        controller.Init(MonsterDataLoader.MonsterDict[monsterId], player);
        return controller;
    }
}