using UnityEngine;

public class ArrowItem : MonoBehaviour
{
    [SerializeField] int arrowsGranted = 5;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        GameEvents.RaiseArrowPickedUp(arrowsGranted);
        Destroy(gameObject);
    }
}
