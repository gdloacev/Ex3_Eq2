using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Transform _shootRespawn;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _coldDownFire = 1f;
    [SerializeField] private float _fireForce = 10f;

    private Animator _animator = null;
    private BossLife _life = null;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _life = GetComponent<BossLife>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_life.isDied()) {
            _animator.Play("Shoot");
            StartCoroutine("Shoot");
        }
    }

    private void Update() {
        if (_life != null && _life.isDied()) StopCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(_coldDownFire);
            GameObject bullet = Instantiate(_bullet, _shootRespawn.position, Quaternion.identity);

            Rigidbody2D balaRigidbody = bullet.GetComponent<Rigidbody2D>();
            balaRigidbody.AddForce(Vector2.left * _fireForce, ForceMode2D.Impulse);
        }
    }
}