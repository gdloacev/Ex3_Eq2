using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    [SerializeField] private Transform _shootRespawn;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _coldDownFire = 1f;
    [SerializeField] private float _fireForce = 10f;
    [SerializeField] private AudioSource _audioSource = null;
    [SerializeField] private AudioClip _audioShoot = null;

    private Animator _animator = null;
    private BossLife _life = null;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _life = GetComponent<BossLife>();
        _audioSource = GetComponent<AudioSource>();
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
            _audioSource.PlayOneShot(_audioShoot);
            GameObject bullet = Instantiate(_bullet, _shootRespawn.position, Quaternion.identity);

            Rigidbody2D balaRigidbody = bullet.GetComponent<Rigidbody2D>();
            balaRigidbody.AddForce(Vector2.left * _fireForce, ForceMode2D.Impulse);
        }
    }
}
