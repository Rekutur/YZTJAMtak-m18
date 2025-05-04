using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private InventorySlotUI originSlot;
    private InventoryItem draggedItem;
    private Image dragIcon;
    private RectTransform dragRect;
    private Canvas canvas;
    private GameObject ghostObject;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        var iconTransform = canvas.transform.Find("DraggingIcon");
        if (iconTransform != null)
        {
            dragIcon = iconTransform.GetComponent<Image>();
            dragRect = iconTransform.GetComponent<RectTransform>();
        }
    }

    public void OnBeginDrag(PointerEventData e)
    {
        originSlot = GetComponent<InventorySlotUI>();
        if (originSlot == null || originSlot.IsEmpty)
        {
            draggedItem = null;
            return;
        }
        draggedItem = originSlot.currentItem;
        originSlot.ClearSlot();

        // UI ikonu göster
        dragIcon.sprite = draggedItem.icon;
        dragIcon.enabled = true;
        dragIcon.color = Color.white;
        dragRect.sizeDelta = new Vector2(64, 64);
        UpdateIconPosition(e);

        // 3D hayalet objeyi oluþtur
        if (draggedItem.prefab != null)
        {
            ghostObject = Instantiate(draggedItem.prefab);
            var col = ghostObject.GetComponent<Collider>();
            if (col) Destroy(col);
            foreach (var rb in ghostObject.GetComponentsInChildren<Rigidbody>())
                Destroy(rb);
            SetAlphaRecursively(ghostObject, 0.5f);
        }
    }

    public void OnDrag(PointerEventData e)
    {
        if (draggedItem == null) return;
        UpdateIconPosition(e);

        if (ghostObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(e.position);
            Plane ground = new Plane(Vector3.up, Vector3.zero);
            if (ground.Raycast(ray, out float enter))
            {
                ghostObject.transform.position = ray.GetPoint(enter);
            }
        }
    }

    public void OnEndDrag(PointerEventData e)
    {
        if (draggedItem == null) return;
        dragIcon.enabled = false;

        bool droppedInSlot = false;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(e, results);
        foreach (var r in results)
        {
            var slot = r.gameObject.GetComponent<InventorySlotUI>();
            if (slot != null && slot.IsEmpty)
            {
                slot.AddItem(draggedItem);
                droppedInSlot = true;
                break;
            }
        }

        // Dünya objesine drop
        if (!droppedInSlot && ghostObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(e.position);
            if (Physics.Raycast(ray, out var hit, 100f))
            {
                var target = hit.collider.GetComponent<ItemDropTarget>();
                if (target != null)
                {
                    target.OnItemDropped(draggedItem);
                    droppedInSlot = true;
                }
            }
        }

        // Hiçbir yere býrakýlmadýysa geri koy
        if (!droppedInSlot)
        {
            originSlot.AddItem(draggedItem);
        }

        // Hayaleti yok et
        if (ghostObject != null) Destroy(ghostObject);

        draggedItem = null;
        originSlot = null;
    }

    private void UpdateIconPosition(PointerEventData e)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            e.position, e.pressEventCamera,
            out Vector2 pos);
        dragRect.anchoredPosition = pos;
    }

    private void SetAlphaRecursively(GameObject go, float alpha)
    {
        foreach (var rend in go.GetComponentsInChildren<Renderer>())
        {
            foreach (var mat in rend.materials)
            {
                if (mat.HasProperty("_Color"))
                {
                    var c = mat.color;
                    c.a = alpha;
                    mat.color = c;
                    mat.SetFloat("_Mode", 2);
                    mat.EnableKeyword("_ALPHABLEND_ON");
                }
            }
        }
    }
}
