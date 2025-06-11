using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolInitializer : MonoBehaviour
{
    [SerializeField] private bool runOnStart = true;

    private void Start()
    {
        if (!runOnStart) return;

        var preloadMap = new Dictionary<string, int>
        {
            { "M0001", 10 },
            { "M0002", 10 },
            { "M0003", 10 },
            { "M0004", 10 },
            { "M0005", 10 },
        };

        MonsterPool.Instance.PreloadMonsters(preloadMap);
    }
}