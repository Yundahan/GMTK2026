using UnityEngine;

public class FlyingPathing : MonoBehaviour
{
    [SerializeField]
    private float flyingDistance = 3f;

    Vector3 startingPosition;
    Vector3 targetPosition;
    bool flyingUp = true;
    bool isPathing = true;

    void Start()
    {
        Init();
    }

    void FixedUpdate()
    {
        if (flyingUp)
        {
            
        } else
        {

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
