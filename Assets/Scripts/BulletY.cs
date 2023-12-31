using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletY : MonoBehaviour
{
    [SerializeField] private float velY;
    private float velX = 0f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velX, velY);
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) Destroy(gameObject);
    }
}
