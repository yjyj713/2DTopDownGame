using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public static MonsterPool Instance;

    [SerializeField] private GameObject monsterPrefab;

    private Queue<MonsterController> pool = new();

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < 10; i++)
        {
            var go = Instantiate(monsterPrefab, transform);
            go.SetActive(false);
            pool.Enqueue(go.GetComponent<MonsterController>());
        }
    }

    public MonsterController SpawnMonster(string monsterId, Vector3 spawnPos, Transform player)
    {
        if (pool.Count == 0) return null;

        var monster = pool.Dequeue();
        monster.gameObject.SetActive(true);
        monster.transform.position = spawnPos;
        monster.Init(MonsterDataLoader.MonsterDict[monsterId], player);
        return monster;
    }

    public void ReturnToPool(MonsterController monster)
    {
        monster.gameObject.SetActive(false);
        pool.Enqueue(monster);
    }
}