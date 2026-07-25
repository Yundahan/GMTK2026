using UnityEngine;

public class FlyingPathing : MonoBehaviour
{
    [SerializeField]
    private float flyingDistance = 3f;
    [SerializeField]
    private float smoothing = 0.5f;
    [SerializeField]
    private float speed = 2f;

    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private bool flyingUp = true;
    private bool isPathing = true;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        Init();
    }

    void FixedUpdate()
    {
        if (!isPathing)
        {
            return;
        }

        if (flyingUp)
        {
            if (transform.position.y + 0.01f > targetPosition.y)
            {
                flyingUp = false;
            }

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing, speed);
        } else
        {
            if (transform.position.y - 0.01f < startingPosition.y)
            {
                flyingUp = true;
            }

            transform.position = Vector3.SmoothDamp(transform.position, startingPosition, ref velocity, smoothing, speed);
        }
    }

    public void Init()
    {
        startingPosition = transform.position;
        targetPosition = startingPosition + flyingDistance * Vector3.up;
        flyingUp = true;
        SetPathing(true);
    }

    public void SetPathing(bool value)
    {
        isPathing = value;
    }
}
