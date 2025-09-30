using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI countText;

    public void Set(Sprite sprite, int count)
    {
        if (icon)
        {
            icon.sprite = sprite;
            icon.enabled = sprite != null;
            icon.preserveAspect = true;
        }

        if (countText)
            countText.text = count > 1 ? count.ToString() : "";
    }
}
