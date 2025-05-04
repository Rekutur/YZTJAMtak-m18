using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item Definition")]
public class ItemDefinition : ScriptableObject
{
    public string itemName;
    public Sprite icon;
}
