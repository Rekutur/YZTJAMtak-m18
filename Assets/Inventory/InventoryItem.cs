using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;      // item ad�
    public Sprite icon;          // UI�daki simge
    public GameObject prefab;    // d�nyada spawn edilen obje
}
