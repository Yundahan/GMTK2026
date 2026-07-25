using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private bool damageActive = true;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (damageActive && collider == playerHealth.GetHitbox())
        {
            playerHealth.SetEnemyPos(transform.position);
            playerHealth.UpdateHealth(-damage);
            SendMessage("PlayerDamaged");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (damageActive && collision.collider == playerHealth.GetHitbox())
        {
            playerHealth.UpdateHealth(-damage);
        }
    }

    public void SetDamageActive(bool value)
    {
        damageActive = value;
    }
}
