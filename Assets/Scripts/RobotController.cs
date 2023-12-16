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
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = FindObjectOfType<PlayerMovement>().transform;

    }

    // Update is called once per frame
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
        if (other.collider.tag == "Player")
        {
            //Destroy(other.gameObject);
            //Colocar aquí evento cuando el enemigo collisione con el player
        } 
        else if (other.collider.tag == "PlayerBullet")
        {
            animator.SetTrigger("isDead");
            //GetComponent<Animator>().enabled = false;
            GetComponent<RobotController>().enabled = false;

        }
    }
}
