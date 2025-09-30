using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] InventorySO inventory;

    [Header("UI")]
    [SerializeField] Transform gridRoot;
    [SerializeField] GameObject slotPrefab;

    void OnEnable()
    {
        if (inventory)
            inventory.Changed += Refresh;

        Refresh();
    }

    void OnDisable()
    {
        if (inventory)
            inventory.Changed -= Refresh;
    }

    public void Refresh()
    {
        for (int i = gridRoot.childCount - 1; i >= 0; i--)
            Destroy(gridRoot.GetChild(i).gameObject);

        if (!inventory || !slotPrefab)
            return;

        foreach (var s in inventory.items)
        {
            if (!s.item)
                continue;

            var go = Instantiate(slotPrefab, gridRoot);
            go.GetComponent<ItemSlotUI>()?.Set(s.item.icon, s.count);
        }
    }
}
