using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider mpSlider;

    private void Update()
    {
        if (PlayerStats.Instance == null) return;

        hpSlider.maxValue = PlayerStats.Instance.MaxHP;
        hpSlider.value = PlayerStats.Instance.CurrentHP;

        mpSlider.maxValue = PlayerStats.Instance.MaxMP;
        mpSlider.value = PlayerStats.Instance.CurrentMP;
    }
}