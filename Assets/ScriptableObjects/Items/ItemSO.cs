using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO", order = 0)]
public class ItemSO : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public ItemType type = ItemType.Other;
    public int maxStack = 99;
    public int healAmount;
    public int ammoAmount;
    public bool isQuestItem;
    public string questIdRequired;
}
