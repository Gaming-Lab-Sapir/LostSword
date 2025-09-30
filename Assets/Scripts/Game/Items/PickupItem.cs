using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupItem : MonoBehaviour
{
    public ItemSO item;
    public int amount = 1;
    public bool destroyOnPickup = true;

    [Header("Consumables")]
    public bool consumeOnPickup = false;

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

        int toAdd = amount;
        if (item.type == ItemType.Ammo && item.ammoAmount > 0)
            toAdd = item.ammoAmount;

        invHolder.inventory.AddItem(item, toAdd);

        switch (item.type)
        {
            case ItemType.Coin:
                GameEvents.RaiseCoinCollected(amount);
                break;

            case ItemType.Ammo:
                GameEvents.RaiseArrowPickedUp(toAdd);
                break;

            case ItemType.HealthPotion:
                if (consumeOnPickup)
                {
                    if (item.healAmount != 0)
                    {
                        if (item.healAmount > 0)
                            GameEvents.RaisePlayerDamaged(-item.healAmount);
                        else
                        {
                            var hp = other.GetComponent<PlayerHealth>();
                            if (hp != null) hp.ForceDamage(-item.healAmount);
                            else GameEvents.RaisePlayerDamaged(-item.healAmount);
                        }
                    }
                    invHolder.inventory.RemoveItem(item, 1);
                }
                break;

            case ItemType.QuestItem:
            case ItemType.Other:
                Debug.Log($"{item.displayName} collected");
                break;
        }

        if (destroyOnPickup) Destroy(gameObject);
    }
}
