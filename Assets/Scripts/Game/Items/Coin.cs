using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator != null) animator.Play("Coin_Spin");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameEvents.RaiseCoinCollected(value);
        Destroy(gameObject);
    }
}
