using UnityEngine;

public class MonsterBookToggle : MonoBehaviour
{
    [SerializeField] private GameObject monsterBookUI;

    public void ToggleBook()
    {
        bool open = !monsterBookUI.activeSelf;
        monsterBookUI.SetActive(open);

        Time.timeScale = open ? 0f : 1f;
    }
}
