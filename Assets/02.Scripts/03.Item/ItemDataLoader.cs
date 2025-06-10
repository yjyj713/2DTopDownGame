using System.Collections.Generic;
using UnityEngine;

public static class ItemDataLoader
{
    public static Dictionary<int, ItemData> ItemDict { get; private set; }

    static ItemDataLoader()
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>("Data/item_data");
        if (jsonAsset == null)
        {
            Debug.LogError("[ItemDataLoader] item_data.json을 찾을 수 없습니다.");
            ItemDict = new Dictionary<int, ItemData>();
            return;
        }

        string json = jsonAsset.text;
        var wrapper = JsonUtility.FromJson<ItemDataWrapper>(json);
        ItemDict = new Dictionary<int, ItemData>();

        foreach (var item in wrapper.Item)
        {
            ItemDict[item.ItemID] = item;
        }

        Debug.Log($"[ItemDataLoader] 아이템 {ItemDict.Count}개 로딩 완료");
    }
}