using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class EnemyBehaviorNine : MonoBehaviour
{
    public enum State
    {
        IDLE,
        CHARGING,
        FLYING
    }

    [SerializeField]
    private Transform spriteTransform;

    private PlayerMovement playerMovement;

    [SerializeField]
    private float chargeTime = 1f;
    [SerializeField]
    private float cooldown = 2f;
    [SerializeField]
    private float gravity = 9.81f;
    [SerializeField]
    private float rotationsPerSecond = 3f;
    [SerializeField]
    private LayerMask groundLayer;

    private State state = State.IDLE;
    private float lastStateChangeTime = -10000f;
    private float horizontalVelocity = 0f;
    private float verticalVelocity = 0f;
    private float rotationDelta = 0f;

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
            spriteTransform.Rotate(new Vector3(0, 0, rotationDelta));

            if (verticalVelocity < 0f && Physics2D.Raycast(transform.position, -transform.up, 2f, groundLayer))
            {
                verticalVelocity = 0;
                horizontalVelocity = 0f;
                spriteTransform.rotation = Quaternion.identity;
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

        float timeUntilTarget = Mathf.Sqrt(2 * Mathf.Abs(deltaY) / gravity);

        if (timeUntilTarget == 0f)
        {
            return false;
        }

        verticalVelocity = gravity * timeUntilTarget * 1.1f;
        horizontalVelocity = deltaX / timeUntilTarget;

        if (deltaX > 0)
        {
            rotationDelta = -Time.fixedDeltaTime * 360 * rotationsPerSecond;
        } else
        {
            rotationDelta = Time.fixedDeltaTime * 360 * rotationsPerSecond;
        }

        return true;
    }

    private void ChangeState(State state)
    {
        this.state = state;
        lastStateChangeTime = Time.time;
        SetAnimationVariables(state);
    }

    private void SetAnimationVariables(State state)
    {
        switch (state)
        {
            case State.IDLE:
                // hier stuff machen
                break;
            case State.CHARGING:
                // hier stuff machen
                break;
            case State.FLYING:
                // hier stuff machen
                break;
        }
    }
}
