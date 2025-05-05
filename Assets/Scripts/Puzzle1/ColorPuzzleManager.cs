using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzleManager : MonoBehaviour
{
    public List<GameObject> colorButtons;
    public float colorShowDelay = 1f;

    public TextMeshProUGUI progressText; // Aşama göstergesi
    public TextMeshProUGUI sequenceDisplayText; // Renk sıralaması
    public TextMeshProUGUI feedbackText; // Kullanıcıya geri bildirim

    public GameObject enemyPrefab;
    public Transform spawnPoint;

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
        // Tüm UI'ları geri aç
        if (sequenceDisplayText != null) sequenceDisplayText.gameObject.SetActive(true);
        if (progressText != null) progressText.gameObject.SetActive(true);
        if (feedbackText != null) feedbackText.text = "";

        canClick = false;
        playerInput.Clear();
        currentSequence.Clear();
        sequenceVisuals.Clear();

        int sequenceLength = currentRound + 2;

        for (int i = 0; i < sequenceLength; i++)
        {
            int rand = Random.Range(0, colorButtons.Count);
            currentSequence.Add(rand);
            sequenceVisuals.Add($"[  ] {colorNames[rand]}");
        }

        if (progressText != null)
            progressText.text = $"LVL: {currentRound}/{maxRounds}";

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
        if (!canClick || isShowing) return;

        int index = colorButtons.IndexOf(clickedButton);
        if (index == -1) return;

        playerInput.Add(index);
        int currentInputIndex = playerInput.Count - 1;
        int correctIndex = currentSequence[currentInputIndex];

        if (index != correctIndex)
        {
            Debug.Log("YANLIŞ! Puzzle başarısız.");
            sequenceVisuals[currentInputIndex] = $"[NO] {colorNames[correctIndex]}";
            UpdateSequenceText();

            // ❌ Diğer yazıları gizle, sadece hata mesajını göster
            if (sequenceDisplayText != null)
                sequenceDisplayText.gameObject.SetActive(false);

            if (progressText != null)
                progressText.gameObject.SetActive(false);

            if (feedbackText != null)
                feedbackText.text = "Yanlis girdin, tekrar dene!";

            SpawnEnemies();
            StartCoroutine(ResetPuzzleAfterDelay());
            return;
        }

        sequenceVisuals[currentInputIndex] = $"[OK] {colorNames[correctIndex]}";
        UpdateSequenceText();

        if (playerInput.Count == currentSequence.Count)
        {
            Debug.Log("Tüm girişler doğru. Bir sonraki tura geçiliyor.");
            currentRound++;

            if (currentRound > maxRounds)
            {
                Debug.Log("Tüm turlar tamamlandı! Oyuncu başarıyla bitirdi.");
            }
            else
            {
                StartCoroutine(StartNextRound());
            }
        }
    }

    void UpdateSequenceText()
    {
        if (sequenceDisplayText == null) return;

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
