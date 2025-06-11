using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private float rotateSpeed = 180f;
    [SerializeField] private float distanceFromPlayer = 0.3f;

    private Transform weapon;


    private void Start()
    {
        GameObject weaponGO = Instantiate(weaponPrefab);
        weaponGO.transform.SetParent(transform);
        weapon = weaponGO.transform;
        weapon.localPosition = Vector3.right * distanceFromPlayer;
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}