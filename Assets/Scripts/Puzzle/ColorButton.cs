using UnityEngine;

public class ColorButton : MonoBehaviour
{
    public ColorPuzzleManager puzzleManager;

    void OnMouseDown()
    {
        if (puzzleManager != null)
        {
            puzzleManager.RegisterClick(gameObject);
            Debug.Log(gameObject.name + " butonuna týklandý.");
        }
        else
        {
            Debug.LogWarning("PuzzleManager atanmadý! " + gameObject.name);
        }
    }
}
