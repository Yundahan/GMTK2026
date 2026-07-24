using UnityEngine;

public class EnemyBehaviorEight : MonoBehaviour
{
    private enum State
    {
        IDLE,
        OPENING,
        OPEN,
        SNAPPING,
        CLOSING
    }

    [SerializeField]
    private DetectionArea outerDetectionArea;
    [SerializeField]
    private DetectionArea innerDetectionArea;
    [SerializeField]
    private Transform leftSnapper;
    [SerializeField]
    private Transform rightSnapper;

    [SerializeField]
    private float openingDuration = 1f;
    [SerializeField]
    private float snappingDuration = 0.3f;
    [SerializeField]
    private float closingDuration = 1f;
    [SerializeField]
    private float cooldown = 2f;

    private State state = State.IDLE;
    private float lastStateChangeTime = -10000f;

    void Update()
    {
        if (state == State.OPENING && lastStateChangeTime + openingDuration < Time.time)
        {
            ChangeState(State.OPEN);
            GetComponent<Health>().SetInvulnerability(true);
            GetComponent<Collider2D>().enabled = false;
        }
        if (state == State.SNAPPING && lastStateChangeTime + snappingDuration < Time.time)
        {
            ChangeState(State.IDLE);
            GetComponent<Health>().SetInvulnerability(false);
            GetComponent<Collider2D>().enabled = true;
        }
        if (state == State.CLOSING && lastStateChangeTime + closingDuration < Time.time)
        {
            ChangeState(State.IDLE);
            GetComponent<Health>().SetInvulnerability(false);
        }
    }

    void FixedUpdate()
    {
        if (state == State.SNAPPING)
        {
            float rotationDelta = Time.fixedDeltaTime * 90 / snappingDuration;
            leftSnapper.Rotate(new Vector3(0, 0, -rotationDelta));
            rightSnapper.Rotate(new Vector3(0, 0, rotationDelta));
        }
    }

    public void OnPlayerDetected(DetectionArea area)
    {
        if (area == outerDetectionArea)
        {
            if ((state != State.IDLE && state != State.CLOSING) || lastStateChangeTime + cooldown > Time.time)
            {
                return;
            }

            ChangeState(State.OPENING);
        } else if (area == innerDetectionArea)
        {
            if (state != State.OPEN)
            {
                return;
            }

            ChangeState(State.SNAPPING);
        }
    }

    public void OnPlayerLeftDetection(DetectionArea area)
    {
        if (area == outerDetectionArea)
        {
            if ((state != State.OPEN && state != State.OPENING))
            {
                return;
            }

            ChangeState(State.CLOSING);
        }
    }

    private void ChangeState(State state)
    {
        switch (state)
        {
            case State.CLOSING:
                if (this.state == State.OPEN)
                {
                    lastStateChangeTime = Time.time;
                }
                else if (this.state == State.OPENING)
                {
                    float timeSinceOpeningStart = Time.time - lastStateChangeTime;
                    float timeLeftToClose = closingDuration - timeSinceOpeningStart;
                    lastStateChangeTime = Time.time - timeLeftToClose;
                }
                break;
            case State.OPENING:
                if (this.state == State.IDLE)
                {
                    lastStateChangeTime = Time.time;
                }
                else if (this.state == State.CLOSING)
                {
                    float timeSinceClosingStart = Time.time - lastStateChangeTime;
                    float timeLeftToOpen = openingDuration - timeSinceClosingStart;
                    lastStateChangeTime = Time.time - timeLeftToOpen;
                }
                break;
            case State.IDLE:
                if (this.state == State.CLOSING)
                {
                    lastStateChangeTime = -10000f;
                }
                else
                {
                    lastStateChangeTime = Time.time;
                }
                break;
            default:
                lastStateChangeTime = Time.time;
                break;
        }

        if (state == State.SNAPPING)
        {
            leftSnapper.gameObject.SetActive(true);
            rightSnapper.gameObject.SetActive(true);
        } else
        {
            leftSnapper.rotation = Quaternion.identity;
            rightSnapper.rotation = Quaternion.identity;
            leftSnapper.gameObject.SetActive(false);
            rightSnapper.gameObject.SetActive(false);
        }

        this.state = state;
    }
}
