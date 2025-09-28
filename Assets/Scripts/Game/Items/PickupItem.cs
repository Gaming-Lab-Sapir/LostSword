using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupItem : MonoBehaviour
{
    public ItemSO item;
    public int amount = 1;        
    public bool destroyOnPickup = true;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var invHolder = other.GetComponent<PlayerInventory>();
        if (invHolder == null || item == null) return;

        invHolder.inventory.AddItem(item, amount);

        switch (item.type)
        {
            case ItemType.Coin:
                GameEvents.RaiseCoinCollected(amount);
                break;

            case ItemType.Ammo:
                GameEvents.RaiseArrowPickedUp(item.ammoAmount);
                break;
        }

        if (destroyOnPickup) Destroy(gameObject);
    }
}
