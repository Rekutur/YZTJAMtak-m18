using UnityEngine;

public class MouseOrbitCamera : MonoBehaviour
{
    [Header("Hedef ve Baþlangýç Ayarlarý")]
    public Transform target;           // Takip edilecek oyuncu
    public float distance = 5f;        // Baþlangýç uzaklýðý

    [Header("Döndürme Hýzlarý")]
    public float xSpeed = 120f;
    public float ySpeed = 120f;

    [Header("Yukarý-Aþaðý Sýnýrlarý")]
    public float yMinLimit = -10f;
    public float yMaxLimit = 80f;

    [Header("Zoom Sýnýrlarý")]
    public float distanceMin = 2f;
    public float distanceMax = 10f;

    private float x = 0f;
    private float y = 0f;

    void Start()
    {
        // Baþlangýçta þimdiki açýlarý al
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Fizik komponenti varsa dönmesin
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Fare hareketi ile açýlarý güncelle
        x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        // Scroll ile mesafeyi ayarla
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - scroll * 5f, distanceMin, distanceMax);

        // Yeni rotasyon ve pozisyonu uygula
        Quaternion rot = Quaternion.Euler(y, x, 0);
        Vector3 negOffset = new Vector3(0f, 0f, -distance);
        Vector3 pos = rot * negOffset + target.position;

        transform.rotation = rot;
        transform.position = pos;
    }

    // Açý sýnýr fonksiyonu
    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
