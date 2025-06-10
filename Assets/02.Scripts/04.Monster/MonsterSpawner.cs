using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, 3f);
    }

    private void Spawn()
    {
        Vector3 pos = player.position + (Vector3)(Random.insideUnitCircle * 3f);

        string monsterId = GetRandomMonsterID();
        MonsterPool.Instance.SpawnMonster(monsterId, pos, player);
    }

    public static string GetRandomMonsterID()
    {
        var keys = MonsterDataLoader.MonsterDict.Keys.ToList();
        return keys[Random.Range(0, keys.Count)];
    }
}