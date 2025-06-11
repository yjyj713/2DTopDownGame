using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    private MonsterData monsterData;
    private System.Action<MonsterData> onClickCallback;

    public void Init(MonsterData data, System.Action<MonsterData> onClick)
    {
        monsterData = data;
        onClickCallback = onClick;

        if (data.Icon != null)
            icon.sprite = data.Icon;
        else
            Debug.LogWarning($"{data.MonsterID} 아이콘 없음");

        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        onClickCallback?.Invoke(monsterData);
    }
}
