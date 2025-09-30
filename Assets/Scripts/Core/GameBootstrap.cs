using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    public InventorySO playerInventory;
    private static bool initialized;

    private void Awake()
    {
        if (initialized) return;
        initialized = true;

        if (playerInventory != null)
            playerInventory.items.Clear(); 
    }
}
