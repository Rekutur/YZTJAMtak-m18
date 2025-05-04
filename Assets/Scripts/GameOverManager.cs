using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReturnToMainMenu()
    {
        Debug.Log("Ana Menü butonuna týklandý (þimdilik sahne geçiþi yok).");
        // Ýleride: SceneManager.LoadScene("MainMenu");
    }

}
