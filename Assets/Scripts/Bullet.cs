using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform _target = null;
    [SerializeField] private float _bulletLife = 5f;

    private void Awake()
    {
        _target = FindAnyObjectByType<PlayerMovement>().transform;
    }

    void Update()
    {
        Vector3.MoveTowards(gameObject.transform.position, _target.position, 0.5f);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player")) Destroy(gameObject);
    }
}
