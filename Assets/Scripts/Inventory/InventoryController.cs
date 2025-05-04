using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("Atamak için sürükle býrak")]
    public GameObject inventoryPanel;

    private bool isOpen = false;

    void Start()
    {
        // Baþlangýçta kapalý olsun
        inventoryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // “E” tuþuna basýldýðýnda aç/kapa
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);

            // Açýkken fareyi serbest býrak, kapalýyken kilitle
            if (isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
