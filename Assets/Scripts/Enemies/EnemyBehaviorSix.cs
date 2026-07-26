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
    private SpriteRenderer idleSpriteRenderer;
    [SerializeField]
    private SpriteRenderer rollingSpriteRenderer;
    [SerializeField]
    private DetectionArea leftDetectionArea;
    [SerializeField]
    private DetectionArea rightDetectionArea;
    [SerializeField]
    private Transform spriteTransform;
    [SerializeField]
    private Animator animator;

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
    private int wallLayer;
    private float lastStateChangeTime = -10000f;
    private float verticalVelocity = 0f;

    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
        wallLayer = LayerMask.GetMask("Wall");
    }

    void FixedUpdate()
    {
        if ((state == State.ROLLING_LEFT || state == State.ROLLING_RIGHT) && lastStateChangeTime + rollingDuration < Time.time)
        {
            ChangeState(State.IDLE);
        }

        UpdateVerticalVelocity(Time.fixedDeltaTime);
        float horizontalMovement = 0f;
        float scaleFactor = transform.localScale.x > 0 ? 1f : -1f;

        if (state == State.ROLLING_LEFT)
        {
            horizontalMovement = -rollingSpeed * Time.fixedDeltaTime * scaleFactor;
            float rotationDelta = -Time.fixedDeltaTime * 360 * rotationsPerSecond * scaleFactor;
            spriteTransform.Rotate(new Vector3(0, 0, rotationDelta));

        } else if (state == State.ROLLING_RIGHT)
        {
            horizontalMovement = rollingSpeed * Time.fixedDeltaTime * scaleFactor;
            float rotationDelta = Time.fixedDeltaTime * 360 * rotationsPerSecond * scaleFactor;
            spriteTransform.Rotate(new Vector3(0, 0, rotationDelta));
        }

        Vector3 movementDelta = new(horizontalMovement, verticalVelocity * Time.fixedDeltaTime, 0f);

        if (!Physics2D.Raycast(transform.position, movementDelta, movementDelta.magnitude, groundLayer) &&
            !Physics2D.Raycast(transform.position, movementDelta, movementDelta.magnitude, wallLayer))
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
        bool isGrounded = Physics2D.Raycast(transform.position, -transform.up, 3f, groundLayer) || 
            Physics2D.Raycast(transform.position, -transform.up, 3f, wallLayer);
        verticalVelocity = isGrounded ? 0 : verticalVelocity - gravity * elapsedTime;
    }

    private void ChangeState(State state)
    {
        if (state == State.IDLE)
        {
            GetComponent<EnemyAttack>().SetDamageActive(false);
            GetComponent<Pathing>().isPathing = true;
            idleSpriteRenderer.enabled = true;
            rollingSpriteRenderer.enabled = false;
        }
        else
        {
            GetComponent<EnemyAttack>().SetDamageActive(true);
            GetComponent<Pathing>().isPathing = false;
            idleSpriteRenderer.enabled = false;
            rollingSpriteRenderer.enabled = true;
        }

        this.state = state;
        lastStateChangeTime = Time.time;
        SetAnimationVariables(state);
    }

    private void SetAnimationVariables(State state)
    {
        switch (state)
        {
            case State.IDLE:
                animator.SetBool("isIdle", true);
                animator.SetBool("isTuckingout", true);
                animator.SetBool("isTucking", false);
                break;
            case State.ROLLING_LEFT:
                animator.SetBool("isTucking", true);
                animator.SetBool("isTuckingout", false);
                animator.SetBool("isIdle", false);
                break;
            case State.ROLLING_RIGHT:
                animator.SetBool("isTucking", true);
                animator.SetBool("isTuckingout", false);
                animator.SetBool("isIdle", false);
                break;
        }
    }
}
