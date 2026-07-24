using UnityEngine;

public class EnemyBehaviorFive : MonoBehaviour
{
    private enum State
    {
        IDLE,
        LEFT_SLAP,
        RIGHT_SLAP,
        LEFT_STANDING_UP,
        RIGHT_STANDING_UP
    }

    [SerializeField]
    private float halfSlapDuration = 1f;
    [SerializeField]
    private GameObject leftAttackArea;
    [SerializeField]
    private GameObject rightAttackArea;

    private State state = State.IDLE;
    private State lastSlapDirection = State.LEFT_SLAP;
    private float lastStateChangeTime = -10000f;

    void FixedUpdate()
    {
        if (state == State.LEFT_SLAP && lastStateChangeTime + halfSlapDuration < Time.time)
        {
            leftAttackArea.GetComponent<EnemyAttack>().SetDamageActive(true);
            leftAttackArea.GetComponent<SpriteRenderer>().enabled = true;//JUST FOR VISUALISATION
            ChangeState(State.LEFT_STANDING_UP);
        }
        else if (state == State.LEFT_STANDING_UP)
        {
            leftAttackArea.GetComponent<EnemyAttack>().SetDamageActive(false);
            leftAttackArea.GetComponent<SpriteRenderer>().enabled = false;//JUST FOR VISUALISATION

            if (lastStateChangeTime + halfSlapDuration < Time.time)
            {
                ChangeState(State.IDLE);
            }
        }
        else if (state == State.RIGHT_SLAP && lastStateChangeTime + halfSlapDuration < Time.time)
        {
            rightAttackArea.GetComponent<EnemyAttack>().SetDamageActive(true);
            rightAttackArea.GetComponent<SpriteRenderer>().enabled = true;//JUST FOR VISUALISATION
            ChangeState(State.RIGHT_STANDING_UP);
        }
        else if (state == State.RIGHT_STANDING_UP)
        {
            rightAttackArea.GetComponent<EnemyAttack>().SetDamageActive(false);
            rightAttackArea.GetComponent<SpriteRenderer>().enabled = false;//JUST FOR VISUALISATION

            if (lastStateChangeTime + halfSlapDuration < Time.time)
            {
                ChangeState(State.IDLE);
            }
        }
    }

    public void OnPlayerDetected()
    {
        if (state == State.IDLE)
        {
            if (lastSlapDirection == State.LEFT_SLAP)
            {
                ChangeState(State.RIGHT_SLAP);
            } else
            {
                ChangeState(State.LEFT_SLAP);
            }
        }
    }

    private void ChangeState(State state)
    {
        if (state == State.RIGHT_SLAP)
        {
            lastSlapDirection = State.RIGHT_SLAP;
        } else if (state == State.LEFT_SLAP)
        {
            lastSlapDirection = State.LEFT_SLAP;
        }

        this.state = state;
        lastStateChangeTime = Time.time;
    }
}
