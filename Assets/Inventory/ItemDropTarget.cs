using UnityEngine;

public class ItemDropTarget : MonoBehaviour
{
    [Header("Bu objenin kabul ettiği item")]
    public InventoryItem requiredItem;

    [Header("Başarılı drop sonrası açılacak portal ya da efekt")]
    public GameObject portalObject; // inspector’dan bağlayacağın portal prefab’ı ya da objesi

    bool used = false; // bir kere kullanıldığında tekrar etmesin

    public void OnItemDropped(InventoryItem item)
    {
        if (used) return;
        if (item == requiredItem)
        {
            Debug.Log("✅ Doğru item bırakıldı: " + item.itemName);
            used = true;
            if (portalObject != null)
            {
                portalObject.SetActive(true);
            }
            // İstersen burada animasyon, ses vs. de tetikleyebilirsin
        }
        else
        {
            Debug.Log("❌ Bu item işe yaramaz: " + item.itemName);
        }
    }
}
