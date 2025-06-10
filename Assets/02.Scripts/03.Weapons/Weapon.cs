using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float rotateSpeed = 180f;
    private Transform center;

    public void Init(Transform target)
    {
        center = target;
    }

    private void Update()
    {
        if (center == null) return;

        transform.RotateAround(center.position, Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Trigger ¹ß»ý: {collision.name}, Tag: {collision.tag}");

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit!");
        }
    }
}
