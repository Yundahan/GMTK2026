using UnityEngine;

public class EnemyBehaviorEight : MonoBehaviour
{
    private enum State
    {
        IDLE,
        OPENING,
        OPEN,
        CLOSING
    }

    private PlayerHealth playerHealth;

    [SerializeField]
    private Collider2D snapArea;
    [SerializeField]
    private float openingDuration = 1f;
    [SerializeField]
    private float closingDuration = 0.3f;
    [SerializeField]
    private float cooldown = 2f;

    private State state = State.IDLE;
    private float lastStateChangeTime = -10000f;

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    void Update()
    {
        if (state == State.OPENING && lastStateChangeTime + openingDuration < Time.time)
        {
            ChangeState(State.OPEN);
            GetComponent<Health>().SetInvulnerability(true);
        }
        if (state == State.OPEN && snapArea.IsTouching(playerHealth.GetHitbox()))
        {
            ChangeState(State.CLOSING);
        }
        if (state == State.CLOSING && lastStateChangeTime + closingDuration < Time.time)
        {
            ChangeState(State.IDLE);
            GetComponent<Health>().SetInvulnerability(false);
        }
    }

    public void OnPlayerDetected()
    {
        if (state != State.IDLE || lastStateChangeTime + cooldown > Time.time)
        {
            return;
        }

        ChangeState(State.OPENING);
    }

    public void OnPlayerLeftDetection()
    {
        // NOOP? Or closing again
        // wie ist das mit damage während die 8 zu ist wenn man einfach dagegen läuft, yes or no damage
    }

    private void ChangeState(State state)
    {
        this.state = state;
        lastStateChangeTime = Time.time;
    }
}
