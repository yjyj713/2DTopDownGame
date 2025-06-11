using UnityEngine;

public class PlayerStatusUIFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 screenPos = mainCam.WorldToScreenPoint(target.position + offset);
        transform.position = screenPos;
    }
}