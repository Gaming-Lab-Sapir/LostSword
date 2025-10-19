using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 12f;
    [SerializeField] float arrowLifetime = 10f;
    [SerializeField] int damage = 10;   

    Rigidbody2D rb;
    Vector2 direction;

    public void Initialize(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;
        if (direction != Vector2.zero)
            transform.right = direction;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.linearVelocity = direction * arrowSpeed;
        Destroy(gameObject, arrowLifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var boss = other.GetComponentInParent<BossVampire>();
        if (boss)
        {
            boss.OnArrowHit(damage);
            Destroy(gameObject);
            return;
        }
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>()?.HitByArrow();
            Destroy(gameObject);
            return;
        }
        if (other.CompareTag("Walls"))
        {
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Gate"))
        {
            Destroy(gameObject);
            return;
        }
    }
}
