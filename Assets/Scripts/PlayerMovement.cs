using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator animTorso;
    [SerializeField] private Animator animLegs;
    [SerializeField] private Animator animSplat, animPool;
    [SerializeField] private GameObject bulletRight, bulletLeft, bulletUp, bulletDown, bloodSplat, bloodPool;
    [SerializeField] private float fireRate = 0.5f;
    private Vector2 bulletPos, splatPos, poolPos;
    private float nextFire = 0.0f;


    //[SerializeField] private BulletBehavior bullet;
    //[SerializeField] private Transform launchOffset;
    private bool readyToJump, crouch;
    private bool dead;
    private float orgSize, newSize, orgPos, newPos;
    private int lives;
    private bool faceRight = true;
    private bool faceUp = false;
    private bool faceDown = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        readyToJump = true;
        dead = false;
        lives = 5;
        orgSize = bc.size.y;
        orgPos = bc.offset.y;
        newSize = bc.size.y * 0.5f;
        newPos = bc.offset.y * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            if (!crouch)
            {
                transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);
            }
        
        if (horizontal > 0 && !faceRight)
        {
            Flip();
        }
        if (horizontal < 0 && faceRight)
        {
            Flip();
        }

        if (faceRight)
        {
            animTorso.SetFloat("moveX", 1f);
            animLegs.SetFloat("moveX", 1f);
        }
        else if (!faceRight)
        {
            animTorso.SetFloat("moveX", 0f);
            animLegs.SetFloat("moveX", 0f);
        }

        if (horizontal < 0)
        {
            animTorso.SetBool("run", true);
            animLegs.SetBool("run", true);
            animTorso.SetBool("idle", false);
            animLegs.SetBool("idle", false);
        }
        else if (horizontal > 0)
        {
            animTorso.SetBool("run", true);
            animLegs.SetBool("run", true);
            animTorso.SetBool("idle", false);
            animLegs.SetBool("idle", false);
        }

        if (horizontal == 0)
        {
            animTorso.SetBool("idle",true);
            animLegs.SetBool("idle", true);
            animTorso.SetBool("run", false);
            animLegs.SetBool("run", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && readyToJump)
        {
            if (isGrounded())
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                readyToJump = false;
                animTorso.SetBool("grounded", false);
                animLegs.SetBool("grounded", false);
                if (horizontal == 0)
                {
                     animTorso.SetFloat("speedY", 0f);
                     animLegs.SetFloat("speedY", 0f);
                }
                else
                {
                     animTorso.SetFloat("speedY", 1f);
                     animLegs.SetFloat("speedY", 1f);
                }
                
            }
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isGrounded())
            {
                    if (horizontal == 0)
                    {
                        rb.bodyType = RigidbodyType2D.Static;
                        animTorso.SetBool("crouch", true);
                        animLegs.SetBool("crouch", true);
                        animTorso.SetBool("run", false);
                        animLegs.SetBool("run", false);
                        animTorso.SetBool("idle", false);
                        animLegs.SetBool("idle", false);
                        crouch = true;
                        Crouch();
                    }
            }
            else if (!isGrounded())
            {
                animTorso.SetBool("faceDown", true);
                faceDown = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
                if (isGrounded())
                {
                    if (horizontal == 0)
                    {
                        rb.bodyType = RigidbodyType2D.Dynamic;
                        animTorso.SetBool("crouch", false);
                        animLegs.SetBool("crouch", false);
                        crouch = false;
                        GetUp();
                    }
                }
                else if (!isGrounded())
                {
                    animTorso.SetBool("faceDown", false);
                    faceDown = false;
                }
            }


        if (Input.GetKeyDown(KeyCode.W) && !crouch)
        {
            animTorso.SetBool("faceUp", true);
            faceUp = true;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            animTorso.SetBool("faceUp", false);
            faceUp = false;
        }

        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (crouch)
            {
                    animTorso.SetTrigger("crouch_shoot");
            }else if (!crouch)
            {
                    if (!faceUp && !faceDown)
                    {
                        animTorso.SetTrigger("shoot");
                    }
                    else if (faceUp && !faceDown)
                    {
                        animTorso.SetTrigger("shootUp");
                    }
                    else if (faceDown && !faceUp)
                    {
                        animTorso.SetTrigger("shootDown");
                    }
            }
            Fire();
        }

        }
        else if (dead)
        {
            horizontal = 0;
            animTorso.SetBool("run", false);
            animLegs.SetBool("run", false);
            animTorso.SetBool("idle", true);
            animLegs.SetBool("idle", true);
            animTorso.SetBool("faceUp", false);
            animTorso.SetBool("faceDown", false);
        }

    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            readyToJump = true;
            animTorso.SetBool("grounded", true);
            animLegs.SetBool("grounded", true);
            animTorso.SetFloat("speedY", 0f);
            animLegs.SetFloat("speedY", 0f);
            animTorso.SetBool("faceDown", false);
            faceDown = false;

        }

        Hurt(collision.gameObject);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hurt(collision.gameObject);    
    }

    private void Hurt(GameObject collision) {
        if (collision.tag == "Enemy" || collision.tag == "EnemyBullet")
        {
            splatPos = transform.position;
            poolPos = transform.position;
            if (lives > 1)
            {
                lives--;
                if (faceRight)
                {
                    splatPos += new Vector2(+1f, +1f);
                    poolPos += new Vector2(0f, +0.2f);
                }
                else if (!faceRight)
                {
                    splatPos += new Vector2(-1f, +1f);
                    poolPos += new Vector2(0f, +0.2f);
                }
                animTorso.SetTrigger("hurt");
                animLegs.SetTrigger("hurt");
                animSplat.SetTrigger("hurt");
                Instantiate(bloodSplat, splatPos, Quaternion.identity);
            }
            else if (lives == 1)
            {
                Instantiate(bloodPool, poolPos, Quaternion.identity);
                Instantiate(bloodSplat, splatPos, Quaternion.identity);
                animPool.SetBool("dead", true);
                animTorso.SetTrigger("deadT");
                animLegs.SetTrigger("deadT");
                lives--;
                dead = true;
            }
        }
    }

    private void Fire()
    {
        bulletPos = transform.position;
        if (faceRight)
        {
            if (!faceUp && !faceDown)
            {
                bulletPos += new Vector2(+1f, +1f);
                Instantiate(bulletRight, bulletPos, Quaternion.identity);
            }
            else
            {
                if (faceUp && !crouch)
                {
                    bulletPos += new Vector2(0f, +2.5f);
                    Instantiate(bulletUp, bulletPos, Quaternion.identity);
                }
                else if (faceDown)
                {
                    bulletPos += new Vector2(0f, -0.5f);
                    Instantiate(bulletDown, bulletPos, Quaternion.identity);
                }
            }
        }
        else if (!faceRight)
        {
            if (!faceUp && !faceDown)
            {
                bulletPos += new Vector2(-1f, +1f);
                Instantiate(bulletLeft, bulletPos, Quaternion.identity);
            }
            else
            {
                if (faceUp && !crouch)
                {
                    bulletPos += new Vector2(0f, +2.5f);
                    Instantiate(bulletUp, bulletPos, Quaternion.identity);
                }
                else if (faceDown)
                {
                    bulletPos += new Vector2(0f, -0.5f);
                    Instantiate(bulletDown, bulletPos, Quaternion.identity);
                }
            }
        }
    }

    private void Crouch()
    {
        bc.size = new Vector2(bc.size.x, newSize);
        bc.offset = new Vector2(bc.offset.x, newPos);
    }

    private void GetUp()
    {
        bc.size = new Vector2(bc.size.x, orgSize);
        bc.offset = new Vector2(bc.offset.x, orgPos);
    }

    private void Flip()
    {
        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        faceRight = !faceRight;
    }
}
