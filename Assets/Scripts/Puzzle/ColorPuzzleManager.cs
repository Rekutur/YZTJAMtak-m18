using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzleManager : MonoBehaviour
{
    public List<GameObject> colorButtons; // Kırmızı, sarı, mavi, yeşil butonlar
    public float colorShowDelay = 1f;

    public GameObject enemyPrefab;       // Düşman prefabı
    public Transform spawnPoint;         // Düşman çıkış noktası
    public TextMeshProUGUI sequenceDisplayText; // UI yazısı (Canvas üzerindeki TMP nesnesi)

    public List<string> colorNames = new List<string> { "Kirmizi", "Sari", "Mavi", "Yesil" };

    private List<int> currentSequence = new List<int>();
    private List<int> playerInput = new List<int>();
    private List<string> sequenceVisuals = new List<string>();

    private int currentRound = 1;
    private int maxRounds = 3;

    private bool isShowing = false;
    private bool canClick = false;

    void Start()
    {
        StartCoroutine(StartNextRound());
    }

    IEnumerator StartNextRound()
    {
        canClick = false;
        playerInput.Clear();
        currentSequence.Clear();
        sequenceVisuals.Clear();

        int sequenceLength = currentRound + 2;

        for (int i = 0; i < sequenceLength; i++)
        {
            int rand = Random.Range(0, colorButtons.Count);
            currentSequence.Add(rand);

            // Başlangıçta tüm kutular boş (☐)
            sequenceVisuals.Add($"☐ {colorNames[rand]}");
        }

        UpdateSequenceText();
        yield return ShowSequence();
        canClick = true;
    }

    IEnumerator ShowSequence()
    {
        isShowing = true;

        foreach (int index in currentSequence)
        {
            GameObject btn = colorButtons[index];
            Renderer renderer = btn.GetComponent<Renderer>();
            if (renderer == null) continue;

            Color originalColor = renderer.material.color;

            renderer.material.color = Color.white;
            yield return new WaitForSeconds(0.3f);
            renderer.material.color = originalColor;
            yield return new WaitForSeconds(colorShowDelay);
        }

        isShowing = false;
    }

    public void RegisterClick(GameObject clickedButton)
    {
        Debug.Log("RegisterClick çalıştı: " + clickedButton.name);

        if (!canClick || isShowing) return;

        int index = colorButtons.IndexOf(clickedButton);
        if (index == -1) return;

        playerInput.Add(index);
        int currentInputIndex = playerInput.Count - 1;
        int correctIndex = currentSequence[currentInputIndex];

        // 🟥 Doğru mu kontrol et
        if (index != correctIndex)
        {
            Debug.Log("YANLIŞ! Puzzle başarısız.");
            sequenceVisuals[currentInputIndex] = $"✖ {colorNames[correctIndex]}";
            UpdateSequenceText();

            SpawnEnemies();
            StartCoroutine(ResetPuzzleAfterDelay());
            return;
        }

        // ✅ Doğru seçim
        sequenceVisuals[currentInputIndex] = $"✔ {colorNames[correctIndex]}";
        UpdateSequenceText();

        if (playerInput.Count == currentSequence.Count)
        {
            Debug.Log("Tüm giriş doğru, bir sonraki tura geçiliyor.");
            currentRound++;

            if (currentRound > maxRounds)
            {
                Debug.Log("Tüm turlar tamamlandı! Başarıyla bitirdin.");
                // Başarı ekranı / kapı açma işlemi buraya
            }
            else
            {
                StartCoroutine(StartNextRound());
            }
        }
    }

    void UpdateSequenceText()
    {
        sequenceDisplayText.text = "";
        for (int i = 0; i < sequenceVisuals.Count; i++)
        {
            sequenceDisplayText.text += $"{i + 1}. {sequenceVisuals[i]}\n";
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = spawnPoint.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }

    IEnumerator ResetPuzzleAfterDelay()
    {
        canClick = false;
        yield return new WaitForSeconds(2f);
        currentRound = 1;
        StartCoroutine(StartNextRound());
    }
}
