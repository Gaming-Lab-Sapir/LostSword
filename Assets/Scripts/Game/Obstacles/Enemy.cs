using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float destroyDelay = 0.5f;
    [SerializeField] float arrowHitDelay = 0.05f;
    [SerializeField] private int enemyDamage = 10;
    [SerializeField] Transform target;
    bool isActive = true;
    Rigidbody2D rb;
    Animator animator;//also here there is no animations for enemy for now.

    public event Action OnEnemyDestroyed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (!target && GameObject.FindGameObjectWithTag("Player"))
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (isActive && target)
            rb.linearVelocity = ((Vector2)(target.position - transform.position)).normalized * moveSpeed;
        else
            rb.linearVelocity = Vector2.zero;
    }

    public void HitByArrow()
    {
        if (!isActive) return;
        isActive = false;
        GameEvents.RaiseEnemyKilledByArrow();
        StartCoroutine(WaitToDestroy(arrowHitDelay));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isActive) return;
        if (other.gameObject.CompareTag("Player"))
        {
            GameEvents.RaisePlayerDamaged(enemyDamage);
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(WaitToDestroy(destroyDelay));
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
