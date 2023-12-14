using System.Collections;
using System.Collections.Generic;
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
    private bool readyToJump, crouch;
    private bool dead;
    private float orgSize, newSize, orgPos, newPos;
    private int lives;
    private bool faceRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        readyToJump = true;
        dead = false;
        lives = 5;
        crouch = false;
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

        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.bodyType = RigidbodyType2D.Static;
            Crouch();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            GetUp();
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
