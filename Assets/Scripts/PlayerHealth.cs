using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private RawImage[] life;
    [SerializeField] private PanelManager _panelManager;
    private int hearts;
    private string enemy;
    private string eBullet;
    private bool isInvincible = false;
    private float invincibleSec = 1.5f;
    private float invincibleDelta = 0.15f;


    private void Start()
    {
        hearts = 5;
    }
    private void Update()
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

        if (isInvincible)
        {
            enemy = "";
            eBullet = "";
        }
        else if (!isInvincible)
        {
            enemy = "Enemy";
            eBullet = "EnemyBullet";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(enemy) || collision.gameObject.CompareTag(eBullet))
        {
            if (hearts > 0)
            {
                UnityEngine.Debug.Log("Herido");
                hearts--;
                BecomeInvincible();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(enemy) || collision.gameObject.CompareTag(eBullet))
        {
            if (hearts > 0)
            {
                UnityEngine.Debug.Log("Herido");
                hearts--;
                BecomeInvincible();
            }
        }
    }

    private void BecomeInvincible()
    {
        if (!isInvincible)
        {
            StartCoroutine(Invincibility());
        }
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        for (float i = 0; i < invincibleSec; i += invincibleDelta)
        {
            yield return new WaitForSeconds(invincibleDelta);
        }
        isInvincible = false;

    }
}
