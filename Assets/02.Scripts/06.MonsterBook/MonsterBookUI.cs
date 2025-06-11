using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefabs;
    [SerializeField] private Transform slotParent;

    [SerializeField] private Image monsterImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;

    private void OnEnable()
    {
        CreateSlots();
    }

    private void CreateSlots()
    {
        foreach (Transform c in slotParent) Destroy(c.gameObject);

        foreach (var data in MonsterDataLoader.MonsterList)
        {
            var go = Instantiate(slotPrefabs, slotParent);
            go.GetComponent<MonsterSlot>().Init(data, ShowInfo);
        }
    }
    private void ShowInfo(MonsterData data)
    {
        monsterImage.sprite = data.Icon;
        nameText.text = data.Name;
        descText.text = data.Description;
    }
}
