using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public static MonsterPool Instance;

    private Dictionary<string, GameObject> prefabDict = new();
    private readonly Dictionary<string, Queue<MonsterController>> poolDict = new();

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

    public void PreloadMonsters(Dictionary<string, int> preloadCounts)
    {
        foreach (var pair in preloadCounts)
        {
            string id = pair.Key;
            int count = pair.Value;

            if (!prefabDict.ContainsKey(id))
            {
                Debug.LogWarning($"[MonsterPool] {id} 프리팹 없음");
                continue;
            }

            if (!poolDict.ContainsKey(id))
                poolDict[id] = new Queue<MonsterController>();

            GameObject prefab = prefabDict[id];

            for (int i = 0; i < count; i++)
            {
                GameObject go = Instantiate(prefab);
                go.SetActive(false);

                var controller = go.GetComponent<MonsterController>();
                poolDict[id].Enqueue(controller);
            }

            Debug.Log($"[MonsterPool] {id} 몬스터 {count}개 생성 완료");
        }
    }

    public MonsterController SpawnMonster(string monsterId, Vector3 spawnPos, Transform player)
    {
        if (!poolDict.ContainsKey(monsterId) || poolDict[monsterId].Count == 0)
        {
            Debug.LogWarning($"[MonsterPool] '{monsterId}' 풀에 몬스터 없음! 새로 생성하지 않음.");
            return null;
        }

        var controller = poolDict[monsterId].Dequeue();
        controller.gameObject.SetActive(true);
        controller.transform.position = spawnPos;
        controller.Init(MonsterDataLoader.MonsterDict[monsterId], player, false);

        return controller;
    }
    public void ReturnToPool(MonsterController monster)
    {
        string monsterId = monster.Data.MonsterID;
        monster.gameObject.SetActive(false);
        poolDict[monsterId].Enqueue(monster);
    }

}