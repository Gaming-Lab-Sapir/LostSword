using UnityEngine;

public class ToggleInventoryUI : MonoBehaviour
{
    [SerializeField] GameObject panel;

    public void Toggle()
    {
        if (panel)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
