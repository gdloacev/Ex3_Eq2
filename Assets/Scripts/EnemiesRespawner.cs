using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesRespawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab = null;
    [SerializeField] private int _enemyRespawnTime = 3;

    IEnumerator Respawn()
    {
        while (true)
        {
            GameObject enemy = Instantiate(_enemyPrefab, gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_enemyRespawnTime);
            GameObject enemy1 = Instantiate(_enemyPrefab, gameObject.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject.CompareTag("Player")) { 
            StartCoroutine(Respawn());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(Respawn());
        }
    }
}
