using UnityEngine;

public class MonsterBookToggle : MonoBehaviour
{
    [SerializeField] private GameObject monsterBookUI;

    public void ToggleBook()
    {
        if (monsterBookUI != null)
            monsterBookUI.SetActive(!monsterBookUI.activeSelf);
    }
}
