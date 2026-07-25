using System;
using UnityEngine;

public class EnemyBehaviorOne : MonoBehaviour
{
    private enum State
    {
        IDLE,
        WINDUP,
        ATTACKING,
        WINDDOWN
    }

    private PlayerMovement player;

    [SerializeField]
    private float detectionRange = 10f;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float attackWindupTime = 1f;
    [SerializeField]
    private float attackWinddownTime = 1f;
    [SerializeField]
    private Animator animator;

    private Vector3 targetPosition;
    private State state = State.IDLE;
    private float lastStateChange = -10000f;

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        if (state == State.WINDDOWN || state == State.IDLE && Vector3.Distance(transform.position, player.transform.position) < detectionRange)
        {
            lastStateChange = Time.time;
            ChangeState(State.WINDUP);
        }
        if (state == State.WINDUP && lastStateChange + attackWindupTime < Time.time)
        {
            ChangeState(State.ATTACKING);
            targetPosition = player.transform.position;
            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();
            float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, zRotation - 90f);
        }
        if (state == State.WINDDOWN && lastStateChange + attackWinddownTime < Time.time)
        {
            lastStateChange = Time.time;
            ChangeState(State.IDLE);
        }
    }

    void FixedUpdate()
    {
        if (state == State.ATTACKING)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            Vector3 movementVector = targetPosition - transform.position;
            movementVector.Normalize();
            movementVector *= speed * Time.fixedDeltaTime;

            if (movementVector.magnitude >= distanceToTarget - 0.001f)
            {
                transform.position = targetPosition;
                ChangeState(State.WINDDOWN);             
                transform.rotation = Quaternion.identity;
            }
            else
            {
                transform.position += movementVector;
            }
        }
    }

    private void ChangeState(State state)
    {
        this.state = state;
        SetAnimationVariables(state);
    }

    private void SetAnimationVariables(State state)
    {
        switch (state)
        {
            case State.IDLE:
                animator.SetBool("isTransforming", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isChilling", true);
                break;
            case State.WINDUP:
                animator.SetBool("isTransforming", true);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isChilling", false);
                break;
            case State.ATTACKING:
                animator.SetBool("isTransforming", false);
                animator.SetBool("isAttacking", true);
                animator.SetBool("isChilling", false);
                break;
            case State.WINDDOWN:
                animator.SetBool("isGoingback", true);
                break;
        }
    }
}
