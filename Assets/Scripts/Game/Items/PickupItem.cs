using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupItem : MonoBehaviour
{
    public ItemSO item;
    public int amount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var inventoryHolder = other.GetComponent<PlayerInventory>();
            if (inventoryHolder != null)
            {
                inventoryHolder.inventory.AddItem(item, amount);
                Destroy(gameObject); 
            }
        }
    }
}
