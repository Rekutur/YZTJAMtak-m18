using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class Stamina : MonoBehaviour
{
    [Header("Stamina Ayarlarý")]
    public float maxStamina = 5f;    // Toplam stamina (saniye cinsinden)
    public float regenRate = 1f;    // Stamina/saniye yenilenme hýzý
    public float depleteRate = 1f;    // Stamina/saniye tükenme hýzý (koþarken)

    [Header("UI Referanslarý")]
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
        // PlayerMovement içinde ekstra IsMoving() metodu tanýmlayacaðýz

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
