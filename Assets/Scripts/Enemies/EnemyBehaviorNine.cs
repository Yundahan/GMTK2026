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
    [SerializeField]
    private float gravity = 9.81f;
    [SerializeField]
    private LayerMask groundLayer;

    private State state = State.IDLE;
    private float lastStateChangeTime = -10000f;
    [SerializeField]
    private float horizontalVelocity = 0f;
    [SerializeField]
    private float verticalVelocity = 0f;

    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void FixedUpdate()
    {
        if (state == State.CHARGING && lastStateChangeTime + chargeTime < Time.time)
        {
            if (!ComputeSpeed(transform.position, playerMovement.transform.position))
            {
                return;
            }

            ChangeState(State.FLYING);
        } else if (state == State.FLYING)
        {
            Vector3 movementDelta = new(horizontalVelocity * Time.fixedDeltaTime, verticalVelocity * Time.fixedDeltaTime, 0f);
            transform.position = transform.position + movementDelta;

            if (verticalVelocity < 0f && Physics2D.Raycast(transform.position, -transform.up, 1f, groundLayer))
            {
                verticalVelocity = 0;
                horizontalVelocity = 0f;
                ChangeState(State.IDLE);
            } else
            {
                verticalVelocity -= gravity * Time.fixedDeltaTime;
            }
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

    private bool ComputeSpeed(Vector3 ownPosition, Vector3 targetPosition)
    {
        float deltaX = targetPosition.x - ownPosition.x;
        float deltaY = targetPosition.y - ownPosition.y;

        if (deltaX == 0)
        {
            deltaX = 0.1f;
        }

        float timeUntilTarget = Mathf.Sqrt(2 * deltaY / gravity);

        if (timeUntilTarget == 0f)
        {
            return false;
        }

        verticalVelocity = gravity * timeUntilTarget * 1.1f;
        horizontalVelocity = deltaX / timeUntilTarget;
        return true;
    }

    private void ChangeState(State state)
    {
        this.state = state;
        lastStateChangeTime = Time.time;
    }
}
