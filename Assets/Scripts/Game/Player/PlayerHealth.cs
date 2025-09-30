using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealthPoints = 100;
    [SerializeField] float invincibilityDurationSeconds = 0.8f;
    [SerializeField] GameObject gameOverText;

    public int MaxHealthPoints => maxHealthPoints;
    public int CurrentHealthPoints { get; private set; }

    float lastDamageTime = float.NegativeInfinity;
    SpriteRenderer sprite;

    void OnEnable() { GameEvents.PlayerDamaged += OnPlayerDamaged; }
    void OnDisable() { GameEvents.PlayerDamaged -= OnPlayerDamaged; }

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        CurrentHealthPoints = maxHealthPoints;
    }

    void OnPlayerDamaged(int amount)
    {
        TakeDamage(amount);
    }

    public void TakeDamage(int damageAmount)
    {
        if (damageAmount > 0)
        {
            if (Time.time - lastDamageTime < invincibilityDurationSeconds) return;
            lastDamageTime = Time.time;

            CurrentHealthPoints = Mathf.Max(0, CurrentHealthPoints - damageAmount);
            if (CurrentHealthPoints == 0) Die();
        }
        else if (damageAmount < 0)
        {
            int healAmount = -damageAmount;
            CurrentHealthPoints = Mathf.Min(maxHealthPoints, CurrentHealthPoints + healAmount);
        }
    }

    public void ForceDamage(int damageAmount)
    {
        if (damageAmount <= 0) return;
        CurrentHealthPoints = Mathf.Max(0, CurrentHealthPoints - damageAmount);
        if (CurrentHealthPoints == 0) Die();
    }

    void Die()
    {
        if (TryGetComponent<PlayerMovement>(out var move)) move.enabled = false;
        if (TryGetComponent<PlayerShoot>(out var shoot)) shoot.enabled = false;
        if (TryGetComponent<Rigidbody2D>(out var rb)) rb.linearVelocity = Vector2.zero;
        if (TryGetComponent<Collider2D>(out var col)) col.enabled = false;
        if (sprite) sprite.enabled = false;
        if (gameOverText) gameOverText.SetActive(true);
        GameEvents.RaiseGameLost();
        Time.timeScale = 0f;
    }
}
