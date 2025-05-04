using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Takip Ayarlarý")]
    public Transform target;              // Player objesinin Transform’u
    public Vector3 offset = new Vector3(0, 2, -5);
    public float smoothSpeed = 0.125f;    // 0’a yaklaþtýkça sert, 1’e yaklaþtýkça yumuþak takip

    void LateUpdate()
    {
        if (target == null) return;

        // 1) Hedef pozisyonunu hesapla
        Vector3 desiredPosition = target.position + offset;
        // 2) Mevcut pozisyon ve hedef pozisyonu yumuþakça birleþtir
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // 3) Kameranýn bakýþ yönünü hedefe çevir, biraz yukarý bakmasý için +Vector3.up * x
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
