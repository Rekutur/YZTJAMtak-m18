using UnityEngine;

public class ColorButton : MonoBehaviour
{
    public ColorPuzzleManager puzzleManager;

    void OnMouseDown()
    {
        Debug.Log($"{gameObject.name} butonuna tıklandı.");

        if (puzzleManager != null)
            puzzleManager.RegisterClick(gameObject);
        else
            Debug.LogWarning("‼️ PuzzleManager atanmadı!");
    }
}
