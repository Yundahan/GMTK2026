using UnityEngine;

public class EnemyBehaviorFour : MonoBehaviour
{
    private enum State
    {
        IDLE,
        HOPPING,
        ATTACKING
    }

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float idleDuration = 2.5f;
    [SerializeField]
    private float hopDuration = 0.5f;

    private State state = State.IDLE;
    private float lastStateChange = -10000f;

    void FixedUpdate()
    {
        if (state == State.IDLE && lastStateChange + idleDuration < Time.time)
        {
            ChangeState(State.HOPPING);
        } else if (state == State.HOPPING && lastStateChange + hopDuration < Time.time)
        {
            TransformUtils.FlipScale(transform);
            ChangeState(State.IDLE);
        }
    }

    public void OnPlayerDetected()
    {
        ChangeState(State.ATTACKING);
        GetComponentInChildren<EnemyAttack>().SetDamageActive(true);
    }

    public void OnPlayerLeftDetection()
    {
        ChangeState(State.IDLE);
        GetComponentInChildren<EnemyAttack>().SetDamageActive(false);
    }

    private void ChangeState(State state)
    {
        if (state == State.IDLE)
        {
            GetComponent<Pathing>().isPathing = true;
        }
        else
        {
            GetComponent<Pathing>().isPathing = false;
        }

        this.state = state;
        lastStateChange = Time.time;
        SetAnimationVariables(state);
    }

    private void SetAnimationVariables(State state)
    {
        switch (state)
        {
            case State.IDLE:
                animator.SetBool("isRetracting", true);
                animator.SetBool("isIdle", true);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isHopping", false);
                break;
            case State.HOPPING:
                animator.SetBool("isHopping", true);
                animator.SetBool("isIdle", false);
                break;
            case State.ATTACKING:
                animator.SetBool("isAttacking", true);
                animator.SetBool("isIdle", false);
                animator.SetBool("isRetracting", false);
                animator.SetBool("isHopping", false);
                break;
        }
    }
}
