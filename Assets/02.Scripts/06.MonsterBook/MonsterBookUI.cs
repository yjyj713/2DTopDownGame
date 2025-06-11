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
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var data in MonsterDataLoader.MonsterList)
        {
            var slot = Instantiate(slotPrefabs, slotParent);
            slot.GetComponent<MonsterSlot>().Init(data, ShowInfo);
        }
    }
    private void ShowInfo(MonsterData data)
    {
        monsterImage.sprite = data.sprite;
        nameText.text = data.Name;
        descText.text = data.Description;
    }
}
