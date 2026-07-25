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
    private SpriteRenderer idleSpriteRenderer;
    [SerializeField]
    private SpriteRenderer invulnerableSprite;

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
            idleSpriteRenderer.enabled = false;
            invulnerableSprite.enabled = true;
        }
        if (state == State.INVULNERABLE && lastStateChangeTime + invulnerableDuration < Time.time)
        {
            ChangeState(State.STANDING_UP);
            GetComponent<Health>().SetInvulnerability(false);
            idleSpriteRenderer.enabled = true;
            invulnerableSprite.enabled = false;
        }
        if (state == State.STANDING_UP && lastStateChangeTime + standingUpDuration < Time.time)
        {
            ChangeState(State.IDLE);
        }
    }

    void FixedUpdate()
    {
        float scaleFactor = transform.localScale.x > 0 ? 1f : -1f;

        if (state == State.FALLING_OVER)
        {
            float rotationDelta = scaleFactor * Time.fixedDeltaTime * 90 / fallingOverDuration;
            transform.Rotate(new Vector3(0, 0, rotationDelta));
            transform.position -= Time.fixedDeltaTime * 0.8f * Vector3.up;
        } else if (state == State.STANDING_UP)
        {
            float rotationDelta = scaleFactor * Time.fixedDeltaTime * 90 / standingUpDuration;
            transform.Rotate(new Vector3(0, 0, -rotationDelta));
            transform.position += Time.fixedDeltaTime * 0.4f * Vector3.up;
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
            case State.FALLING_OVER:
                // hier stuff machen
                break;
            case State.INVULNERABLE:
                // hier stuff machen
                break;
            case State.STANDING_UP:
                // hier stuff machen
                break;
        }
    }
}
