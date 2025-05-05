using UnityEngine;
using TMPro;

public class ColorTargetPuzzle : MonoBehaviour
{
    public GameObject[] colorTargets;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI pointsText;

    public float timeLimit = 5f;
    public float puzzleDuration = 30f;
    public int requiredPoints = 15;
    public Transform player;
    public string[] colorNames = { "Kırmızı", "Sarı", "Mavi", "Yeşil" };

    private GameObject currentTarget;
    private bool puzzleActive = false;
    private float timer;
    private float totalTimer;
    private int points = 0;

    void Start()
    {
        resultText.text = "";
        instructionText.text = "";
        totalTimer = puzzleDuration;
        StartPuzzle();
        UpdatePointsText();
    }

    void Update()
    {
        if (!puzzleActive) return;

        timer -= Time.deltaTime;
        totalTimer -= Time.deltaTime;

        pointsText.text = $"Puan: {points}";
        resultText.text = $"Süre: {timer:F1} sn";

        if (totalTimer <= 0f)
        {
            puzzleActive = false;
            resultText.text = points >= requiredPoints
                ? $"🎉 Tebrikler! Başarılısın. Puan: {points}"
                : $"⛔ Süre bitti. Yetersiz puan. Puan: {points}";
            return;
        }

        if (timer <= 0f)
        {
            PuzzleFailed();
        }
    }


    public void StartPuzzle()
    {
        int index = Random.Range(0, colorTargets.Length);
        currentTarget = colorTargets[index];
        instructionText.text = $"Hedef: {colorNames[index]} butonuna git!";
        resultText.text = "";
        timer = timeLimit;
        puzzleActive = true;
    }


    void PuzzleSuccess()
    {
        points += 2;
        UpdatePointsText();
        resultText.text = "✅ Doğru hedef! +2 puan.";
        puzzleActive = false;
        Invoke(nameof(StartPuzzle), 1.2f);
    }

    void PuzzleFailed()
    {
        points -= 1;
        UpdatePointsText();
        resultText.text = "⏰ Süre doldu! -1 puan.";
        puzzleActive = false;
        Invoke(nameof(StartPuzzle), 1.2f);
    }

    void UpdatePointsText()
    {
        if (pointsText != null)
            pointsText.text = $"Puan: {points}";
    }

    public void CheckIfCorrect(Collider other)
    {
        if (!puzzleActive) return;

        if (other.gameObject == currentTarget)
        {
            PuzzleSuccess();
        }
        else
        {
            points -= 1;
            UpdatePointsText();
            resultText.text = $"❌ Yanlış hedef! Bu {other.gameObject.name} idi.";
        }
    }
}
