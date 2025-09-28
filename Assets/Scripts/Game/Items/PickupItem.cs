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

        switch (item.type)
        {
            case ItemType.Coin:
                invHolder.inventory.AddItem(item, amount);
                GameEvents.RaiseCoinCollected(amount);
                break;

            case ItemType.Ammo:
                invHolder.inventory.AddItem(item, item.ammoAmount);
                GameEvents.RaiseArrowPickedUp(item.ammoAmount);
                break;

            case ItemType.HealthPotion:
                if (item.healAmount > 0)
                {
                    GameEvents.RaisePlayerDamaged(-item.healAmount);
                }
                else if (item.healAmount < 0)
                {
                    var hp = other.GetComponent<PlayerHealth>();
                    if (hp != null)
                        hp.ForceDamage(-item.healAmount);
                    else
                        GameEvents.RaisePlayerDamaged(-item.healAmount);
                }
                break;

            case ItemType.QuestItem:
            case ItemType.Other:
                invHolder.inventory.AddItem(item, amount);
                Debug.Log($"{item.displayName} collected");
                break;
        }

        if (destroyOnPickup) Destroy(gameObject);
    }
}
