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
    [SerializeField]
    private Animator animator;

    private float rayDist = 1f;
    [SerializeField]
    private State attackState = State.IDLE;
    private int groundLayer;
    private int wallLayer;

    private bool isPathing = true;
    private bool isCharging = false;
    private float deltaX = 1f;

    private PlayerHealth playerHealth;
    private EnemyAttack enemyAttack;
    private Collider2D enemyAttackCollider;

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        enemyAttack = GetComponentInChildren<EnemyAttack>();
        enemyAttackCollider = enemyAttack.GetComponent<Collider2D>();
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
        }
        else if (attackState == State.SLAPPIN && deltaX * (playerHealth.transform.position.x - transform.position.x) < 0f) //check if in slappin state, but past the players x position
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
            deltaX = playerHealth.transform.position.x - transform.position.x;
            ChangeState(State.CHARGE);
            enemyAttack.SetDamageActive(true);
        }
    }

    public void OnPlayerLeftDetection()
    {
        isCharging = false;
        ChangeState(State.IDLE);
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

    private bool IsWallOrGroundAhead(Vector3 direction)
    {
        return Physics2D.Raycast(transform.position, direction, rayDist, wallLayer) ||
            Physics2D.Raycast(transform.position, direction, rayDist, groundLayer);
    }

    private void ChangeState(State state)
    {
        this.attackState = state;
        SetAnimationVariables(state);
    }

    private void SetAnimationVariables(State state)
    {
        switch (state)
        {
            case State.IDLE:
                animator.SetBool("isWalking", true);
                animator.SetBool("isSlapping", false);
                break;
            case State.CHARGE:
                animator.SetBool("isWalking", true);
                animator.SetBool("isSlapping", false);
                break;
            case State.SLAPPIN:
                animator.SetBool("isSlapping", true);
                animator.SetBool("isWalking", false);
                break;
        }
    }
}
