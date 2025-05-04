using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("Atamak i�in s�r�kle b�rak")]
    public GameObject inventoryPanel;

    private bool isOpen = false;

    void Start()
    {
        // Ba�lang��ta kapal� olsun
        inventoryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // �E� tu�una bas�ld���nda a�/kapa
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);

            // A��kken fareyi serbest b�rak, kapal�yken kilitle
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
