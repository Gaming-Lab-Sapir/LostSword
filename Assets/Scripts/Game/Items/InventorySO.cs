using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "Scriptable Objects/InventorySO", order = 1)]
public class InventorySO : ScriptableObject
{
    [Serializable]
    public class ItemStack
    {
        public ItemSO item;
        public int count;
    }

    public List<ItemStack> items = new List<ItemStack>();

    public void AddItem(ItemSO item, int amount = 1)
    {
        foreach (var stack in items)
        {
            if (stack.item == item)
            {
                stack.count += amount;
                return;
            }
        }

        items.Add(new ItemStack { item = item, count = amount });
    }

    public bool RemoveItem(ItemSO item, int amount = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
            {
                if (items[i].count >= amount)
                {
                    items[i].count -= amount;
                    if (items[i].count <= 0)
                        items.RemoveAt(i);
                    return true;
                }
            }
        }
        return false;
    }

    public int GetCount(ItemSO item)
    {
        foreach (var stack in items)
        {
            if (stack.item == item)
                return stack.count;
        }
        return 0;
    }

    public bool HasItem(ItemSO item, int amount = 1)
    {
        return GetCount(item) >= amount;
    }
}
