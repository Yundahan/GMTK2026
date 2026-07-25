using UnityEngine;

public class EnemyBehaviourTwo : MonoBehaviour
{

    private enum State
    {
        IDLE,
        CHARGE,
        SLAPPIN
    }

    [SerializeField]
    private Vector2 boxSize; //with a 1x1 sized object  x=1 and y=1 
    [SerializeField]
    private float castDist; // with a 1x1 sized object castDist = 1 
    [SerializeField]
    private float chargeSpeed = 15f;

    private float rayDist = 1f;
    private State attackState = State.IDLE;
    private int groundLayer;
    private int wallLayer;

    private bool isPathing = true;
    private bool isCharging = false;

    private PlayerHealth playerHealth;
    private EnemyAttack enemyAttack;
    private Collider2D enemyAttackCollider;

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        enemyAttack = FindFirstObjectByType<EnemyAttack>();
        enemyAttackCollider = GetComponentInChildren<EnemyAttack>().GetComponent<Collider2D>();
        wallLayer = LayerMask.GetMask("Wall");
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        if (!isPathing && isCharging)
        {
            chargeToLastKnownPlayerDirection();
        }
    }

    void FixedUpdate()
    {
        if (attackState == State.CHARGE)
        {
            ChangeState(State.SLAPPIN);
            enemyAttack.SetDamageActive(true);
        }
        else if (attackState == State.SLAPPIN && enemyAttackCollider.IsTouching(playerHealth.GetHitbox())) //check if in slappin state and able to slap player
        {
            ChangeState(State.IDLE);
            enemyAttack.SetDamageActive(false);
        }
    }

    public void OnPlayerDetected()
    {
        isPathing = GetComponent<Pathing>().isPathing = false;
        isCharging = true;

        if (attackState == State.IDLE)
        {
            ChangeState(State.CHARGE);
        }

    }

    public void OnPlayerLeftDetection()
    {
        isCharging = false;
        isPathing = GetComponent<Pathing>().isPathing = true;
    }

    private void chargeToLastKnownPlayerDirection()
    {

        if (IsGroundAhead(transform.right) && !IsWallOrGroundAhead(transform.right) && transform.localScale.x > 0) //check right
        {
            //charge right
            transform.Translate(chargeSpeed * Time.deltaTime * Vector2.right);
        }
        else if (transform.localScale.x > 0 && !enemyAttackCollider.IsTouching(playerHealth.GetHitbox()))
        {
            TransformUtils.FlipScale(transform);
        }
        if (IsGroundAhead(-transform.right) && !IsWallOrGroundAhead(-transform.right) && transform.localScale.x < 0) // check left
        {
            //charge left
            transform.Translate(chargeSpeed * Time.deltaTime * -Vector2.right);
        }
        else if (transform.localScale.x < 0 && !enemyAttackCollider.IsTouching(playerHealth.GetHitbox()))
        {
            TransformUtils.FlipScale(transform);
        }
    }

    private bool IsGroundAhead(Vector3 direction)
    {
        return Physics2D.BoxCast(transform.position + direction, boxSize, 0, -transform.up, castDist, groundLayer);
    }

    private void ChangeState(State state)
    {
        this.attackState = state;
        SetAnimationVariables(state);
    }

    private bool IsWallOrGroundAhead(Vector3 direction)
    {
        if (Physics2D.Raycast(transform.position, direction, rayDist, wallLayer))
        {
            return true;
        }
        else
        {
            return Physics2D.Raycast(transform.position, direction, rayDist, groundLayer);
        }
    }

    private void SetAnimationVariables(State state)
    {
        switch (state)
        {
            case State.IDLE:
                // hier stuff machen
                break;
            case State.CHARGE:
                // hier stuff machen
                break;
            case State.SLAPPIN:
                // hier stuff machen
                break;
        }
    }
}
