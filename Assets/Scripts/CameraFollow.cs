using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Takip Ayarlar�")]
    public Transform target;              // Player objesinin Transform�u
    public Vector3 offset = new Vector3(0, 2, -5);
    public float smoothSpeed = 0.125f;    // 0�a yakla�t�k�a sert, 1�e yakla�t�k�a yumu�ak takip

    void LateUpdate()
    {
        if (target == null) return;

        // 1) Hedef pozisyonunu hesapla
        Vector3 desiredPosition = target.position + offset;
        // 2) Mevcut pozisyon ve hedef pozisyonu yumu�ak�a birle�tir
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // 3) Kameran�n bak�� y�n�n� hedefe �evir, biraz yukar� bakmas� i�in +Vector3.up * x
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
