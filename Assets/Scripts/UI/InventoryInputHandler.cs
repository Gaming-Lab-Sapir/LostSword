using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel; 
    private InputSystem inputs;

    private void Awake()
    {
        inputs = new InputSystem();
        inputs.UI.ToggleInventory.performed += OnToggle;
    }

    private void OnEnable() => inputs.Enable();
    private void OnDisable() => inputs.Disable();

    private void OnToggle(InputAction.CallbackContext ctx)
    {
        if (!inventoryPanel) return;
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
