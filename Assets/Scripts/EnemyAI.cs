using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public Transform player;
    public float chaseRange = 10f;
    public float stopDistance = 0.9f;
    public float killDistance = 1f;
    public GameObject gameOverPopup; // 👈 Paneli buraya atayacağız

    private NavMeshAgent agent;
    private bool canChase = false;
    private bool hasSpottedPlayer = false;
    private bool gameEnded = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stopDistance;
        StartCoroutine(DelayBeforeChase());
    }

    IEnumerator DelayBeforeChase()
    {
        yield return new WaitForSeconds(2f);
        canChase = true;
    }

    void Update()
    {
        if (!canChase || player == null || gameEnded) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= chaseRange)
        {
            hasSpottedPlayer = true;
        }

        if (hasSpottedPlayer)
        {
            if (distance <= killDistance)
            {
                gameEnded = true;
                agent.ResetPath();                 // Takibi bırak
                Time.timeScale = 0f;               // Oyunu durdur
                gameOverPopup.SetActive(true);     // Popup'ı aç
                return;
            }

            if (distance > stopDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath();
            }
        }
        Vector3 horizontalVelocity = new Vector3(agent.velocity.x, 0f, agent.velocity.z);
        animator.SetFloat("Speed", horizontalVelocity.magnitude);
    }
}
