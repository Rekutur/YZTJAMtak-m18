using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class Stamina : MonoBehaviour
{
    [Header("Stamina Ayarlar�")]
    public float maxStamina = 5f;    // Toplam stamina (saniye cinsinden)
    public float regenRate = 1f;    // Stamina/saniye yenilenme h�z�
    public float depleteRate = 1f;    // Stamina/saniye t�kenme h�z� (ko�arken)

    [Header("UI Referanslar�")]
    public Slider staminaSlider;

    float currentStamina;
    PlayerMovement pm;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        currentStamina = maxStamina;
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }
    }

    void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && pm.IsMoving();
        // PlayerMovement i�inde ekstra IsMoving() metodu tan�mlayaca��z

        if (isRunning && currentStamina > 0f)
        {
            currentStamina -= depleteRate * Time.deltaTime;
            if (currentStamina < 0f)
                currentStamina = 0f;
        }
        else if (!isRunning && currentStamina < maxStamina)
        {
            currentStamina += regenRate * Time.deltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
        }

        if (staminaSlider != null)
            staminaSlider.value = currentStamina;
    }

    public bool HasStamina()
    {
        return currentStamina > 0f;
    }
}
