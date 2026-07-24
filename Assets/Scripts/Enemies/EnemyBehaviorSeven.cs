using TMPro;
using UnityEngine;

public class EnemyBehaviorSeven : MonoBehaviour
{
    private enum State
    {
        IDLE,
        WINDUP
    }

    [SerializeField]
    private GameObject projectile;

    private PlayerMovement player;

    [SerializeField]
    private float detectionRange = 10f;
    [SerializeField]
    private float projectileSpeed = 7f;
    [SerializeField]
    private float cooldown = 1f;

    private State state = State.IDLE;
    private float windUpStartTime = -10000f;

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        if (state == State.IDLE && Vector3.Distance(transform.position, player.transform.position) < detectionRange)
        {
            windUpStartTime = Time.time;
            state = State.WINDUP;
        }
        if (state == State.WINDUP && windUpStartTime + cooldown < Time.time)
        {
            state = State.IDLE;
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject instance = Instantiate(projectile, transform.position, Quaternion.Euler(0f, 0f, zRotation - 90f));
            instance.GetComponent<Projectile>().Init(direction * projectileSpeed);
        }
    }
}
