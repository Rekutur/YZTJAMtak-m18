using UnityEngine;

public class ColorButton : MonoBehaviour
{
    public ColorPuzzleManager puzzleManager;

    void OnMouseDown()
    {
        if (puzzleManager != null)
        {
            puzzleManager.RegisterClick(gameObject);
            Debug.Log(gameObject.name + " butonuna t�kland�.");
        }
        else
        {
            Debug.LogWarning("PuzzleManager atanmad�! " + gameObject.name);
        }
    }
}
