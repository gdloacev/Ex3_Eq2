using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : MonoBehaviour
{
    [SerializeField] private int _bossLife = 10;
    private Animator _animator = null;
    private BoxCollider2D _boxCollider = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
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
        if (_bossLife <= 0)
        {
            UnityEngine.Debug.Log("Muerto");
            _animator.Play("Die");
            _boxCollider.isTrigger = true;
            gameObject.tag = "Untagged";
        }
    }

    public bool isDied() {
        return _bossLife <= 0;
    }
}
