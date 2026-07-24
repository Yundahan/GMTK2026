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

    private State attackState = State.IDLE;
    private float lastStateChangeTime = -10000f;


    // Update is called once per frame
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

    }
}
