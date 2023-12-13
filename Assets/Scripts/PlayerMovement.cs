using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private bool readyToJump;
    private bool dead;
    private int lives;
    private bool faceRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        readyToJump = true;
        dead = false;
        lives = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        else if (dead)
        {
            horizontal = 0;

        }

        transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);
        
        if (horizontal > 0 && !faceRight)
        {
            Flip();
        }
        if (horizontal < 0 && faceRight)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.Space) && readyToJump)
        {
            if (isGrounded())
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                readyToJump = false;

            }
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
        }
    }

    private void Flip()
    {
        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        faceRight = !faceRight;
    }
}
