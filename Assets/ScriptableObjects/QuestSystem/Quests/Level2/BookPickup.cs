using UnityEngine;

public class BookPickup : MonoBehaviour
{
    [SerializeField] private string bookId;       
    [SerializeField] private bool destroyOnPickup = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameEvents.RaiseBookCollected(bookId);

        if (destroyOnPickup) Destroy(gameObject);
        else gameObject.SetActive(false);
    }
}
