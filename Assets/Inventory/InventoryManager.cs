using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<InventorySlotUI> slots = new List<InventorySlotUI>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Envantere item eklemeye çalýþýr
    public bool AddItem(InventoryItem item)
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item);
                return true;
            }
        }
        Debug.Log("Envanter dolu!");
        return false;
    }
}
