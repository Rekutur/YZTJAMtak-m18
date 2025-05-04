using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickable : MonoBehaviour
{
    [Header("Toplanacak Item")]
    public InventoryItem item;       // Inspector'dan sürükle-býrak
    public float pickupRange = 2f;   // Alýnma mesafesi

    Transform playerCam;

    void Start()
    {
        // Kamera referansý al
        playerCam = Camera.main.transform;
    }

    void Update()
    {
        if (playerCam == null)
        {
            Debug.LogError("Pickable: playerCam null!");
            return;
        }
        float dist = Vector3.Distance(transform.position, playerCam.position);
        // Mesafeyi her frame yazdýr (sadece test için):
        Debug.Log($"Pickable distance to camera: {dist}");
        if (dist > pickupRange) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F basýldý, deneme baþlýyor.");
            bool added = InventoryManager.Instance.AddItem(item);
            Debug.Log("AddItem döndü: " + added);
            if (added)
            {
                Destroy(gameObject);
            }
        }
    }

}
