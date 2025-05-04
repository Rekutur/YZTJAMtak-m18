using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [HideInInspector] public InventoryItem currentItem;
    public Image icon;  // Slot i�indeki ItemImage objesine ba�lanacak

    public bool IsEmpty => currentItem == null;

    // Envantere item ekler
    public void AddItem(InventoryItem item)
    {
        currentItem = item;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    // Slot�u temizler
    public void ClearSlot()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
