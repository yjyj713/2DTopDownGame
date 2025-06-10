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

    public MonsterController SpawnMonster(string monsterId, Vector3 spawnPos, Transform player)
    {
        MonsterController controller;

        if (!poolDict.ContainsKey(monsterId))
            poolDict[monsterId] = new Queue<MonsterController>();

        if (poolDict[monsterId].Count > 0)
        {
            controller = poolDict[monsterId].Dequeue();
            controller.gameObject.SetActive(true);
            controller.transform.position = spawnPos;
            controller.Init(MonsterDataLoader.MonsterDict[monsterId], player, false);
        }
        else
        {
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/Monsters/{monsterId}");
            if (prefab == null)
            {
                Debug.LogError($"[MonsterPool] Resources.Load 실패: {monsterId} 프리팹 없음");
                return null;
            }

            GameObject go = Instantiate(prefab, spawnPos, Quaternion.identity);
            controller = go.GetComponent<MonsterController>();
            controller.Init(MonsterDataLoader.MonsterDict[monsterId], player, true);
        }

        return controller;
    }
    public void ReturnToPool(MonsterController monster)
    {
        string monsterId = monster.Data.MonsterID;
        monster.gameObject.SetActive(false);
        poolDict[monsterId].Enqueue(monster);
    }

}