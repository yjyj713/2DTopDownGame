using UnityEngine;
using System.Linq;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, 3f);
    }

    private void Spawn()
    {
        var keys = MonsterDataLoader.MonsterDict.Keys
            .Where(k => Resources.Load<GameObject>($"Prefabs/Monsters/{k}") != null)
            .ToList();

        if (keys.Count == 0) return;

        string randomID = keys[Random.Range(0, keys.Count)];
        Vector3 pos = player.position + (Vector3)(Random.insideUnitCircle * 4f);

        MonsterPool.Instance.SpawnMonster(randomID, pos, player);
    }
}