using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickable : MonoBehaviour
{
    [Header("Toplanacak Item")]
    public InventoryItem item;       // Inspector'dan s�r�kle-b�rak
    public float pickupRange = 2f;   // Al�nma mesafesi

    Transform playerCam;

    void Start()
    {
        // Kamera referans� al
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
        // Mesafeyi her frame yazd�r (sadece test i�in):
        Debug.Log($"Pickable distance to camera: {dist}");
        if (dist > pickupRange) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F bas�ld�, deneme ba�l�yor.");
            bool added = InventoryManager.Instance.AddItem(item);
            Debug.Log("AddItem d�nd�: " + added);
            if (added)
            {
                Destroy(gameObject);
            }
        }
    }

}
