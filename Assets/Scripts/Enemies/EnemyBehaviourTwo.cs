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
    private LayerMask groundLayer;
    [SerializeField]
    private float chargeSpeed = 15f;

    private State attackState = State.IDLE;
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
            attackState = State.SLAPPIN;
            enemyAttack.SetDamageActive(true);
        }
        else if (attackState == State.SLAPPIN && enemyAttackCollider.IsTouching(playerHealth.GetHitbox())) //check if in slappin state and able to slap player
        {
            attackState = State.IDLE;
            enemyAttack.SetDamageActive(false);
        }
    }

    public void OnPlayerDetected()
    {
        isPathing = GetComponent<Pathing>().isPathing = false;
        isCharging = true;

        if (attackState == State.IDLE)
        {
            attackState = State.CHARGE;
        }

    }

    public void OnPlayerLeftDetection()
    {
        isCharging = false;
        isPathing = GetComponent<Pathing>().isPathing = true;
    }

    private void chargeToLastKnownPlayerDirection()
    {

        if (IsGroundAhead(transform.right) && transform.localScale.x > 0) //check right
        {
            //charge right
            transform.Translate(chargeSpeed * Time.deltaTime * Vector2.right);
        }
        else if (transform.localScale.x > 0 && !enemyAttackCollider.IsTouching(playerHealth.GetHitbox()))
        {
            TransformUtils.FlipScale(transform);
        }
        if (IsGroundAhead(-transform.right) && transform.localScale.x < 0) // check left
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
