using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] List<int> _levels = new List<int>();
    [SerializeField] int _level = 0;
    [SerializeField] int _offsetX = 10;
    private Camera _cam = null;
    private GameObject _player = null;

    private void Awake()
    {
        _cam = Camera.main;
        _player = FindAnyObjectByType<PlayerMovement>().GameObject();
    }

    private void LoadLevel(int index) {
        _player.transform.position = new Vector3(gameObject.transform.position.x + _offsetX, _player.transform.position.y, _player.transform.position.z);
        _cam.transform.position = new Vector3(_levels[index], _cam.transform.position.y, _cam.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject.CompareTag("Player")) {
            LoadLevel(_level);
        }
    }
}
