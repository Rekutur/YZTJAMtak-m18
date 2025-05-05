using UnityEngine;

public class PlayerTriggerCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Red") || other.CompareTag("Blue") || other.CompareTag("Green") || other.CompareTag("Yellow"))
        {
            ColorTargetPuzzle puzzle = other.GetComponent<ColorTargetPuzzle>();
            if (puzzle != null)
            {
                puzzle.CheckIfCorrect(other);
            }
        }
    }
}
