using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;      // item adý
    public Sprite icon;          // UI’daki simge
    public GameObject prefab;    // dünyada spawn edilen obje
}
