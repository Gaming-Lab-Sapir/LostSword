using UnityEngine;

public class DevilTrap : MonoBehaviour
{
    [SerializeField] private int trapDamage = 10;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;  

        if (other.CompareTag("Player"))
        {
            GameEvents.RaisePlayerDamaged(trapDamage);
            hasTriggered = true;
        }
    }
}


