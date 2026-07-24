using UnityEngine;

public class EnemyBehaviorThree : MonoBehaviour
{
    public enum State
    {
        IDLE,
        FALLING_OVER,
        INVULNERABLE,
        STANDING_UP
    }

    [SerializeField]
    private float fallingOverDuration = 0.5f;
    [SerializeField]
    private float invulnerableDuration = 3f;
    [SerializeField]
    private float standingUpDuration = 1f;
    [SerializeField]
    private float cooldown = 2f;

    private State state = State.IDLE;
    private float lastStateChangeTime = -10000f;

    void Update()
    {
        if (state == State.FALLING_OVER && lastStateChangeTime + fallingOverDuration < Time.time)
        {
            ChangeState(State.INVULNERABLE);
            GetComponent<Health>().SetInvulnerability(true);
        }
        if (state == State.INVULNERABLE && lastStateChangeTime + invulnerableDuration < Time.time)
        {
            ChangeState(State.STANDING_UP);
            GetComponent<Health>().SetInvulnerability(false);
        }
        if (state == State.STANDING_UP && lastStateChangeTime + standingUpDuration < Time.time)
        {
            ChangeState(State.IDLE);
        }
    }

    void FixedUpdate()
    {
        if (state == State.FALLING_OVER)
        {
            float rotationDelta = Time.fixedDeltaTime * 90 / fallingOverDuration;
            transform.Rotate(new Vector3(0, 0, rotationDelta));
        } else if (state == State.STANDING_UP)
        {
            float rotationDelta = Time.fixedDeltaTime * 90 / standingUpDuration;
            transform.Rotate(new Vector3(0, 0, -rotationDelta));
        }
    }

    public void OnPlayerDetected()
    {
        if (state != State.IDLE || lastStateChangeTime + cooldown > Time.time)
        {
            return;
        }

        ChangeState(State.FALLING_OVER);
    }

    public void OnPlayerLeftDetection()
    {
        // NOOP
    }

    private void ChangeState(State state)
    {
        this.state = state;
        lastStateChangeTime = Time.time;
    }
}
