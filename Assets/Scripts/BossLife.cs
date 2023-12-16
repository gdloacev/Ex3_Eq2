using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : MonoBehaviour
{
    [SerializeField] private int _bossLife = 10;
    [SerializeField] private GameObject _tag = null;
    [SerializeField] private AudioClip _audioDie = null;
    private Animator _animator = null;
    private BoxCollider2D _boxCollider = null;
    private AudioSource _audioSource = null;

    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("PlayerBullet")) {
            _bossLife--;
            Die();
        }
    }

    private void Update()
    {
        Die();
    }

    private void Die() {
        if (_bossLife <= 0 && !_boxCollider.isTrigger)
        {
            UnityEngine.Debug.Log("Muerto");
            _animator.Play("Die");
            _audioSource.PlayOneShot(_audioDie);
            _boxCollider.isTrigger = true;
            if (_tag != null)
            {
                _tag.tag = gameObject.tag;
                _tag.SetActive(false);
            }

        }
    }

    public bool isDied() {
        return _bossLife <= 0;
    }
}
