using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle2Manager : MonoBehaviour
{
    public List<Transform> targetCubes; // Hedef küpler
    public List<string> colorNames = new List<string> { "Kirmizi", "Sari", "Mavi", "Yesil" };
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI pointsText; // 🆕 Puan göstergesi

    public float timeLimit = 5f;
    private float timer;

    private int currentTargetIndex = -1;
    private bool puzzleActive = false;

    public Transform player;
    private int points = 0;

    void Start()
    {
        instructionText.text = "";
        timerText.text = "";
        pointsText.text = "";
        StartNextRound();
    }

    void Update()
    {
        if (!puzzleActive) return;

        timer -= Time.deltaTime;
        timerText.text = "Süre: " + timer.ToString("F1") + " sn";

        // Önce doğru buton kontrolü
        float correctDist = Vector3.Distance(player.position, targetCubes[currentTargetIndex].position);
        if (correctDist < 1.2f)
        {
            points += 2;
            UpdatePointsUI();
            Debug.Log("✅ Doğru hedefe ulaşıldı!");
            instructionText.text = "✅ Dogru!";
            puzzleActive = false;
            StartCoroutine(WaitAndStartNext());
            return; // ❗ burası kritik
        }

        // Sonra yanlış buton kontrolü
        for (int i = 0; i < targetCubes.Count; i++)
        {
            if (i == currentTargetIndex) continue;

            float wrongDist = Vector3.Distance(player.position, targetCubes[i].position);
            if (wrongDist < 1.2f)
            {
                points -= 1;
                UpdatePointsUI();
                Debug.Log("❌ Yanlis buton algılandı.");
                instructionText.text = "❌ Yanlis butona bastin!";
                puzzleActive = false;
                StartCoroutine(WaitAndStartNext());
                return;
            }
        }

        // Süre dolarsa
        if (timer <= 0f)
        {
            points -= 1;
            UpdatePointsUI();
            instructionText.text = "⏰ Süre bitti! -1 puan";
            timerText.text = "";
            Debug.Log("⛔ Süre doldu.");
            puzzleActive = false;
            StartCoroutine(WaitAndStartNext());
        }

        // Puzzle başarılı mı? 15 veya 16 puan
        if (points >= 15 && points <= 16)
        {
            puzzleActive = false;
            instructionText.text = $"🎉 Basarili! Puan: {points}";
            timerText.text = "";
            Debug.Log("🎯 Puzzle basarili!");
        }

        // Eğer puan -3 veya daha düşükse kaybet
        if (points <= -3)
        {
            puzzleActive = false;
            instructionText.text = "⛔ Kaybettiniz! Puan: " + points;
            timerText.text = "";
            Debug.Log("❌ Puzzle kaybedildi!");
        }
    }

    void StartNextRound()
    {
        currentTargetIndex = Random.Range(0, targetCubes.Count);
        instructionText.text = $"Hedef: {colorNames[currentTargetIndex]} küpe git!";
        timer = timeLimit;
        puzzleActive = true;
        Debug.Log("🎯 Yeni hedef: " + colorNames[currentTargetIndex]);
    }

    IEnumerator WaitAndStartNext()
    {
        yield return new WaitForSeconds(1.5f);
        StartNextRound();
    }

    void UpdatePointsUI()
    {
        if (pointsText != null)
            pointsText.text = $"Puan: {points}";
    }
}
