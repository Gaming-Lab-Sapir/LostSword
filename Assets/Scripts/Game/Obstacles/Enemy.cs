using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;    
    [SerializeField] float deathDelay = 0.7f;      
    [SerializeField] private int enemyDamage = 10;
    Transform target;


    bool isActive = true;
    Rigidbody2D rb;
    Animator animator;

    public event Action OnEnemyDestroyed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (!target && GameObject.FindGameObjectWithTag("Player"))
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (isActive && target)
        {
            if(animator != null)
            {
                animator.SetBool("Moving", true);
                animator.SetFloat("MoveX", (target.position.x - transform.position.x));
                animator.SetFloat("MoveY", (target.position.y - transform.position.y));
            }
            rb.linearVelocity = ((Vector2)(target.position - transform.position)).normalized * moveSpeed;
        }
        else
        {
            if (animator != null) animator.SetBool("Moving", false);
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void HitByArrow()
    {
        if (!isActive) return;
        isActive = false;

        rb.linearVelocity = Vector2.zero;
        if (animator != null) animator.SetTrigger("Death");                 
        StartCoroutine(WaitToDestroy(deathDelay));   
        GameEvents.RaiseEnemyKilledByArrow();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isActive) return;
        if (other.gameObject.CompareTag("Player"))
        {
            GameEvents.RaisePlayerDamaged(enemyDamage);

            isActive = false;
            rb.linearVelocity = Vector2.zero;
            if (animator != null) animator.SetTrigger("Death");            
            StartCoroutine(WaitToDestroy(deathDelay));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;
        if (other.CompareTag("Arrow"))
            HitByArrow();
    }

    IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        OnEnemyDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
