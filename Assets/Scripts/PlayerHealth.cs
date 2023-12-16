using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private RawImage[] life;
    private int hearts;

    // Start is called before the first frame update
    void Start()
    {
        hearts = 5;
    }

    // Update is called once per frame
    void Update()
    {
        switch (hearts)
        {
            case 0:
                LeanTween.scale(life[0].rectTransform, new Vector3(0f, 0f, 0f), 0.5f);
                break;
            case 1:
                LeanTween.scale(life[1].rectTransform, new Vector3(0f, 0f, 0f), 0.5f);
                break;
            case 2:
                LeanTween.scale(life[2].rectTransform, new Vector3(0f, 0f, 0f), 0.5f);
                break;
            case 3:
                LeanTween.scale(life[3].rectTransform, new Vector3(0f, 0f, 0f), 0.5f);
                break;
            case 4:
                LeanTween.scale(life[4].rectTransform, new Vector3(0f, 0f, 0f), 0.5f);
                break;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            if (hearts > 0)
            {
                UnityEngine.Debug.Log("Herido");
                hearts--;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            if (hearts > 0)
            {
                UnityEngine.Debug.Log("Herido");
                hearts--;
            }
        }
    }
}
