using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int spikeDamage = 10;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerHealth>()?.TakeDamage(spikeDamage);
    }
    public void EnableHitbox() => GetComponent<Collider2D>().enabled = true;
    public void DisableHitbox() => GetComponent<Collider2D>().enabled = false;

}
