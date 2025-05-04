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
        Debug.Log("Ana Men� butonuna t�kland� (�imdilik sahne ge�i�i yok).");
        // �leride: SceneManager.LoadScene("MainMenu");
    }

}
