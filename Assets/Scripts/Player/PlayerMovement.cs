using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Header("Speed Settings")]
    public float walkSpeed = 5f;            // Yürüyüþ hýzý
    public float runSpeed = 10f;           // Koþma hýzý
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    public TextMeshProUGUI progressText; // Aþama ilerleme göstergesi


    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // 1) Input al
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 2) Hangi hýzda gideceðine karar ver (Shift basýlýysa koþ)
        bool runKey = Input.GetKey(KeyCode.LeftShift);
        bool canRun = runKey && GetComponent<Stamina>().HasStamina();
        float currentSpeed = canRun ? runSpeed : walkSpeed;

        // 3) Kamera bazlý yön vektörünü hesapla
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0; right.y = 0;
        forward.Normalize(); right.Normalize();

        Vector3 moveDir = forward * z + right * x;
        if (moveDir.sqrMagnitude > 1f) moveDir.Normalize();

        // 4) Zýplama
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // 5) Yerçekimini uygula
        velocity.y += gravity * Time.deltaTime;

        // 6) Hareket et
        controller.Move((moveDir * currentSpeed + Vector3.up * velocity.y) * Time.deltaTime);

        // 7) Dönüþ
        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }
        float animationSpeed = moveDir.sqrMagnitude > 0f ? currentSpeed : 0f;
        animator.SetFloat("Speed", animationSpeed);
    }
    public bool IsMoving()
    {
        // WASD veya ok tuþlarýndan herhangi biri basýlý mý?
        return Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f
            || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;
    }
}
