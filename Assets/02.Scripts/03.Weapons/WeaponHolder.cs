using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public float rotateSpeed = 180f;

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}