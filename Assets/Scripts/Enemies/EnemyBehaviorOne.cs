using System;
using UnityEngine;

public class EnemyBehaviorOne : MonoBehaviour
{
    private enum State
    {
        IDLE,
        WINDUP,
        ATTACKING
    }

    private PlayerMovement player;

    [SerializeField]
    private float detectionRange = 10f;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float attackWindupTime = 1f;
    [SerializeField]
    private Animator Animator;

    private Vector3 targetPosition;
    private State state = State.IDLE;
    private float windUpStartTime = -10000f;

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        if (state == State.IDLE && Vector3.Distance(transform.position, player.transform.position) < detectionRange)
        {
            windUpStartTime = Time.time;
            ChangeState(State.WINDUP);
        }
        if (state == State.WINDUP && windUpStartTime + attackWindupTime < Time.time)
        {
            ChangeState(State.ATTACKING);
            targetPosition = player.transform.position;
            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();
            float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, zRotation - 90f);
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
                ChangeState(State.IDLE);
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
                Animator.SetBool("isTransforming", false);
                Animator.SetBool("isAttacking", false);
                Animator.SetBool("isChilling", true);
                break;
            case State.WINDUP:
                Animator.SetBool("isTransforming", true);
                Animator.SetBool("isAttacking", false);
                Animator.SetBool("isChilling", false);
                break;
            case State.ATTACKING:
                Animator.SetBool("isTransforming", false);
                Animator.SetBool("isAttacking", true);
                Animator.SetBool("isChilling", false);
                break;
        }
    }
}
