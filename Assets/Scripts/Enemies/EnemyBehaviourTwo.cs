using UnityEngine;

public class EnemyBehaviourTwo : MonoBehaviour
{

    private enum State
    {
        IDLE,
        WINDUP,
        SLAPPIN
    }

    [SerializeField]
    private float attackCooldown = 2f;
    [SerializeField]
    private float attackWindupTime = 0.5f;
    [SerializeField]
    private Vector2 boxSize; //with a 1x1 sized object  x=1 and y=1 
    [SerializeField]
    private float castDist; // with a 1x1 sized object castDist = 1 
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float chargeSpeed = 15f;

    private State attackState = State.IDLE;
    private float lastStateChangeTime = -10000f;
    private bool isPathing = true;
    private bool isCharging = false;

    void Update()
    {
        if (!isPathing && isCharging)
        {
            chargeToLastKnownPlayerDirection();
        }
    }

    void FixedUpdate()
    {
        if (attackState == State.WINDUP && lastStateChangeTime + attackWindupTime < Time.time)
        {
            attackState = State.SLAPPIN;
            lastStateChangeTime = Time.time;
            GetComponentInChildren<EnemyAttack>().SetDamageActive(true);
            Debug.Log("Slappin!");
        }
        else if (attackState == State.SLAPPIN)
        {
            attackState = State.IDLE;
            lastStateChangeTime = Time.time;
            GetComponentInChildren<EnemyAttack>().SetDamageActive(false);
            Debug.Log("Idlin");
        }
    }

    public void OnPlayerDetected()
    {
        isPathing = GetComponent<Pathing>().isPathing = false;
        isCharging = true;

        if (attackState == State.IDLE && lastStateChangeTime + attackCooldown < Time.time)
        {
            attackState = State.WINDUP;
            lastStateChangeTime = Time.time;
            Debug.Log("Windup START");
        }

    }

    public void OnPlayerLeftDetection()
    {
        //NOOP
        isCharging = false;
        isPathing = GetComponent<Pathing>().isPathing = true;

    }

    public void chargeToLastKnownPlayerDirection()
    {
        if (IsGroundAhead(transform.right) && transform.localScale.x > 0) //check right
        {
            //charge right
            transform.Translate(chargeSpeed * Time.deltaTime * Vector2.right);
        }
        else if (transform.localScale.x > 0)
        {
            FlipScale();
        }
        if (IsGroundAhead(-transform.right) && transform.localScale.x < 0) // check left
        {
            //charge left
            transform.Translate(chargeSpeed * Time.deltaTime * -Vector2.right);
        }
        else if (transform.localScale.x < 0)
        {
            FlipScale();
        }
    }

    private void FlipScale()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private bool IsGroundAhead(Vector3 direction)
    {
        if (Physics2D.BoxCast(transform.position + direction, boxSize, 0, -transform.up, castDist, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
