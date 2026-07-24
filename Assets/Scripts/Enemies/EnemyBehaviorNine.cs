using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviorNine : MonoBehaviour
{
    public enum State
    {
        IDLE,
        CHARGING,
        FLYING
    }

    private PlayerMovement playerMovement;

    [SerializeField]
    private float chargeTime = 1f;
    [SerializeField]
    private float cooldown = 2f;

    private State state = State.IDLE;
    private float lastStateChangeTime = -10000f;

    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void FixedUpdate()
    {
        if (state == State.CHARGING && lastStateChangeTime + chargeTime < Time.time)
        {
            Vector3 targetPosition = playerMovement.transform.position;
            ChangeState(State.FLYING);
        }
    }

    public void OnPlayerDetected()
    {
        if (state != State.IDLE || lastStateChangeTime + cooldown > Time.time)
        {
            return;
        }

        ChangeState(State.CHARGING);
    }

    private void ChangeState(State state)
    {
        this.state = state;
        lastStateChangeTime = Time.time;
    }
}
