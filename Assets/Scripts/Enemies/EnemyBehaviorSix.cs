using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorSix : MonoBehaviour
{
    public enum State
    {
        IDLE,
        ROLLING_LEFT,
        ROLLING_RIGHT
    }

    [SerializeField]
    private DetectionArea leftDetectionArea;
    [SerializeField]
    private DetectionArea rightDetectionArea;
    [SerializeField]
    private Transform spriteTransform;

    [SerializeField]
    private float rollingDuration = 2f;
    [SerializeField]
    private float rollingSpeed = 7f;
    [SerializeField]
    private float rotationsPerSecond = 3f;
    [SerializeField]
    private float cooldown = 2f;
    [SerializeField]
    private float gravity = 9.81f;

    private State state = State.IDLE;
    private int groundLayer;
    private float lastStateChangeTime = -10000f;
    private float verticalVelocity = 0f;

    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    void FixedUpdate()
    {
        if ((state == State.ROLLING_LEFT || state == State.ROLLING_RIGHT) && lastStateChangeTime + rollingDuration < Time.time)
        {
            ChangeState(State.IDLE);
        }

        UpdateVerticalVelocity(Time.fixedDeltaTime);
        float horizontalMovement = 0f;

        if (state == State.ROLLING_LEFT)
        {
            horizontalMovement = -rollingSpeed * Time.fixedDeltaTime;
            float rotationDelta = Time.fixedDeltaTime * 360 * rotationsPerSecond;
            spriteTransform.Rotate(new Vector3(0, 0, rotationDelta));

        } else if (state == State.ROLLING_RIGHT)
        {
            horizontalMovement = rollingSpeed * Time.fixedDeltaTime;
            float rotationDelta = -Time.fixedDeltaTime * 360 * rotationsPerSecond;
            spriteTransform.Rotate(new Vector3(0, 0, rotationDelta));
        }

        Vector3 movementDelta = new Vector3(horizontalMovement, verticalVelocity * Time.fixedDeltaTime, 0f);

        if (!Physics2D.Raycast(transform.position, movementDelta, 1f + movementDelta.magnitude, groundLayer))
        {
            transform.position = transform.position + movementDelta;
        }
    }

    public void OnPlayerDetected(DetectionArea area)
    {
        if (state != State.IDLE || lastStateChangeTime + cooldown > Time.time)
        {
            return;
        }

        if (area == leftDetectionArea)
        {
            ChangeState(State.ROLLING_LEFT);
        }
        else if (area == rightDetectionArea)
        {
            ChangeState(State.ROLLING_RIGHT);
        }
    }

    public void UpdateVerticalVelocity(float elapsedTime)
    {
        bool isGrounded = Physics2D.Raycast(transform.position, -transform.up, 1f, groundLayer);
        verticalVelocity = isGrounded ? 0 : verticalVelocity - gravity * elapsedTime;
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
            case State.ROLLING_LEFT:
                // hier stuff machen
                break;
            case State.ROLLING_RIGHT:
                // hier stuff machen
                break;
        }
    }
}
