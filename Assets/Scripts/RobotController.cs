using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    private Animator animator;
    private Transform target;
    private Rigidbody2D m_Rigidbody;

    [SerializeField] private float speed = 1;
    [SerializeField] private float maxRange = 10;
    private float attackRange = 3;
    private BoxCollider2D _collider = null;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        target = FindObjectOfType<PlayerMovement>().transform;
        _collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= attackRange)
        {
            animator.SetBool("withinRangeAttack", true);
        }
        else if (Vector3.Distance(target.position, transform.position) <= maxRange)
        {
            FollowPlayer();
        } 
        else
        {
            animator.SetBool("withinRangeView", false);
            animator.SetBool("withinRangeAttack", false);
        }
        
    }

    public void FollowPlayer()
    {
        if (animator.GetBool("isDead") == false)
        { 
            animator.SetBool("withinRangeAttack", false);
            animator.SetBool("withinRangeView", true);
            animator.SetFloat("moveX",(target.position.x - transform.position.x));
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "PlayerBullet")
        {
            animator.SetTrigger("isDead");
            GetComponent<RobotController>().enabled = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            animator.SetTrigger("isDead");
            _collider.isTrigger = true;
            gameObject.tag = "Untagged";
        }
    }
}
