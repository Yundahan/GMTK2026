using TMPro;
using UnityEngine;

public class EnemyBehaviorSeven : MonoBehaviour
{
    private enum State
    {
        IDLE,
        WINDUP,
        SHOOTING,
        WINDDOWN
    }

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Animator animator;

    private PlayerMovement player;

    [SerializeField]
    private float projectileSpeed = 7f;
    [SerializeField]
    private float windupDuration = 1f;
    [SerializeField]
    private float cooldown = 1f;
    [SerializeField]
    private float winddownDuration = 1f;

    private State state = State.IDLE;
    private float lastStateChangeTime = -10000f;

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        if (state == State.WINDUP && lastStateChangeTime + windupDuration < Time.time)
        {
            ChangeState(State.SHOOTING);
        }
        else if (state == State.SHOOTING && lastStateChangeTime + cooldown < Time.time)
        {
            ChangeState(State.SHOOTING);
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject instance = Instantiate(projectile, transform.position, Quaternion.Euler(0f, 0f, zRotation - 90f));
            instance.GetComponent<Projectile>().Init(direction * projectileSpeed);
        }
        else if (state == State.WINDDOWN && lastStateChangeTime + winddownDuration < Time.time)
        {
            ChangeState(State.IDLE);
        }
    }

    public void OnPlayerDetected()
    {
        if (state == State.IDLE)
        {
            ChangeState(State.WINDUP);
        }
    }

    public void OnPlayerLeftDetection()
    {
        ChangeState(State.WINDDOWN);
    }

    private void ChangeState(State state)
    {
        this.state = state;
        lastStateChangeTime = Time.time;
        SetAnimationVariables(state);
    }

    private void SetAnimationVariables(State state)
    {
        switch (state)
        {
            case State.IDLE:
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isWindup", false);
                animator.SetBool("isWinddown", false);
                break;
            case State.WINDUP:
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isWindup", true);
                animator.SetBool("isWinddown", false);
                break;
            case State.SHOOTING:
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", true);
                animator.SetBool("isWindup", false);
                animator.SetBool("isWinddown", false);
                break;
            case State.WINDDOWN:
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isWindup", false);
                animator.SetBool("isWinddown", true);
                break;
        }
    }
}
