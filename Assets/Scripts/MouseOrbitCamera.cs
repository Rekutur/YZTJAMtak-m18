using UnityEngine;

public class MouseOrbitCamera : MonoBehaviour
{
    [Header("Hedef ve Ba�lang�� Ayarlar�")]
    public Transform target;           // Takip edilecek oyuncu
    public float distance = 5f;        // Ba�lang�� uzakl���

    [Header("D�nd�rme H�zlar�")]
    public float xSpeed = 120f;
    public float ySpeed = 120f;

    [Header("Yukar�-A�a�� S�n�rlar�")]
    public float yMinLimit = -10f;
    public float yMaxLimit = 80f;

    [Header("Zoom S�n�rlar�")]
    public float distanceMin = 2f;
    public float distanceMax = 10f;

    private float x = 0f;
    private float y = 0f;

    void Start()
    {
        // Ba�lang��ta �imdiki a��lar� al
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Fizik komponenti varsa d�nmesin
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Fare hareketi ile a��lar� g�ncelle
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

    // A�� s�n�r fonksiyonu
    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
