using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundScript : MonoBehaviour
{
    [SerializeField] private AudioSource playerHit, playerDeath, playerRun, playerJump, playerShoot;
    private PlayerMovement pmove;
    private bool run;

    private string enemy;
    private string eBullet;

    // Start is called before the first frame update
    void Start()
    {
        pmove = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && pmove.GetReadyJump())
        {
            playerJump.Play();
        }

        if (Input.GetAxisRaw("Horizontal") != 0 && pmove.isGrounded() && !pmove.GetCrouch())
        {
            run = true;
        }
        else
        {
            run = false;
        }

        if (run && !playerRun.isPlaying)
        {
            playerRun.Play();
        }
        else if (!run)
        {
            playerRun.Stop();
        }

        if (Input.GetMouseButton(0))
        {
            playerShoot.Play();
        }

        if (pmove.GetInvincible())
        {
            enemy = "";
            eBullet = "";
        }else if (!pmove.GetInvincible())
        {
            enemy = "Enemy";
            eBullet = "EnemyBullet";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(enemy) || collision.gameObject.CompareTag(eBullet))
        {
            if (pmove.GetLivesCount() > 0)
            {
                playerHit.Play();
            }
            else if (pmove.GetLivesCount() == 0)
            {
                playerDeath.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(enemy) || collision.gameObject.CompareTag(eBullet))
        {
            if (pmove.GetLivesCount() > 0)
            {
                playerHit.Play();
            }
            else if (pmove.GetLivesCount() == 0)
            {
                playerDeath.Play();
            }
        }
    }
}
